using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 更具Type生成默认数据
{
    public class TestDto
    {
        public int Age { get; set; }

        public string Name { get;set; }

        public TestDto1 TestDto1 { get; set; }
        public TestDto2 TestDto2 { get; set; }
    }

    public class TestDto1
    {
        public int Age { get; set; }

        public string Name { get;set; }

    }

    public class TestDto2
    {
        public int Age { get; set; }

        public string Name { get;set; }
    }
}
