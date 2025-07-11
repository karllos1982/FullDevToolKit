using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection; 

namespace FullDevToolKit.Core.Helpers
{

    public static class ENUM_CONNECTIONSNAMES
    {
        public static string MASTERDB { get; } = "MASTERDB"; 

        public static string SLAVE01DB { get; } = "SLAVE01DB";

        public static string SLAVE02DB { get; } = "SLAVE02DB";

        public static string SLAVE03DB { get; } = "SLAVE03DB";

    }

    public class ConnectionStringItem
    {
        public string Name { get; set; }

        public string Value { get; set; }

    }

    public class ConnectionStringManager
    {
        public ConnectionStringManager()
        {
                
        }

        public ConnectionStringManager(string connections)
        {
            Connections 
                = JsonConvert.DeserializeObject<List<ConnectionStringItem>>(connections);
        }

        public List<ConnectionStringItem> Connections { get; set; }

        public void AddConnection(string name, string value)
        {
            if (Connections == null)
            {
                Connections= new List<ConnectionStringItem>();  
            }

            Connections.Add(new ConnectionStringItem()
            {
                Name = name,
                Value = value
            }); 
        }

        public ConnectionStringItem GetConnection(string name) 
        {
            ConnectionStringItem ret = Connections.Where(c=>c.Name==name).FirstOrDefault();

            return ret;
        }

    }
}
