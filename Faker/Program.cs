using System;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Console.WriteLine("{0} {1}", foo.x, foo.y);
            foreach (var bar in foo.z)
            {
                Console.WriteLine("{0} {1}", bar.x, bar.y);
            }
        }
    }
}