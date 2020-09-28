using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Faker
{
    public class Faker
    {
        private readonly TypeGenerator _generator = new TypeGenerator();

        private object[] GenerateParameters(ConstructorInfo constructor)
        {
            List<object> parameters = new List<object>(
                constructor.GetParameters().Length);
            constructor.GetParameters()
                .Select(p => p.GetType())
                .ToList()
                .ForEach(t => parameters.Add(_generator.Generate(t)));
            
            return parameters.ToArray();
        }

        private void SetFields(object obj)
        {
            obj.GetType().GetFields().ToList()
                .ForEach(f => f.SetValue(obj, _generator.Generate(f.FieldType)));
        }
        
        public T Create<T>()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();

            Type[] types = fields.Select(x => x.GetType()).ToArray();
            ConstructorInfo constructor = type.GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
                null, types,null);

            if (constructor == null)
            {
                constructor = type.GetConstructors(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)[0];
            }

            object[] parameters = GenerateParameters(constructor);
            object obj = constructor.Invoke(parameters);

            SetFields(obj);
            
            return (T) obj;
        }
    }
}