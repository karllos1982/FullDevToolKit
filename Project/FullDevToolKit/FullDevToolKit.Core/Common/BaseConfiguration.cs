using FullDevToolKit.Sys.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDevToolKit.Common
{
    public class BaseConfiguration
    {

        private Dictionary<string, string> _configs;

        public void Set(List<ConfigsResult> texts)
        {
            _configs = new Dictionary<string, string>();

            foreach (ConfigsResult r in texts)
            {               
                if (!_configs.ContainsKey(r.ConfigName))
                {
                    _configs.Add(r.ConfigName, r.ConfigValue);
                }
                
            }
        }

        public string Get(string name)
        {
            string ret = name;

            if (_configs != null)
            {
                if (_configs.ContainsKey(name))
                {
                    ret = _configs[name];
                }
            }

            return ret;
        }
    }
}
