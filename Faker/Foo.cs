using System;
using System.Collections.Generic;

namespace Faker
{
    [DTO]
    public class Foo
    {
        public int x;
        public string y;
        // public List<Bar> z;
        public Bar z;
        public DateTime Num { get; set; }

        public Foo(int x)
        {
            this.x = x;
        }

        private Foo()
        {
            x = 1;
            y = "1";
        }
    }
}