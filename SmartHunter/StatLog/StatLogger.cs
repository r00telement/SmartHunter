using System.Collections.Generic;
using SmartHunter.Game.Data;
using SmartHunter.Core;
using Newtonsoft.Json;
using System;
using System.IO;
using SmartHunter.Game.Helpers;

namespace SmartHunter.StatLog
{
    static class StatLogger
    {
        public static List<Monster> MonsterList { get; set; } = new List<Monster>();

        public static long LastStamp { get; private set; } = 0;
        public static void StartLogger()
        {
            if (StatObject.IsLogging)
            {
                return;
            }
            StatObject.Init();

            // Subscribing to events
            MhwHelper.OnMissionStart += OnMissionStart;
            MhwHelper.OnMissionEnd += OnMissionEnd;

            StatObject.IsLogging = true;
            StatObject.Clear();
            StatObject.Instance.Location = "WIP"; // TODO: Somehow get the Location into this :thinkingEmoji:
            LastStamp = 0;
            Log.WriteLine("Stat Logging Started!");
        }

        private static void OnMissionStart(List<Player> updatedPlayers)
        {
            long stamp = Utils.GetUnixTimeStamp(); // Get Current Time in unix format
            if (!StatObject.IsLogging || stamp <= LastStamp) // If it's not logging, or the last log is younger than a second, do nothing
            {
                return; // Our exit Strategy
            }

            if (MonsterList.Count <= 0)
            {
                return;
            }

            List<StatMonster> _monsters = new List<StatMonster>();
            #region MonsterList
            _monsters.Clear(); // Safety measure
            foreach (Monster mnst in MonsterList)
            {
                StatMonster _temp = new StatMonster
                {
                    MonsterName = mnst.Name,
                    MonsterHP = mnst.Health.Current,
                    MonsterHPMax = mnst.Health.Max,
                    MonsterCrown = mnst.Crown
                };

                _monsters.Add(_temp);
            }
            #endregion
            List<StatPlayer> _players = new List<StatPlayer>();
            #region PlayerList
            _players.Clear(); // Safety measure
            foreach (Player ply in updatedPlayers)
            {
                StatPlayer _temp = new StatPlayer
                {
                    PlayerName = ply.Name,
                    PlayerTotalDmg = ply.Damage,
                    PlayerDps = 0 // TODO: Calculate this 
                };

                _players.Add(_temp);
            }
            #endregion                   

            StatObject.Instance.DataObject
                .Add(new DataObject()
                {
                    TimeStamp = stamp,
                    Monsters = _monsters,
                    Players = _players
                }
                );

            LastStamp = stamp; // Update the old stamp to be ready for the next one
            return;
        }

        private static void OnMissionEnd()
        {
            // TODO
            throw new NotImplementedException();
        }        

        // 

        public static void AddEntry(List<Player> updatedPlayers)
        {            
            long stamp = Utils.GetUnixTimeStamp(); // Get Current Time in unix format
            if ( !StatObject.IsLogging || stamp <= LastStamp ) // If it's not logging, or the last log is younger than a second, do nothing
            {
                return; // Our exit Strategy
            }            

            if( MonsterList.Count <= 0 )
            {
                return;
            }

            List<StatMonster> _monsters = new List<StatMonster>();
            #region MonsterList
            _monsters.Clear(); // Safety measure
            foreach (Monster mnst in MonsterList)
            {
                StatMonster _temp = new StatMonster
                {
                    MonsterName = mnst.Name,
                    MonsterHP = mnst.Health.Current,
                    MonsterHPMax = mnst.Health.Max,
                    MonsterCrown = mnst.Crown
                };

                _monsters.Add(_temp);
            }
            #endregion
            List<StatPlayer> _players = new List<StatPlayer>();
            #region PlayerList
            _players.Clear(); // Safety measure
            foreach (Player ply in updatedPlayers)
            {
                StatPlayer _temp = new StatPlayer
                {
                    PlayerName = ply.Name,
                    PlayerTotalDmg = ply.Damage,
                    PlayerDps = 0 // TODO: Calculate this 
                };

                _players.Add(_temp);
            }
            #endregion                   

            StatObject.Instance.DataObject
                .Add(new DataObject(){
                    TimeStamp = stamp,
                    Monsters = _monsters,
                    Players = _players }
                );

            LastStamp = stamp; // Update the old stamp to be ready for the next one
            return;
        }

        public static void StopLogging()
        {
            if (!StatObject.IsLogging)
            {
                return; // More Safety measures
            }
            StatObject.IsLogging = false;

            string dir = "data";
            Directory.CreateDirectory(dir); // Create the dir if it does not exist yet

            JsonSerialization.WriteToJsonFile<StatObject>($"{dir}\\{LastStamp}.json", StatObject.Instance, false);

            StatObject.Clear(); // Clearing the object so it'll be empty when the next mission starts

            Log.WriteLine("Stat Logging Stopped!");            
        }
    }   

}
