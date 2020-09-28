using System;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            // Bar bar = new Bar();
            // Console.WriteLine("{0}", bar.GetType().GetField("x").GetValue(bar));
            Console.WriteLine("{0} {1}", foo.x, foo.y);
        }
    }
}