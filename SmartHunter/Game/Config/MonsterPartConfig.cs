using System.Collections.Generic;
using System.Linq;

namespace SmartHunter.Game.Config
{
    public class MonsterPartConfig
    {
        public string StringId;
        public string[] Tags;
        public bool IsRemovable;

        public MonsterPartConfig(string stringId, IEnumerable<string> tags, bool isRemovable = false)
        {
            StringId = stringId;
            Tags = tags.ToArray();
            IsRemovable = isRemovable;
        }
    }
}
