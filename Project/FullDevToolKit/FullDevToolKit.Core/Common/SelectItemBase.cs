using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDevToolKit.Core.Common
{
    public class SelectItemBase
    {
        public SelectItemBase()
        {
            
        }

        public SelectItemBase(string value, string text) 
        { 
            Value = value;
            Text = text;    
        }

        public string Value { get; set; }

        public string Text { get; set; }

    }
}
