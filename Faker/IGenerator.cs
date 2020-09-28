using System;

namespace Faker
{
    public interface IGenerator
    {
        object Next(Type type);
        bool IsGeneratable(Type type);
    }
}