using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Faker
{
    public class Faker
    {
        private readonly TypeGenerator _generator = new TypeGenerator();

        private object GenerateWithFaker(Type type)
        {
            if (_generator.CanGenerate(type))
            {
                return _generator.Generate(type);
            }

            if (IsFaker(type))
            {
                MethodInfo create = typeof(Faker).GetMethod("Create").MakeGenericMethod(type);
                return create.Invoke(this, null);    
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var listType = type.GetGenericTypeDefinition();
                    var genericType = type.GetGenericArguments()[0];
                    var constructedList = listType.MakeGenericType(genericType);
                    var random = new Random();
                    byte length = (byte) random.Next();
                    object[] parameters = {length};
                    var instance = Activator.CreateInstance(constructedList, parameters);
                    for (int i = 0; i < length; i++)
                    {
                        instance.GetType().GetMethod("Add")
                            .Invoke(instance, new [] {GenerateWithFaker(genericType)});
                    }

                    return instance;
                }
            }

            return null;
        }

        private object[] GenerateParameters(ConstructorInfo constructor)
        {
            List<object> parameters = new List<object>(
                constructor.GetParameters().Length);
            constructor.GetParameters()
                .Select(p => p.GetType())
                .ToList()
                .ForEach(t => parameters.Add(GenerateWithFaker(t)));
            
            return parameters.ToArray();
        }

        private void SetFields(object obj)
        {
            obj.GetType().GetFields().ToList()
                .ForEach(f => f.SetValue(obj, GenerateWithFaker(f.FieldType)));
        }
        
        public bool IsFaker(Type type)
        {
            return type.GetCustomAttributes(typeof(DTO), false).Length == 1;
        }
        
        public T Create<T>()
        {
            Type type = typeof(T);
            if (!IsFaker(type))
            {
                return default;
            }
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