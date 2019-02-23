﻿using System.Collections.Generic;
using SmartHunter.Game.Data;
using SmartHunter.Core;
using Newtonsoft.Json;
using System;

namespace SmartHunter.StatLog
{
    class StatLogger
    {
        public static List<Monster> MonsterList { get; set; } = new List<Monster>();

        public static long LastStamp { get; private set; } = 0;
        public static void StartLogging()
        {
            if (StatObject.IsLogging)
            {
                return;
            }
            StatObject.Init(); // TODO: Do this at a better, Global place (main?)           

            StatObject.IsLogging = true;
            StatObject.Instance.Location = "WIP"; // TODO: Somehow get the Location into this :thinkingEmoji:
            Log.WriteLine("Stat Logging Started!");
        }

        public static bool AddEntry(List<Player> updatedPlayers)
        {            
            StatObject.Init(); // TODO: Do this at a better, Global place (main?)

            long stamp = Utils.GetUnixTimeStamp(); // Get Current Time in unix format
            if (!StatObject.IsLogging || stamp <= LastStamp) // If it's not logging, or the last log is younger than a second, do nothing
            {
                return false; // Our exit Strategy
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
            return true;
        }

        public static void StopLogging()
        {
            StatObject.Init(); // TODO: Do this at a better, Global place (main?)
            StatObject.IsLogging = false;
            JsonSerialization.WriteToJsonFile<StatObject>("test.json", StatObject.Instance, false);
            Log.WriteLine("Stat Logging Stopped!");            
        }
    }

    class StatObject
    {
        public string Location { get; set; }
        public List<DataObject> DataObject { get; set; }

        #region SingletonStuff
        [JsonIgnore] // We don't want this in the final log file spammed all over the place
        public static bool IsLogging { get; set; }
        [JsonIgnore] // Same here
        public static StatObject Instance { get; private set; } // Singleton, we only want 1 instance of this type
        
        public static void Init() // Making sure only 1 instance of this exists
        {
            // If the object already has an Instance, do nothing
            if (Instance == null) 
            {
                // Creating a new instance if needed
                Instance = new StatObject();                
            }
        }
        #endregion

    }

    public class StatMonster
    {
        public string MonsterName { get; set; }
        public float MonsterHP { get; set; }
        public float MonsterHPMax { get; set; }
        public MonsterCrown MonsterCrown { get; set; }
    }

    public class StatPlayer
    {
        public string PlayerName { get; set; }
        public int PlayerDps { get; set; }
        public int PlayerTotalDmg { get; set; }
    }

    public class DataObject
    {
        public long TimeStamp { get; set; }
        public List<StatMonster> Monsters { get; set; }
        public List<StatPlayer> Players { get; set; }
    }

}
