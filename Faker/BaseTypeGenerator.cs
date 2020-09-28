﻿using System;

namespace Faker
{
    public class BaseTypeGenerator
    {
        private readonly Random _random = new Random();

        private long GetLong()
        {
            byte[] buf = new byte[8];
            _random.NextBytes(buf);
            return BitConverter.ToInt64(buf, 0);
        }

        private string GetString()
        {
            string str = "";
            byte count = (byte) _random.Next();
            for (int i = 0; i < count; i++)
            {
                str += (char) _random.Next('A', 'z');
            }

            return str;
        }
        
        public object Next(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean: return _random.Next(0, 2) > 0;
                
                case TypeCode.Byte: return (byte) _random.Next();
                    
                case TypeCode.SByte: return (sbyte) _random.Next();
                    
                case TypeCode.Int16: return (short) _random.Next();
                    
                case TypeCode.UInt16: return (ushort) _random.Next();
                    
                case TypeCode.Int32: return _random.Next();
                
                case TypeCode.UInt32: return (uint) _random.Next();
                    
                case TypeCode.Int64: return GetLong();
                    
                case TypeCode.UInt64: return (ulong) GetLong();

                case TypeCode.Single: return (float) _random.NextDouble();
                    
                case TypeCode.Double: return _random.NextDouble();
                    
                case TypeCode.Decimal: return new decimal(_random.NextDouble());
                    
                case TypeCode.Char: return (char) _random.Next('A', 'z');
                    
                case TypeCode.String: return GetString();
                
                default: return null;
            }
        }
    }
}