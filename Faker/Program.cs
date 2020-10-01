using System;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Console.WriteLine("{0} {1} {2} {3} {4}", foo.x, foo.y, foo.z.x, foo.z.foo, foo.Num);
            // foreach (var bar in foo.z)
            // {
                // Console.WriteLine("{0} {1}", bar.x, bar.y);
            // }
        }
    }
}