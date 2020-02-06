// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using MessagePack.GeneratorCore.Utils;
using Microsoft.CodeAnalysis.CSharp;

namespace MessagePackCompiler
{
    public class CsvCompilation
    {
        public static Task<CSharpCompilation> CreateFromProjectAsync(string[] csprojs, string[] preprocessorSymbols, CancellationToken cancellationToken)
        {
            return PseudoCompilation.CreateFromProjectAsync(csprojs, preprocessorSymbols, cancellationToken);
        }

        public static Task<CSharpCompilation> CreateFromDirectoryAsync(string directoryRoot, string[] preprocessorSymbols, CancellationToken cancellationToken)
        {
            return PseudoCompilation.CreateFromDirectoryAsync(directoryRoot, preprocessorSymbols, DummyAnnotation, cancellationToken);
        }

        private const string DummyAnnotation = @"
using System;

namespace Foundation.Serialization.Csv
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class CsvObjectAttribute : Attribute
    {
        public bool KeyAsPropertyName { get; private set; }

        public MessagePackObjectAttribute(bool keyAsPropertyName = false)
        {
            this.KeyAsPropertyName = keyAsPropertyName;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CsvIgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
    public class SerializationConstructorAttribute : Attribute
    {
    }

    // dummy
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MessagePackFormatterAttribute : Attribute
    {
        public Type FormatterType { get; private set; }
        public object[] Arguments { get; private set; }

        public MessagePackFormatterAttribute(Type formatterType)
        {
            this.FormatterType = formatterType;
        }

        public MessagePackFormatterAttribute(Type formatterType, params object[] arguments)
        {
            this.FormatterType = formatterType;
            this.Arguments = arguments;
        }
    }
}

";
    }
}
