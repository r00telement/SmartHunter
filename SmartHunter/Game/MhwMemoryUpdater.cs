using SmartHunter.Core;
using SmartHunter.Core.Helpers;
using SmartHunter.Game.Data.ViewModels;
using SmartHunter.Game.Helpers;
using System.Linq;

namespace SmartHunter.Game
{
    public class MhwMemoryUpdater : MemoryUpdater
    {
        BytePattern m_PlayerDamagePattern = new BytePattern("48 8B 0D ?? ?? ?? ?? 48 89 7C 24 ?? E8 ?? ?? ?? ?? 48 85 C0 75 ?? 33 FF");
        BytePattern m_PlayerNamePattern = new BytePattern("48 8B D ?? ?? ?? ?? 48 8D 54 24 38 C6 44 24 20 0 4D 8B 40 8 E8 ?? ?? ?? ?? 48 8B 5C 24 60 48 83 C4 50 5F C3");
        BytePattern m_MonsterPattern = new BytePattern("48 8B 15 ?? ?? ?? ?? 48 8B 29 48 63 82 ?? ?? ?? ??");
        BytePattern m_MonsterOffset1Pattern = new BytePattern("48 8B 8B ?? ?? ?? ?? 48 8B 01 FF 50 ?? 48 8B 8B ?? ?? ?? ?? E8 ?? ?? ?? ??  48 8B 8B ?? ?? ?? ?? B2 01 E8 ?? ?? ?? ??");
        BytePattern m_PlayerBuffPattern = new BytePattern("48 8B 0D ?? ?? ?? ?? 48 8B D3 48 8B 01 FF 90 ?? ?? ?? ?? 48 8B CB");
        BytePattern m_PlayerBuffOffsetPattern = new BytePattern("49 8B 8E ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B 47 08 C1 E8 ??");

        protected override string ProcessName
        {
            get
            {
                return "MonsterHunterWorld";
            }
        }

        protected override int ThreadCount
        {
            get
            {
                return 4;
            }
        }

        protected override AddressRange ScanAddressRange
        {
            get
            {
                return new AddressRange(0x140004000, 0x145000000);
            }
        }

        protected override BytePattern[] Patterns
        {
            get
            {
                return new BytePattern[]
                {
                    m_PlayerDamagePattern,
                    m_PlayerNamePattern,
                    m_MonsterPattern,
                    m_MonsterOffset1Pattern,
                    m_PlayerBuffOffsetPattern,
                    m_PlayerBuffPattern
                };
            }
        }

        protected override void UpdateMemory()
        {
            UpdateVisibility();

            bool traceUniquePointers = ConfigHelper.Main.Values.Debug.TraceUniquePointers;

            bool monsterWidgetIsVisible = ConfigHelper.Main.Values.Overlay.MonsterWidget.IsVisible;
            bool teamWidgetIsVisible = ConfigHelper.Main.Values.Overlay.TeamWidget.IsVisible;
            bool playerWidgetIsVisible = ConfigHelper.Main.Values.Overlay.PlayerWidget.IsVisible;

            if (monsterWidgetIsVisible)
            {                
                var monsterAndBuffRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_MonsterPattern.Addresses.First());
                var monsterAndBuffOffset1 = MemoryHelper.ReadStaticOffset(Process, m_MonsterOffset1Pattern.Addresses.First());
                var lastMonsterAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, monsterAndBuffRootPtr, monsterAndBuffOffset1, 0x8F9BC * 8, 0, 0);

                MhwHelper.UpdateMonsterWidget(Process, lastMonsterAddress);
            }
            else if (OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Any())
            {
                OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Clear();
            }

            if (teamWidgetIsVisible)
            {
                ulong playerNamesPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerNamePattern.Addresses.First());
                var playerDamageRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerDamagePattern.Addresses.First());
                var playerDamageCollectionAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerDamageRootPtr, 0x48 + 0x20 * 0x58);
                var playerNamesAddress = MemoryHelper.Read<uint>(Process, playerNamesPtr);

                MhwHelper.UpdateTeamWidget(Process, playerDamageCollectionAddress, playerNamesAddress);
            }
            else if (OverlayViewModel.Instance.TeamWidget.Context.Players.Any())
            {
                OverlayViewModel.Instance.TeamWidget.Context.Players.Clear();
            }

            if (playerWidgetIsVisible)
            {
                int buffIndexOffset = 0;
                if (OverlayViewModel.Instance.TeamWidget.Context.Players.Any())
                {
                    buffIndexOffset = OverlayViewModel.Instance.TeamWidget.Context.Players.Count - 1;
                }

                var playerBuffPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerBuffPattern.Addresses.First());
                var playerBuffOffset = MemoryHelper.ReadStaticOffset(Process, m_PlayerBuffOffsetPattern.Addresses.First());
                var buffAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerBuffPtr, 0X9B0 + 0XC8, buffIndexOffset * 8, playerBuffOffset, 0);

                var buffAddressValidity = MemoryHelper.Read<float>(Process, buffAddress + 0x20);
                if (buffAddressValidity != 0)
                {
                    var equipmentAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, buffAddress + 0x8, 0x70, 0x78, 0X50, -0x10);
                    MhwHelper.UpdatePlayerWidget(Process, buffAddress, equipmentAddress);
                }
                else if (OverlayViewModel.Instance.PlayerWidget.Context.StatusEffects.Any())
                {
                    OverlayViewModel.Instance.PlayerWidget.Context.StatusEffects.Clear();
                }
            }
            else if (OverlayViewModel.Instance.PlayerWidget.Context.StatusEffects.Any())
            {
                OverlayViewModel.Instance.PlayerWidget.Context.StatusEffects.Clear();
            }
        }

        void UpdateVisibility()
        {
            // Show or hide the overlay depending on whether the game process is active
            var foregroundWindowHandle = WindowsApi.GetForegroundWindow();
            if (ConfigHelper.Main.Values.Overlay.HideWhenGameWindowIsInactive && OverlayViewModel.Instance.IsVisible && foregroundWindowHandle != Process.MainWindowHandle)
            {
                OverlayViewModel.Instance.IsVisible = false;
            }
            else if (!OverlayViewModel.Instance.IsVisible && 
                (!ConfigHelper.Main.Values.Overlay.HideWhenGameWindowIsInactive || foregroundWindowHandle == Process.MainWindowHandle))
            {
                OverlayViewModel.Instance.IsVisible = true;
            }
        }
    }
}
