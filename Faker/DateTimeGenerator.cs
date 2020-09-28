using System;

namespace Faker
{
    public class DateTimeGenerator
    {
        private readonly Random _random = new Random();

        public DateTime Next()
        {
            return new DateTime(_random.Next(0, DateTime.Now.Year),
                _random.Next(1, 12),_random.Next(1, 30));
        }
        
    }
}