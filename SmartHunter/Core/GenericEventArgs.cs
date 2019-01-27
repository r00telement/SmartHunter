namespace SmartHunter.Core
{
    public class GenericEventArgs<T>
    {
        public T Value { get; private set; }

        public GenericEventArgs(T value)
        {
            Value = value;
        }
    }
}
