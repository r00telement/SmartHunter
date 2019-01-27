namespace SmartHunter.Game.Config
{
    public class PlayerStatusEffectConditionConfig
    {
        public string Offset;
        public int Value;

        public PlayerStatusEffectConditionConfig(string offset, int value)
        {
            Offset = offset;
            Value = value;
        }
    }
}
