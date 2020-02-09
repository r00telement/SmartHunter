using System.Linq;
using SmartHunter.Core;
using SmartHunter.Core.Helpers;
using SmartHunter.Game.Data.ViewModels;
using SmartHunter.Game.Helpers;

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

            if (ConfigHelper.Main.Values.Overlay.MonsterWidget.IsVisible && m_MonsterPattern.MatchedAddresses.Any())
            {
                /*
                var range = new AddressRange(0x140000000, 0x163B0A000);
                //var pattern = new BytePattern(new Config.BytePatternConfig("48 8B 05 ?? ?? ?? ?? 41 8B", "140000000", "163B0A000", null));

                var pattern = new BytePattern(new Config.BytePatternConfig("null", "48 8B 05 ?? ?? ?? ?? 41 8B", "140000000", "163B0A000", null));
                var matches = MemoryHelper.FindPatternAddresses(Process, range, pattern, false);

                //Log.WriteLine($"Found {matches.Count()} matches...");

                ulong[] lookingFor = new ulong[1] { 0x144F03280 };

                var res = new System.Collections.Generic.List<ulong>();

                foreach (ulong address in matches)
                {
                    ulong lear = MemoryHelper.LoadEffectiveAddressRelative(Process, address);
                    foreach (ulong looking in lookingFor)
                    {
                        if (looking == lear)
                        {
                            res.Add(address);
                            //Log.WriteLine($"Found match for 0x{looking} at 0x{address}");
                        }
                    }
                }
                */
                var monsterRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_MonsterPattern.MatchedAddresses.First()) - 0x36CE0; // yeah i know this is basically a static pointer
                var monsterBaseList = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, monsterRootPtr, 0x128, 0x8, 0x0);

                MhwHelper.UpdateMonsterWidget(Process, monsterBaseList);
            }
            else if (OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Any())
            {
                OverlayViewModel.Instance.MonsterWidget.Context.Monsters.Clear();
            }

            if (ConfigHelper.Main.Values.Overlay.TeamWidget.IsVisible && m_PlayerDamagePattern.MatchedAddresses.Any())
            {
                ulong playerNamesPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerNamePattern.MatchedAddresses.First());
                var playerDamageRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerDamagePattern.MatchedAddresses.First());
                var playerDamageCollectionAddress = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerDamageRootPtr, (long)MhwHelper.DataOffsets.PlayerDamageCollection.FirstPlayerPtr + (long)MhwHelper.DataOffsets.PlayerDamageCollection.MaxPlayerCount * sizeof(long) * (long)MhwHelper.DataOffsets.PlayerDamageCollection.NextPlayerPtr);
                var playerNamesAddress = MemoryHelper.Read<uint>(Process, playerNamesPtr);

                MhwHelper.UpdateTeamWidget(Process, playerDamageCollectionAddress, playerNamesAddress);
            }
            else if (OverlayViewModel.Instance.TeamWidget.Context.Players.Any())
            {
                OverlayViewModel.Instance.TeamWidget.Context.Players.Clear();
            }

            if (ConfigHelper.Main.Values.Overlay.PlayerWidget.IsVisible && m_PlayerBuffPattern.MatchedAddresses.Any())
            {
                var playerBuffRootPtr = MemoryHelper.LoadEffectiveAddressRelative(Process, m_PlayerBuffPattern.MatchedAddresses.First());

                var buffPtr = MemoryHelper.ReadMultiLevelPointer(traceUniquePointers, Process, playerBuffRootPtr, 0x8C8, 0x1C0, 0x0);

                ulong lastBuffAddress = MemoryHelper.Read<ulong>(Process, buffPtr - 0x38) + 0x80;

                ulong equipmentAddress = MemoryHelper.Read<ulong>(Process, lastBuffAddress + 0x14F8);
                ulong weaponAddress = MemoryHelper.Read<ulong>(Process, lastBuffAddress + 0x76B0);
                ulong buffAddress = MemoryHelper.Read<ulong>(Process, lastBuffAddress + 0x7D20);

                MhwHelper.UpdatePlayerWidget(Process, buffAddress, equipmentAddress, weaponAddress);
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
