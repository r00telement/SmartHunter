using SmartHunter.Core;
using SmartHunter.Core.Helpers;
using SmartHunter.Game.Data.ViewModels;
using SmartHunter.Game.Helpers;
using System.Linq;

namespace SmartHunter.Game
{
    public class MhwMemoryUpdater : MemoryUpdater
    {
        BytePattern m_PlayerDamagePattern = new BytePattern(ConfigHelper.Memory.Values.PlayerDamagePattern);
        BytePattern m_PlayerNamePattern = new BytePattern(ConfigHelper.Memory.Values.PlayerNamePattern);
        BytePattern m_MonsterPattern = new BytePattern(ConfigHelper.Memory.Values.MonsterPattern);
        BytePattern m_MonsterOffsetPattern = new BytePattern(ConfigHelper.Memory.Values.MonsterOffsetPattern);
        BytePattern m_PlayerBuffPattern = new BytePattern(ConfigHelper.Memory.Values.PlayerBuffPattern);

        protected override string ProcessName
        {
            get
            {
                return ConfigHelper.Memory.Values.ProcessName;
            }
        }

        protected override int ThreadsPerScan
        {
            get
            {
                return ConfigHelper.Memory.Values.ThreadsPerScan;
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
                    m_MonsterOffsetPattern,
                    m_PlayerBuffPattern
                };
            }
        }

        protected override int UpdatesPerSecond
        {
            get
            {
                return ConfigHelper.Main.Values.Overlay.UpdatesPerSecond;
            }
        }

        protected override bool ShutdownWhenProcessExits
        {
            get
            {
                return ConfigHelper.Main.Values.ShutdownWhenProcessExits;
            }
        }

        public MhwMemoryUpdater()
        {
            ConfigHelper.Main.Loaded += (s, e) => { TryUpdateTimerInterval(); };
        }

        protected override void UpdateMemory()
        {
            UpdateVisibility();

            bool traceUniquePointers = ConfigHelper.Main.Values.Debug.TraceUniquePointers;

            if (ConfigHelper.Main.Values.Overlay.MonsterWidget.IsVisible)
            {
                var monsterRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_MonsterPattern.MatchedAddresses.First());
                var monsterOffset = MemoryHelper.ReadStaticOffset(Process, m_MonsterOffsetPattern.MatchedAddresses.First());
                var lastMonsterAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, monsterRootPtr, monsterOffset, 0x8F9BC * 8, 0, 0);

                MhwHelper.UpdateMonsterWidget(Process, lastMonsterAddress);
            }
            else if (OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Any())
            {
                OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Clear();
            }

            if (ConfigHelper.Main.Values.Overlay.TeamWidget.IsVisible)
            {
                ulong playerNamesPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerNamePattern.MatchedAddresses.First());
                var playerDamageRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerDamagePattern.MatchedAddresses.First());
                var playerDamageCollectionAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerDamageRootPtr, 0x48 + 0x20 * 0x58);
                var playerNamesAddress = MemoryHelper.Read<uint>(Process, playerNamesPtr);

                MhwHelper.UpdateTeamWidget(Process, playerDamageCollectionAddress, playerNamesAddress);
            }
            else if (OverlayViewModel.Instance.TeamWidget.Context.Players.Any())
            {
                OverlayViewModel.Instance.TeamWidget.Context.Players.Clear();
            }

            if (ConfigHelper.Main.Values.Overlay.PlayerWidget.IsVisible)
            {
                var playerBuffRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerBuffPattern.MatchedAddresses.First());

                // The local player is guaranteed to be the last item in the list,
                // So, keep reading each pointer in the collection until we reach null
                var buffPtr = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerBuffRootPtr, 0X9B0 + 0XC8, 0);
                ulong lastBuffAddress = 0;
                ulong currentBuffAddress = MemoryHelper.Read<ulong>(Process, buffPtr);
                while (currentBuffAddress != 0)
                {
                    lastBuffAddress = currentBuffAddress;
                    buffPtr += 8;
                    currentBuffAddress = MemoryHelper.Read<ulong>(Process, buffPtr);
                }

                var buffAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, lastBuffAddress + 0x79D0, 0);
                var equipmentAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, buffAddress + 0x8, 0x70, 0x78, 0x50, -0x10);
                var weaponAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, buffAddress + 0x10, 0x8, 0x78, 0x48, 0x0);

                var isBuffAddressValid = MemoryHelper.Read<float>(Process, equipmentAddress + 0x20) != 0;
                var isEquipmentAddressValid = MemoryHelper.Read<ulong>(Process, equipmentAddress + 0x8) == 0;
                if (isBuffAddressValid && isEquipmentAddressValid)
                {
                    MhwHelper.UpdatePlayerWidget(Process, buffAddress, equipmentAddress, weaponAddress);
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
                OverlayViewModel.Instance.IsGameActive = false;
            }
            else if (!OverlayViewModel.Instance.IsVisible && 
                (!ConfigHelper.Main.Values.Overlay.HideWhenGameWindowIsInactive || foregroundWindowHandle == Process.MainWindowHandle))
            {
                OverlayViewModel.Instance.IsGameActive = true;
            }
        }
    }
}
