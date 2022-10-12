using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeLib
{
    public class XmlConfig
    {
       public List<ItemConfig> items = new List<ItemConfig>();
    }

    public class ItemConfig
    {
        public string processName = "";
        public int left = 0;
        public int top = 0;
        public int width = 800;
        public int height = 600;
    }
}
