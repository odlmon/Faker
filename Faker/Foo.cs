namespace Faker
{
    public class Foo
    {
        public int x;
        public string y;

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