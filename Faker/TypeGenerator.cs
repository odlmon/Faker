using PluginBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Faker
{
    public class TypeGenerator
    {
        private List<IGenerator> generators;

        public TypeGenerator()
        {
            string[] pluginPaths = 
            {
                @"BaseTypeGenerator\bin\Debug\netcoreapp3.1\BaseTypeGenerator.dll",
                @"DateTimeGenerator\bin\Debug\netcoreapp3.1\DateTimeGenerator.dll"
            };

            generators = pluginPaths.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreateGenerators(pluginAssembly);
            }).ToList();
            
        }

        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, 
                relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(
                new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        static IEnumerable<IGenerator> CreateGenerators(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IGenerator).IsAssignableFrom(type))
                {
                    IGenerator result = Activator.CreateInstance(type) as IGenerator;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IGenerator in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
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

        public bool CanGenerate(Type type)
        {
            foreach (var generator in generators)
            {
                if (generator.IsGeneratable(type))
                {
                    return true;
                }
            }

            return false;
        }
    }
}