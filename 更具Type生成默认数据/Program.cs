using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace 更具Type生成默认数据
{
    class Program
    {
        static void Main(string[] args)
        {
            var build = new DefaultValueBuilder();
            var data = build.Build(typeof(TestDto));
            Console.WriteLine(JsonConvert.SerializeObject(data,Formatting.Indented));
            Console.ReadKey();
        }
    }
}
