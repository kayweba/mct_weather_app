using StorageService;
using System.Dynamic;
using System.Net;
using System.Reflection;

namespace StorageService
{
    public class DbConfiguration
    {
        public DbConfiguration(string _host, string _user, string _password, string _dbmsName)
        {
            try
            {
                host = IPAddress.Parse(_host);
            }
            catch (FormatException)
            {
                throw new ConfigurationException("Не удается преобразовать адрес хоста. Неверный формат",
                    (int)ConfigurationErrorCode.typecastError);
            }
            user = _user;
            password = _password;
            switch (_dbmsName)
            {
                case "pgsql":
                case "PgSQL":
                    dbms = DBMS_Type.PGSQL;
                    break;
                case "sqlite":
                case "SQLite":
                    dbms = DBMS_Type.SQLITE;
                    break;
                default:
                    dbms = DBMS_Type.UNDEFINED;
                    // TODO: log wrong dbms
                    break;
            }
        }
        public IPAddress Host 
        { 
            get => host; 
        }
        public string User
        {
            get => user;
        }
        public string Password
        {
            get => password;
        }
        public DBMS_Type DBMS
        {
            get => dbms;
        }

        private IPAddress host = IPAddress.Loopback;
        private string user = "";
        private string password = "";
        private DBMS_Type dbms = DBMS_Type.UNDEFINED;
    }
}
public class ConnConfiguration
{
    public ConnConfiguration(string _host, int _port)
    {
        try
        {
            host = IPAddress.Parse(_host);
        }
        catch (FormatException)
        {
            throw new ConfigurationException("Не удается преобразовать адрес хоста. Неверный формат",
                (int)ConfigurationErrorCode.typecastError);
        }
        //TODO: parse error?
        port = _port;
    }
    public IPAddress Host
    {
        get => host;
    }
    public int Port
    {
        get => port;
    }
    private IPAddress host = IPAddress.Loopback;
    private int port = -1;
}

public class LogConfiguration
{
    public LogConfiguration(uint _level, string _path)
    {
        level = _level;
        path = _path;
    }
    public uint Level
    {
        get => level;
    }
    public string Path
    {
        get => path;
    }
    private uint level = 0;
    private string path = "";
}

public enum DBMS_Type
{
    UNDEFINED,
    PGSQL,
    SQLITE
}

public enum ConfigurationErrorCode
{
    invalidRoot,
    invalidChild,
    invalidAttribute,
    invalidValue,
    typecastError
}
