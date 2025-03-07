using StorageService;
using System.Xml.Linq;

namespace StorageService.Tests
{
    public class ConfigTests
    {
        [Fact]
        public void TestCorrectConfig()
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("configuration");
            XElement database = new XElement("database");
            XAttribute dbAttr = new XAttribute("host", "127.0.0.1");
            database.Add(dbAttr);
            dbAttr = new XAttribute("port", "5433");
            database.Add(dbAttr);
            dbAttr = new XAttribute("user", "admin");
            database.Add(dbAttr);
            dbAttr = new XAttribute("password", "root");
            database.Add(dbAttr);
            dbAttr = new XAttribute("dbms", "root");
            database.Add(dbAttr);
            root.Add(database);

            XElement connection = new XElement("connection");
            XAttribute conAttr = new XAttribute("host", "127.0.0.1");
            connection.Add(conAttr);
            conAttr = new XAttribute("port", "5117");
            connection.Add(conAttr);
            root.Add(connection);

            XElement logger = new XElement("logger");
            XAttribute logAttr = new XAttribute("level", "3");
            logger.Add(logAttr);
            logAttr = new XAttribute("path", "");
            logger.Add(logAttr);
            logAttr = new XAttribute("destination", "console");
            logger.Add(logAttr);
            root.Add(logger);

            doc.Add(root);
            Assert.NotEmpty(doc.ToString());
            ConfigManager configManager = new ConfigManager(doc.ToString());
            Assert.NotNull(configManager.Logger);
            Assert.NotNull(configManager.Connection);
            Assert.NotNull(configManager.Database);
        }
        [Fact]
        public void DbConfigCreation()
        {
            DbConfiguration dbConfig = new DbConfiguration("255.255.255.255", "AdminQA", 5555, "AdminQA", "pgsql");
            Assert.NotNull(dbConfig);
            Assert.Equal("255.255.255.255", dbConfig.Host.ToString());
            Assert.Equal("AdminQA", dbConfig.User);
            Assert.Equal(5555, dbConfig.Port);
            Assert.Equal("AdminQA", dbConfig.Password);
            Assert.Equal(DBMS_Type.PGSQL, dbConfig.DBMS);
        }
        [Fact]
        public void ConnectionConfiguration()
        {
            ConnConfiguration connConfig = new ConnConfiguration("255.255.255.255", 1010);
            Assert.NotNull(connConfig);
            Assert.Equal("255.255.255.255", connConfig.Host.ToString());
            Assert.Equal(1010, connConfig.Port);
        }
    }
}
