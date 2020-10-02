using PluginBase;
using System;

namespace DateTimeGenerator
{
    public class DateTimeGenerator: IGenerator
    {
        private readonly Random _random = new Random();

        public object Next(Type type)
        {
            return new DateTime(_random.Next(0, DateTime.Now.Year),
                _random.Next(1, 12),_random.Next(1, 30));
        }

        public bool IsGeneratable(Type type)
        {
            return type == typeof(DateTime);
        }
    }
}