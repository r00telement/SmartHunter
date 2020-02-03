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
                    //m_PlayerBuffPattern
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
                /*
                var range = new AddressRange(0x140000000, 0x140001000);//0x163B0A000);
                //var pattern = new BytePattern(new Config.BytePatternConfig("48 8B 05 ?? ?? ?? ?? 41 8B", "140000000", "163B0A000", null));

                var pattern = new BytePattern(new Config.BytePatternConfig("48 8B 05 ?? ?? ?? ?? 41 8B", "140000000", "163B0A000", null));

                var matches = MemoryHelper.FindPatternAddresses(Process, range, pattern, false);

                var res = new System.Collections.Generic.List<ulong>();

                foreach (ulong address in matches)
                {
                    ulong lear = MemoryHelper.LoadEffectiveAddressRelative(Process, address);
                    if (lear == 0x144C77498 || lear == 0x144CE4150 || lear == 0x144CEA750)// || lear == 0x144ee4630)// || lear == 0x144e437d0)
                    {
                        res.Add(address);
                    }
                }
                */
                var monsterRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_MonsterPattern.MatchedAddresses.First()) - 0x36CE0;//MemoryHelper.LoadEffectiveAddressRelative(Process, m_MonsterPattern.MatchedAddresses.First());
                var monsterBaseList = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, monsterRootPtr, 0x128, 0x8, 0x0);//MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, monsterRootPtr, monsterOffset, 0x18, 0xD0, 0xC8, 0x738, 0x8, 0x0);

                MhwHelper.UpdateMonsterWidget(Process, monsterBaseList);
            }
            else if (OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Any())
            {
                OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Clear();
            }

            if (ConfigHelper.Main.Values.Overlay.TeamWidget.IsVisible)
            {
                ulong playerNamesPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerNamePattern.MatchedAddresses.First()); // Players Damages are not working (probably offset is wrong)
                var playerDamageRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerDamagePattern.MatchedAddresses.First());
                var playerDamageCollectionAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerDamageRootPtr, (long)MhwHelper.DataOffsets.PlayerDamageCollection.FirstPlayerPtr + (long)MhwHelper.DataOffsets.PlayerDamageCollection.MaxPlayerCount * sizeof(long) * (long)MhwHelper.DataOffsets.PlayerDamageCollection.NextPlayerPtr);
                var playerNamesAddress = MemoryHelper.Read<uint>(Process, playerNamesPtr);

                MhwHelper.UpdateTeamWidget(Process, playerDamageCollectionAddress, playerNamesAddress);
            }
            else if (OverlayViewModel.Instance.TeamWidget.Context.Players.Any())
            {
                OverlayViewModel.Instance.TeamWidget.Context.Players.Clear();
            }

            if (ConfigHelper.Main.Values.Overlay.PlayerWidget.IsVisible && false)
            {
                var playerBuffRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerBuffPattern.MatchedAddresses.First());

                // The local player is guaranteed to be the last item in the list,
                // So, keep reading each pointer in the collection until we reach null
                var buffPtr = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerBuffRootPtr, 0x9B0 + 0xC8, 0); // 0xA78
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
