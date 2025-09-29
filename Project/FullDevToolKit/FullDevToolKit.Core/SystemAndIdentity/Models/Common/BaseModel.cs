using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDevToolKit.Core.SystemAndIdentity.Models.Common
{
    public abstract class BaseModel
    {
        public long Seq { get; set; }

        public DateTime TSCreate { get; set; }

        public DateTime TSLastUpdate { get; set; }

    }
}
