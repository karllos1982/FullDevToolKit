using FullDevToolKit.Sys.Domains;
using FullDevToolKit.Sys.Models.Common; 
using System.Collections.Generic;

namespace MyApp.ViewModel
{
   

    public class BaseLocalization: LocalizationServiceBase
    {
        
        private Dictionary<string, string> _texts;

        public void Set(List<LocalizationTextResult> texts, string lang)
        {
            _texts = new Dictionary<string, string>();
          
            foreach (LocalizationTextResult r in texts)
            {                
                if (r.Language == lang) {
                   
                    if (!_texts.ContainsKey(r.Name))
                    {
                        _texts.Add(r.Name, r.Text);
                    }                    
                }                
            }
        }

        public string Get(string name)
        {
            string ret = name;

            if (_texts != null)
            {
                if (_texts.ContainsKey(name))
                {
                    ret = _texts[name];
                }
            }
            
            return ret;
        }

        
    }
}
