namespace StorageService
{
    class BasicException : Exception
    {
        public BasicException(string _message, int _code) : base(_message)
        {
            code = _code;
        }

        public int ErrorCode
        {
            get => code;
        }

        private int code = -1;

    }
    class ConfigurationException : BasicException
    {
        public ConfigurationException(string _message, int code) : base(_message, code) { }
    }
    class LogException : BasicException
    {
        public LogException(string _message, int code) : base(_message, code) { }
    }
    class DatabaseException : BasicException
    {
        public DatabaseException(string _message, int _code) : base(_message, _code) {}
    }
}
