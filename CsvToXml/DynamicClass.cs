using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvToXml
{
    public class DynamicClass
    {
        public List<string> Headers { get; set; }
        public List<Entry> Entries { get; set; }

        public class Entry
        {
            public List<string> Values { get; set; }
        }
    }
}
