using System;
using System.Collections.Generic;

namespace Faker
{
    public class TypeGenerator
    {
        private List<IGenerator> generators = new List<IGenerator>();

        public TypeGenerator()
        {
            generators.Add(new BaseTypeGenerator());
            generators.Add(new DateTimeGenerator());
        }

        public object Generate(Type type)
        {
            foreach (var generator in generators)
            {
                if (generator.IsGeneratable(type))
                {
                    return generator.Next(type);
                }
            }

            return null;
        }
    }
}