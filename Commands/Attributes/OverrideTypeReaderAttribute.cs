using System;
using System.Reflection;

namespace TwitchBot.Commands
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class OverrideTypeReaderAttribute : Attribute
    {
        private static readonly TypeInfo _typeReaderTypeInfo = typeof(TypeReader).GetTypeInfo();

        public Type TypeReader { get; }

        public OverrideTypeReaderAttribute(Type overridenTypeReader)
        {
            if (!_typeReaderTypeInfo.IsAssignableFrom(overridenTypeReader.GetTypeInfo()))
                throw new ArgumentException($"{nameof(overridenTypeReader)} must inherit from {nameof(TypeReader)}");
            
            TypeReader = overridenTypeReader;
        }
    } 
}
