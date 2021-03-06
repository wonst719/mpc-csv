﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable SA1649 // File name should match first type name

namespace MessagePackCompiler.CodeAnalysis
{
    public interface IResolverRegisterInfo
    {
        string FullName { get; }

        string FormatterName { get; }
    }

    public class CsvResolverRegisterInfo
    {
        public string FullName { get; set; }

        public string ResolverName { get; set; }

        public string SerializerNamespace { get; set; }
    }

    public class ObjectSerializationInfo : IResolverRegisterInfo
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Namespace { get; set; }

        public bool IsClass { get; set; }

        public bool IsStruct
        {
            get { return !this.IsClass; }
        }

        public MemberSerializationInfo[] ConstructorParameters { get; set; }

        public MemberSerializationInfo[] Members { get; set; }

        public bool HasIMessagePackSerializationCallbackReceiver { get; set; }

        public bool NeedsCastOnBefore { get; set; }

        public bool NeedsCastOnAfter { get; set; }

        public string FormatterName => (this.Namespace == null ? this.Name : this.Namespace + "." + this.Name) + "Formatter";

        public int WriteCount
        {
            get
            {
                return this.Members.Count(x => x.IsReadable);
            }
        }

        public int MaxKey
        {
            get
            {
                return this.Members.Where(x => x.IsReadable).Select(x => x.IntKey).DefaultIfEmpty(-1).Max();
            }
        }

        public MemberSerializationInfo GetMember(int index)
        {
            return this.Members.FirstOrDefault(x => x.IntKey == index);
        }

        public string GetConstructorString()
        {
            var args = string.Join(", ", this.ConstructorParameters.Select(x => "__" + x.Name + "__"));
            return $"{this.FullName}({args})";
        }
    }

    public class MemberSerializationInfo
    {
        public bool IsProperty { get; set; }

        public bool IsField { get; set; }

        public bool IsWritable { get; set; }

        public bool IsReadable { get; set; }

        public int IntKey { get; set; }

        public string StringKey { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string ColumnName { get; set; }

        public string ShortTypeName { get; set; }

        public string CustomFormatterTypeName { get; set; }

        private readonly HashSet<string> primitiveTypes = new HashSet<string>(new string[]
        {
            "short",
            "int",
            "long",
            "ushort",
            "uint",
            "ulong",
            "float",
            "double",
            "bool",
            "byte",
            "sbyte",
            "char",
            ////"global::System.DateTime",
            ////"byte[]",
            ////"string",
        });

        private readonly HashSet<string> primitiveIntegerTypes = new HashSet<string>(new string[]
        {
            "short",
            "int",
            "long",
            "ushort",
            "uint",
            "ulong",
            "byte",
            "sbyte",
        });

        public string GetSerializeMethodString()
        {
            if (CustomFormatterTypeName != null)
            {
                return $"this.__{this.Name}CustomFormatter__.Serialize(ref writer, value.{this.Name}, options)";
            }
            else if (this.primitiveTypes.Contains(this.Type))
            {
                return $"writer.Write(value.{this.Name})";
            }
            else
            {
                return $"formatterResolver.GetFormatterWithVerify<{this.Type}>().Serialize(ref writer, value.{this.Name}, options)";
            }
        }

        public string GetDeserializeMethodString()
        {
            // TODO: 더 깔끔하게 만들자.
            if (CustomFormatterTypeName != null)
            {
                return $"({this.Type})this.__{this.Name}CustomFormatter__.ConvertFrom(StringUtil.MakeString(value))";
            }
            else if (this.primitiveIntegerTypes.Contains(this.Type))
            {
                return $"({this.Type})Strtol.strtol(value.Array, value.Offset, value.Count, 10)";
            }
            else if (this.Type == "bool")
            {
                return "StringUtil.ParseBool(value)";
            }
            else if (this.Type == "string")
            {
                return "StringUtil.MakeString(value)";
            }
            else if (this.Type == "float")
            {
                return "Foundation.Serialization.Csv.Utf8Json.Internal.DoubleConversion.StringToDoubleConverter.ToSingle(value.Array, value.Offset, out _)";
            }
            else if (this.Type == "double")
            {
                return "Foundation.Serialization.Csv.Utf8Json.Internal.DoubleConversion.StringToDoubleConverter.ToDouble(value.Array, value.Offset, out _)";
            }
            else
            {
                return "throw new NotSupportedException()";
            }

            //if (CustomFormatterTypeName != null)
            //{
            //    return $"this.__{this.Name}CustomFormatter__.Deserialize(ref reader, options)";
            //}
            //else if (this.primitiveTypes.Contains(this.Type))
            //{
            //    return $"reader.Read{this.ShortTypeName.Replace("[]", "s")}()";
            //}
            //else
            //{
            //    return $"formatterResolver.GetFormatterWithVerify<{this.Type}>().Deserialize(ref reader, options)";
            //}
        }
    }

    public class EnumSerializationInfo : IResolverRegisterInfo
    {
        public string Namespace { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string UnderlyingType { get; set; }

        public string FormatterName => (this.Namespace == null ? this.Name : this.Namespace + "." + this.Name) + "Formatter";
    }

    public class GenericSerializationInfo : IResolverRegisterInfo, IEquatable<GenericSerializationInfo>
    {
        public string FullName { get; set; }

        public string FormatterName { get; set; }

        public bool Equals(GenericSerializationInfo other)
        {
            return this.FullName.Equals(other.FullName);
        }

        public override int GetHashCode()
        {
            return this.FullName.GetHashCode();
        }
    }
}
