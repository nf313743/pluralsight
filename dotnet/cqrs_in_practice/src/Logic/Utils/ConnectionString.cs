namespace Logic.Utils
{
    public sealed class CommandConnectionString
    {
        public CommandConnectionString(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    public sealed class QueriesConnectionString
    {
        public QueriesConnectionString(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}