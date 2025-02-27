namespace StorageService
{
    class ConfigurationException : Exception
    {
        public ConfigurationException(string _message, int _code) : base(_message)
        {
            code = _code;
        }

        public int ErrorCode
        {
            get => code;
        }

        private int code = -1;
    }
}
