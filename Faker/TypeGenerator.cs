using System;

namespace Faker
{
    public class TypeGenerator
    {
        //possible made a list which can be updated
        private readonly BaseTypeGenerator _baseTypeGenerator = new BaseTypeGenerator();
        private readonly DateTimeGenerator _dateTimeGenerator = new DateTimeGenerator();
        
        private bool IsBaseType(Type type)
        {
            if (type == typeof(byte) || type == typeof(sbyte) || type == typeof(short)
                || type == typeof(ushort) || type == typeof(int) || type == typeof(uint)
                || type == typeof(long) || type == typeof(ulong) || type == typeof(float)
                || type == typeof(double) || type == typeof(decimal) || type == typeof(char)
                || type == typeof(string) || type == typeof(bool))
            {
                return true;
            }
            
            return false;
        }

        public object Generate(Type type)
        {
            //possible create a self checking interface
            if (IsBaseType(type))
            {
                return _baseTypeGenerator.Next(type);
            }
            if (type == typeof(DateTime))
            {
                return _dateTimeGenerator.Next();
            }

            return null;
        }
    }
}