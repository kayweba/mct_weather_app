using System.Xml;

namespace StorageService
{
    public class ConfigManager : IConfiguration
    {
        public ConfigManager(string? configuration = null)
        {
            if (string.IsNullOrEmpty(configuration))
                ReadConfiguration();
            else
                ReadConfiguration(configuration);
        }

        private void ReadConfiguration()
        {
            configFileName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".xml";
            try
            {
                string configuration = File.ReadAllText(configFileName);
                XmlDocument doc = ParseConfiguration(configuration);
                ReadConfigurationInternal(doc);
            }
            catch (FileNotFoundException)
            {
                LogManager.Instance().Log($"Не удалось найти файл конфигурации '{configFileName}'.", MType.Error, MSeverity.Important);
                return;
            }
            catch (UnauthorizedAccessException)
            {
                LogManager.Instance().Log($"Не удалось получить доступ к файлу конфигурации '{configFileName}'.", MType.Error, MSeverity.Important);
                return;
            }
            catch (PathTooLongException)
            {
                LogManager.Instance().Log($"Имя файла конфигурации '{configFileName}' слишком длинное.", MType.Error, MSeverity.Important);
                return;
            }
            catch (Exception)
            {
                LogManager.Instance().Log($"Не удалось прочитать файл конфигурации '{configFileName}'.", MType.Error, MSeverity.Important);
                return;
            }
        }
        private void ReadConfiguration(string configuration)
        {
            
            XmlDocument doc = ParseConfiguration(configuration);
            ReadConfigurationInternal(doc);
        }
        private XmlDocument ParseConfiguration(string configuration)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(configuration);
            }
            catch (XmlException ex)
            {
                LogManager.Instance().Log($"Не удается прочитать конфигурацю. {ex.Message}", MType.Error, MSeverity.Important);
            }
            return doc;
        }
        private void ReadConfigurationInternal(XmlDocument? doc)
        {
            if (doc is null)
            {
                LogManager.Instance().Log($"Отсутствует конфигурация. ", MType.Error, MSeverity.Important);
                return;
            }
            try
            {
                XmlElement? root = doc.DocumentElement;
                if (root is null) throw new ConfigurationException("Отсутствует корневой элемент конфигурации.",
                    (int)ConfigurationErrorCode.invalidRoot);
                #region 'database'
                XmlNodeList dbElemList = root.GetElementsByTagName("database");
                if (dbElemList.Count == 0) throw new ConfigurationException("Отсутствует элемент конфигурации \"database\".",
                    (int)ConfigurationErrorCode.invalidChild);
                if (dbElemList.Count > 1) throw new ConfigurationException("Элемент конфигурации \"database\" повторяется более одного раза.",
                    (int)ConfigurationErrorCode.invalidChild);
                XmlNode? dbElem = dbElemList.Item(0);
                if (dbElem is not null)
                {
                    string host = "", user = "", password = "", dbms = "";
                    int port = 0;
                    XmlAttributeCollection? dbAttrs = dbElem.Attributes;
                    if (dbAttrs is not null)
                    {
                        XmlAttribute? attr = dbAttrs["host"];
                        if (attr is not null) host = attr.Value;
                        else throw new ConfigurationException("Отсутствует обязательный атрибут \"host\" в элементе \"database\".",
                            (int)ConfigurationErrorCode.invalidAttribute);
                        attr = dbAttrs["user"];
                        if (attr is not null) user = attr.Value;
                        else throw new ConfigurationException("Отсутствует обязательный атрибут \"user\" в элементе \"database\".",
                            (int)ConfigurationErrorCode.invalidAttribute);
                        attr = dbAttrs["port"];
                        if (attr is not null)
                        {
                            try
                            {
                                port = Int32.Parse(attr.Value);
                            }
                            catch (FormatException)
                            {
                                throw new ConfigurationException("Не удается преобразовать значение атрибута \"port\" в элементе \"database\". Неверный формат.",
                                    (int)ConfigurationErrorCode.typecastError);
                            }
                        }
                        attr = dbAttrs["password"];
                        if (attr is not null) password = attr.Value;
                        else throw new ConfigurationException("Отсутствует обязательный атрибут \"password\" в элементе \"database\".",
                            (int)ConfigurationErrorCode.invalidAttribute);
                        attr = dbAttrs["dbms"];
                        if (attr is not null) dbms = attr.Value;
                        dbConfig = new DbConfiguration(host, user, port, password, dbms);
                    }
                }
                #endregion
                #region 'connection'
                XmlNodeList connElemList = root.GetElementsByTagName("connection");
                if (connElemList.Count == 0) throw new ConfigurationException("Отсутствует элемент конфигурации \"connection\".",
                    (int)ConfigurationErrorCode.invalidChild);
                if (connElemList.Count > 1) throw new ConfigurationException("Элемент конфигурации \"connection\" повторяется более одного раза.",
                    (int)ConfigurationErrorCode.invalidChild);
                XmlNode? connElem = connElemList.Item(0);
                if (connElem is not null)
                {
                    string host = "";
                    int port = 0;
                    XmlAttributeCollection? connAttrs = connElem.Attributes;
                    if (connAttrs is not null)
                    {
                        XmlAttribute? attr = connAttrs["host"];
                        if (attr is not null) host = attr.Value;
                        else throw new ConfigurationException("Отсутствует обязательный атрибут \"host\" в элементе \"connection\".",
                            (int)ConfigurationErrorCode.invalidAttribute);
                        attr = connAttrs["port"];
                        if (attr is not null)
                        {
                            try
                            {
                                port = Int32.Parse(attr.Value);
                            }
                            catch (FormatException)
                            {
                                throw new ConfigurationException("Не удается преобразовать значение атрибута \"port\" в элементе \"connection\". Неверный формат.",
                                    (int)ConfigurationErrorCode.typecastError);
                            }
                        }
                        else throw new ConfigurationException("Отсутствует обязательный атрибут \"port\" в элементе \"connection\".",
                            (int)ConfigurationErrorCode.invalidAttribute);
                        conConfig = new ConnConfiguration(host, port);
                    }
                }
                #endregion
                #region 'logger'
                XmlNodeList loggerElemList = root.GetElementsByTagName("logger");
                if (loggerElemList.Count == 0) throw new ConfigurationException("Отсутствует элемент конфигурации \"logger\".",
                    (int)ConfigurationErrorCode.invalidChild);
                if (loggerElemList.Count > 1) throw new ConfigurationException("Элемент конфигурации \"logger\" повторяется более одного раза.",
                    (int)ConfigurationErrorCode.invalidChild);
                XmlNode? loggerElem = loggerElemList.Item(0);
                if (loggerElem is not null)
                {
                    string path = "", destination = "silence";
                    uint level = 0;
                    XmlAttributeCollection? loggerAttrs = loggerElem.Attributes;
                    if (loggerAttrs is not null)
                    {
                        XmlAttribute? attr = loggerAttrs["path"];
                        if (attr is not null) path = attr.Value;
                        attr = loggerAttrs["destination"];
                        if (attr is not null) destination = attr.Value;
                        attr = loggerAttrs["level"];
                        if (attr is not null)
                        {
                            try
                            {
                                level = UInt32.Parse(attr.Value);
                            }
                            catch (FormatException)
                            {
                                throw new ConfigurationException("Не удается преобразовать значение атрибута \"level\" в элементе \"logger\". Неверный формат.",
                                    (int)ConfigurationErrorCode.typecastError);

                            }
                            catch (OverflowException)
                            {
                                throw new ConfigurationException("Не удается преобразовать значение атрибута \"level\" в элементе \"logger\". Неверный формат.",
                                    (int)ConfigurationErrorCode.typecastError);
                            }
                        }
                        else throw new ConfigurationException("Отсутствует обязательный атрибут \"level\" в элементе \"logger\".",
                            (int)ConfigurationErrorCode.invalidAttribute);
                        logConfig = new LogConfiguration(level, path, destination);
                    }
                }
                #endregion
            }
            // Пока логгера еще нет
            catch (XmlException ex)
            {
                LogManager.Instance().Log($"Ошибка парсинга конфигурации. {ex.Message}", MType.Error, MSeverity.Important);
            }
            catch (ConfigurationException ex)
            {
                LogManager.Instance().Log($"Ошибка конфигурации. {ex.Message}", MType.Error, MSeverity.Important);
            }
        }

        public DbConfiguration? Database 
        {
            get => dbConfig;
        }

        public ConnConfiguration? Connection
        {
            get => conConfig;
        }

        public LogConfiguration? Logger
        {
            get => logConfig;
        }

        private DbConfiguration? dbConfig;
        private LogConfiguration? logConfig;
        private ConnConfiguration? conConfig;
        private string configFileName = "";
    }
}
