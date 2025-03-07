using StorageService;
using System.Net;
namespace StorageService
{
    public class DbConfiguration
    {
        public DbConfiguration(string _host, string _user, int _port, string _password, string _dbmsName)
        {
            try
            {
                host = IPAddress.Parse(_host);
                if (string.IsNullOrEmpty(host.ToString()) || _port <= 0)
                    throw new ConfigurationException("Адрес или порт БД указаны неправильно. Адрес должен быть не пустым, " +
                        "порт должен быть больше либо равен 0.",
                        (int)ConfigurationErrorCode.invalidValue);
            }
            catch (FormatException)
            {
                throw new ConfigurationException("Не удается преобразовать адрес БД. Неверный формат",
                    (int)ConfigurationErrorCode.typecastError);
            }
            user = _user;
            password = _password;
            _dbmsName = _dbmsName.ToLower();
            port = _port;
            if (!string.IsNullOrEmpty(_dbmsName))
            {
                switch (_dbmsName)
                {
                    case "pgsql":
                        dbms = DBMS_Type.PGSQL;
                        break;
                    case "sqlite":
                        dbms = DBMS_Type.SQLITE;
                        break;
                    default:
                        dbms = DBMS_Type.UNDEFINED;
                        // TODO: log wrong dbms
                        break;
                }
            }
            else
                dbms = DBMS_Type.SQLITE;
        }
        public IPAddress Host 
        { 
            get => host; 
        }
        public string User
        {
            get => user;
        }
        public int Port
        {
            get => port;
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
        private int port = 5433;
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
            if (string.IsNullOrEmpty(host.ToString()) || _port <= 0)
                throw new ConfigurationException("Адрес или порт хоста указаны неправильно. Адрес должен быть не пустым, " +
                    "порт должен быть больше либо равен 0.",
                    (int)ConfigurationErrorCode.invalidValue);
        }
        catch (FormatException)
        {
            throw new ConfigurationException("Не удается преобразовать адрес хоста. Неверный формат",
                (int)ConfigurationErrorCode.typecastError);
        }
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
    private int port = 80;
}

public class LogConfiguration
{
    public LogConfiguration() : this(3, "", "") {}
    public LogConfiguration(uint _level, string _path, string _destination)
    {
        level = _level;
        if (level > 3) level = 3;
        path = _path;
        // Определяем куда писать логи
        _destination = _destination.ToLower();
        if (string.IsNullOrEmpty(_destination)) // Если строка с назначением не пустая
            return;
        dest.Clear();
        string[] dests = _destination.Split(','); // Разделяем переданные значения
        // Обрезаем все пробелы
        for (int i = 0; i <  dests.Length; i++)
            dests[i] = dests[i].Trim();
        // Если в качестве назначения было передано silence, то логирования не будет,
        // все остальные значения игнорируем
        if (dests.Contains("silence"))
        {
            dest.Add(LOG_Destination.SILENCE);
            return;
        }
        else
        {
            // Если передали что-то кроме SILENCE
            if (dests.Contains("console")) dest.Add(LOG_Destination.CONSOLE);
            if (dests.Contains("file")) dest.Add(LOG_Destination.FILE);
            // Если передано что-то другое, то игнориуем и пишем только в лог по умолчанию
            if (dest.Count == 0)
            {
                dest.Add(LOG_Destination.CONSOLE);
                return;
            }
        }

    }
    public uint Level
    {
        get => level;
    }
    public string Path
    {
        get => path;
    }
    public List<LOG_Destination> Destination
    {
        get => dest;
    }
    private uint level = 0;
    private string path = "";
    private List<LOG_Destination> dest = new List<LOG_Destination> { LOG_Destination.CONSOLE }; // По умолчанию пишем в консоль
}

public enum DBMS_Type
{
    UNDEFINED,
    PGSQL,
    SQLITE
}

public enum LOG_Destination
{
    SILENCE = 0,
    FILE = 2,
    CONSOLE = 4
}

public enum ConfigurationErrorCode
{
    invalidRoot,
    invalidChild,
    invalidAttribute,
    invalidValue,
    typecastError
}
public enum LogErrorCode
{
    dirNotFound,
    ioFailed,
    ioForbidden
}
public enum DbErrorCode
{
    incorrectValue
}
