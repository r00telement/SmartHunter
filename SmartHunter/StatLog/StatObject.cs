using Newtonsoft.Json;
using SmartHunter.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHunter.StatLog
{
    class StatObject
    {
        public string Location { get; set; }
        public List<DataObject> DataObject { get; set; } = new List<DataObject>();

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
