using System;
using System.Collections.Generic;
using System.Text;

namespace FullDevToolKit.Core.Helpers
{
   
    public class ContextConnectionSwitcher
    {
        private Dictionary<string, ConnectionStringItem> Connections 
                = new Dictionary<string, ConnectionStringItem>();

        public ContextConnectionSwitcher()
        {

        }

        public void AddDefault(ConnectionStringItem conn)
        {
            Connections.Add("DEFAULT", conn);

        }

        public void AddConnection(string name, ConnectionStringItem conn)
        {
            Connections.Add(name, conn);
        }

        public ConnectionStringItem GetConnection(string name)
        {
            if (Connections.ContainsKey(name))
            {
                return Connections[name];
            }
            else
            {
                return null;
            }
        }

    }
   
}
