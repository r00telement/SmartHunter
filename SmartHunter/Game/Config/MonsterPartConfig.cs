namespace SmartHunter.Game.Config
{
    public class MonsterPartConfig
    {
        public string StringId;
        public bool IsRemovable;
        public string[] Tags;

        public MonsterPartConfig(string stringId, bool isRemovable, string[] tags)
        {
            StringId = stringId;
            IsRemovable = isRemovable;
            Tags = tags;
        }
    }
}
