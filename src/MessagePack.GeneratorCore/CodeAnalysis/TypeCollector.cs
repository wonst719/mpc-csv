// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable SA1649 // File name should match first type name

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace MessagePackCompiler.CodeAnalysis
{
    public class MessagePackGeneratorResolveFailedException : Exception
    {
        public MessagePackGeneratorResolveFailedException(string message)
            : base(message)
        {
        }
    }

    internal class ReferenceSymbols
    {
#pragma warning disable SA1401 // Fields should be private
        internal readonly INamedTypeSymbol Task;
        internal readonly INamedTypeSymbol TaskOfT;
        //internal readonly INamedTypeSymbol MessagePackObjectAttribute;
        //internal readonly INamedTypeSymbol SerializationConstructorAttribute;
        //internal readonly INamedTypeSymbol KeyAttribute;
        //internal readonly INamedTypeSymbol IgnoreAttribute;
        //internal readonly INamedTypeSymbol IgnoreDataMemberAttribute;
        //internal readonly INamedTypeSymbol IMessagePackSerializationCallbackReceiver;
        //internal readonly INamedTypeSymbol MessagePackFormatterAttribute;

        internal readonly INamedTypeSymbol CsvObjectAttribute;
        internal readonly INamedTypeSymbol CsvIgnoreAttribute;
        internal readonly INamedTypeSymbol SerializationConstructorAttribute;
        internal readonly INamedTypeSymbol IgnoreDataMemberAttribute;
#pragma warning restore SA1401 // Fields should be private

        public ReferenceSymbols(Compilation compilation, Action<string> logger)
        {
            TaskOfT = compilation.GetTypeByMetadataName("System.Threading.Tasks.Task`1");
            if (TaskOfT == null)
            {
                logger("failed to get metadata of System.Threading.Tasks.Task`1");
            }

            Task = compilation.GetTypeByMetadataName("System.Threading.Tasks.Task");
            if (Task == null)
            {
                logger("failed to get metadata of System.Threading.Tasks.Task");
            }

#if false
            MessagePackObjectAttribute = compilation.GetTypeByMetadataName("MessagePack.MessagePackObjectAttribute");
            if (MessagePackObjectAttribute == null)
            {
                throw new InvalidOperationException("failed to get metadata of MessagePack.MessagePackObjectAttribute");
            }

            SerializationConstructorAttribute = compilation.GetTypeByMetadataName("MessagePack.SerializationConstructorAttribute");
            if (SerializationConstructorAttribute == null)
            {
                throw new InvalidOperationException("failed to get metadata of MessagePack.SerializationConstructorAttribute");
            }

            KeyAttribute = compilation.GetTypeByMetadataName("MessagePack.KeyAttribute");
            if (KeyAttribute == null)
            {
                throw new InvalidOperationException("failed to get metadata of MessagePack.KeyAttribute");
            }

            IgnoreAttribute = compilation.GetTypeByMetadataName("MessagePack.IgnoreMemberAttribute");
            if (IgnoreAttribute == null)
            {
                throw new InvalidOperationException("failed to get metadata of MessagePack.IgnoreMemberAttribute");
            }

            IgnoreDataMemberAttribute = compilation.GetTypeByMetadataName("System.Runtime.Serialization.IgnoreDataMemberAttribute");
            if (IgnoreDataMemberAttribute == null)
            {
                logger("failed to get metadata of System.Runtime.Serialization.IgnoreDataMemberAttribute");
            }

            IMessagePackSerializationCallbackReceiver = compilation.GetTypeByMetadataName("MessagePack.IMessagePackSerializationCallbackReceiver");
            if (IMessagePackSerializationCallbackReceiver == null)
            {
                throw new InvalidOperationException("failed to get metadata of MessagePack.IMessagePackSerializationCallbackReceiver");
            }

            MessagePackFormatterAttribute = compilation.GetTypeByMetadataName("MessagePack.MessagePackFormatterAttribute");
            if (IMessagePackSerializationCallbackReceiver == null)
            {
                throw new InvalidOperationException("failed to get metadata of MessagePack.MessagePackFormatterAttribute");
            }
#endif
            CsvObjectAttribute = compilation.GetTypeByMetadataName("Foundation.Serialization.Csv.CsvObjectAttribute");
            if (CsvObjectAttribute == null)
            {
                throw new InvalidOperationException("failed to get metadata of Foundation.Serialization.Csv.CsvObjectAttribute");
            }

            CsvIgnoreAttribute = compilation.GetTypeByMetadataName("Foundation.Serialization.Csv.CsvIgnoreAttribute");
            if (CsvIgnoreAttribute == null)
            {
                throw new InvalidOperationException("failed to get metadata of Foundation.Serialization.Csv.CsvIgnoreAttribute");
            }

            SerializationConstructorAttribute = compilation.GetTypeByMetadataName("Foundation.Serialization.Csv.SerializationConstructorAttribute");
            if (SerializationConstructorAttribute == null)
            {
                throw new InvalidOperationException("failed to get metadata of Foundation.Serialization.Csv.SerializationConstructorAttribute");
            }

            IgnoreDataMemberAttribute = compilation.GetTypeByMetadataName("System.Runtime.Serialization.IgnoreDataMemberAttribute");
            if (IgnoreDataMemberAttribute == null)
            {
                logger("failed to get metadata of System.Runtime.Serialization.IgnoreDataMemberAttribute");
            }
        }
    }

    public class TypeCollector
    {
        private const string CodegeneratorOnlyPreprocessorSymbol = "INCLUDE_ONLY_CODE_GENERATION";

        private static readonly SymbolDisplayFormat BinaryWriteFormat = new SymbolDisplayFormat(
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                miscellaneousOptions: SymbolDisplayMiscellaneousOptions.ExpandNullable,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly);

        private static readonly SymbolDisplayFormat ShortTypeNameFormat = new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes);

        private readonly bool isForceUseMap;
        private readonly ReferenceSymbols typeReferences;
        private readonly INamedTypeSymbol[] targetTypes;
        private readonly HashSet<string> embeddedTypes = new HashSet<string>(new string[]
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
            "decimal",
            "char",
            "string",
            "object",
            "System.Guid",
            "System.TimeSpan",
            "System.DateTime",
            "System.DateTimeOffset",

            //"MessagePack.Nil",

            // and arrays
            "short[]",
            "int[]",
            "long[]",
            "ushort[]",
            "uint[]",
            "ulong[]",
            "float[]",
            "double[]",
            "bool[]",
            "byte[]",
            "sbyte[]",
            "decimal[]",
            "char[]",
            "string[]",
            "System.DateTime[]",
            "System.ArraySegment<byte>",
            "System.ArraySegment<byte>?",

            // extensions
            "UnityEngine.Vector2",
            "UnityEngine.Vector3",
            "UnityEngine.Vector4",
            "UnityEngine.Quaternion",
            "UnityEngine.Color",
            "UnityEngine.Bounds",
            "UnityEngine.Rect",
            "UnityEngine.AnimationCurve",
            "UnityEngine.RectOffset",
            "UnityEngine.Gradient",
            "UnityEngine.WrapMode",
            "UnityEngine.GradientMode",
            "UnityEngine.Keyframe",
            "UnityEngine.Matrix4x4",
            "UnityEngine.GradientColorKey",
            "UnityEngine.GradientAlphaKey",
            "UnityEngine.Color32",
            "UnityEngine.LayerMask",
            "UnityEngine.Vector2Int",
            "UnityEngine.Vector3Int",
            "UnityEngine.RangeInt",
            "UnityEngine.RectInt",
            "UnityEngine.BoundsInt",

            "System.Reactive.Unit",
        });

        private readonly Action<string> logger;

        private readonly bool disallowInternal;

        // visitor workspace:
        private HashSet<ITypeSymbol> alreadyCollected;
        private List<ObjectSerializationInfo> collectedObjectInfo;
        private List<EnumSerializationInfo> collectedEnumInfo;
        private List<GenericSerializationInfo> collectedGenericInfo;

        public TypeCollector(Compilation compilation, bool disallowInternal, bool isForceUseMap, Action<string> logger)
        {
            this.logger = logger;
            this.typeReferences = new ReferenceSymbols(compilation, logger);
            this.disallowInternal = disallowInternal;
            this.isForceUseMap = isForceUseMap;

            targetTypes = compilation.GetNamedTypeSymbols()
                .Where(x =>
                {
                    if (x.DeclaredAccessibility == Accessibility.Public)
                    {
                        return true;
                    }

                    if (!disallowInternal)
                    {
                        return x.DeclaredAccessibility == Accessibility.Friend;
                    }

                    return false;
                })
                .Where(x =>
                    ((x.TypeKind == TypeKind.Class) && x.GetAttributes().Any(x2 => x2.AttributeClass.ApproximatelyEqual(typeReferences.CsvObjectAttribute)))
                    || ((x.TypeKind == TypeKind.Struct) && x.GetAttributes().Any(x2 => x2.AttributeClass.ApproximatelyEqual(typeReferences.CsvObjectAttribute))))
                .ToArray();
        }

        private void ResetWorkspace()
        {
            this.alreadyCollected = new HashSet<ITypeSymbol>();
            this.collectedObjectInfo = new List<ObjectSerializationInfo>();
            this.collectedEnumInfo = new List<EnumSerializationInfo>();
            this.collectedGenericInfo = new List<GenericSerializationInfo>();
        }

        // EntryPoint
        public (ObjectSerializationInfo[] objectInfo, EnumSerializationInfo[] enumInfo, GenericSerializationInfo[] genericInfo) Collect()
        {
            this.ResetWorkspace();

            foreach (INamedTypeSymbol item in this.targetTypes)
            {
                this.CollectCore(item);
            }

            return (
                this.collectedObjectInfo.OrderBy(x => x.FullName).ToArray(),
                this.collectedEnumInfo.OrderBy(x => x.FullName).ToArray(),
                this.collectedGenericInfo.Distinct().OrderBy(x => x.FullName).ToArray()
                );
        }

        // Gate of recursive collect
        private void CollectCore(ITypeSymbol typeSymbol)
        {
            if (!this.alreadyCollected.Add(typeSymbol))
            {
                return;
            }

            if (this.embeddedTypes.Contains(typeSymbol.ToString()))
            {
                return;
            }

            if (typeSymbol.TypeKind == TypeKind.Array)
            {
                this.CollectArray(typeSymbol as IArrayTypeSymbol);
                return;
            }

            if (!this.IsAllowAccessibility(typeSymbol))
            {
                return;
            }

            var type = typeSymbol as INamedTypeSymbol;

            if (typeSymbol.TypeKind == TypeKind.Enum)
            {
                this.CollectEnum(type);
                return;
            }

            if (type.Locations[0].IsInMetadata)
            {
                return;
            }

            this.CollectObject(type);
            return;
        }

        private void CollectEnum(INamedTypeSymbol type)
        {
            var info = new EnumSerializationInfo
            {
                Name = type.Name,
                Namespace = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToDisplayString(),
                FullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                UnderlyingType = type.EnumUnderlyingType.ToDisplayString(BinaryWriteFormat),
            };

            this.collectedEnumInfo.Add(info);
        }

        private void CollectArray(IArrayTypeSymbol array)
        {
            ITypeSymbol elemType = array.ElementType;
            this.CollectCore(elemType);

            var info = new GenericSerializationInfo
            {
                FullName = array.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            };

            if (array.IsSZArray)
            {
                info.FormatterName = $"global::MessagePack.Formatters.ArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else if (array.Rank == 2)
            {
                info.FormatterName = $"global::MessagePack.Formatters.TwoDimensionalArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else if (array.Rank == 3)
            {
                info.FormatterName = $"global::MessagePack.Formatters.ThreeDimensionalArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else if (array.Rank == 4)
            {
                info.FormatterName = $"global::MessagePack.Formatters.FourDimensionalArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else
            {
                throw new InvalidOperationException("does not supports array dimension, " + info.FullName);
            }

            this.collectedGenericInfo.Add(info);

            return;
        }

        private void CollectObject(INamedTypeSymbol type)
        {
            var isClass = !type.IsValueType;

            AttributeData contractAttr = type.GetAttributes().FirstOrDefault(x => x.AttributeClass.ApproximatelyEqual(this.typeReferences.CsvObjectAttribute));
            if (contractAttr == null)
            {
                throw new MessagePackGeneratorResolveFailedException("Serialization Object must mark MessagePackObjectAttribute." + " type: " + type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            }

            var intMembers = new Dictionary<int, MemberSerializationInfo>();
            var stringMembers = new Dictionary<string, MemberSerializationInfo>();

            // All public members are serialize target except [Ignore] member.
            var hiddenIntKey = 0;

            foreach (IPropertySymbol item in type.GetAllMembers().OfType<IPropertySymbol>().Where(x => !x.IsOverride))
            {
                if (item.GetAttributes().Any(x => x.AttributeClass.ApproximatelyEqual(this.typeReferences.CsvIgnoreAttribute) || x.AttributeClass.Name == this.typeReferences.IgnoreDataMemberAttribute.Name))
                {
                    continue;
                }

                //var customFormatterAttr = item.GetAttributes().FirstOrDefault(x => x.AttributeClass.ApproximatelyEqual(this.typeReferences.MessagePackFormatterAttribute))?.ConstructorArguments[0].Value as INamedTypeSymbol;

                var member = new MemberSerializationInfo
                {
                    IsReadable = (item.GetMethod != null) && item.GetMethod.DeclaredAccessibility == Accessibility.Public && !item.IsStatic,
                    IsWritable = (item.SetMethod != null) && item.SetMethod.DeclaredAccessibility == Accessibility.Public && !item.IsStatic,
                    StringKey = item.Name,
                    IsProperty = true,
                    IsField = false,
                    Name = item.Name,
                    Type = item.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    ShortTypeName = item.Type.ToDisplayString(BinaryWriteFormat),
                    //CustomFormatterTypeName = customFormatterAttr?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                };
                if (!member.IsReadable && !member.IsWritable)
                {
                    continue;
                }

                member.IntKey = hiddenIntKey++;
                stringMembers.Add(member.StringKey, member);

                this.CollectCore(item.Type); // recursive collect
            }

            foreach (IFieldSymbol item in type.GetAllMembers().OfType<IFieldSymbol>())
            {
                if (item.GetAttributes().Any(x => x.AttributeClass.ApproximatelyEqual(this.typeReferences.CsvIgnoreAttribute) || x.AttributeClass.Name == this.typeReferences.IgnoreDataMemberAttribute.Name))
                {
                    continue;
                }

                if (item.IsImplicitlyDeclared)
                {
                    continue;
                }

                //var customFormatterAttr = item.GetAttributes().FirstOrDefault(x => x.AttributeClass.ApproximatelyEqual(this.typeReferences.MessagePackFormatterAttribute))?.ConstructorArguments[0].Value as INamedTypeSymbol;

                var member = new MemberSerializationInfo
                {
                    IsReadable = item.DeclaredAccessibility == Accessibility.Public && !item.IsStatic,
                    IsWritable = item.DeclaredAccessibility == Accessibility.Public && !item.IsReadOnly && !item.IsStatic,
                    StringKey = item.Name,
                    IsProperty = false,
                    IsField = true,
                    Name = item.Name,
                    Type = item.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    ShortTypeName = item.Type.ToDisplayString(BinaryWriteFormat),
                    //CustomFormatterTypeName = customFormatterAttr?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                };
                if (!member.IsReadable && !member.IsWritable)
                {
                    continue;
                }

                member.IntKey = hiddenIntKey++;
                stringMembers.Add(member.StringKey, member);
                this.CollectCore(item.Type); // recursive collect
            }

            // GetConstructor
            IEnumerator<IMethodSymbol> ctorEnumerator = null;
            IMethodSymbol ctor = type.Constructors.Where(x => x.DeclaredAccessibility == Accessibility.Public).SingleOrDefault(x => x.GetAttributes().Any(y => y.AttributeClass.ApproximatelyEqual(this.typeReferences.SerializationConstructorAttribute)));
            if (ctor == null)
            {
                ctorEnumerator = type.Constructors.Where(x => x.DeclaredAccessibility == Accessibility.Public).OrderByDescending(x => x.Parameters.Length).GetEnumerator();

                if (ctorEnumerator.MoveNext())
                {
                    ctor = ctorEnumerator.Current;
                }
            }

            // struct allows null ctor
            if (ctor == null && isClass)
            {
                throw new MessagePackGeneratorResolveFailedException("can't find public constructor. type:" + type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            }

            var constructorParameters = new List<MemberSerializationInfo>();
            if (ctor != null)
            {
                ILookup<string, KeyValuePair<string, MemberSerializationInfo>> constructorLookupDictionary = stringMembers.ToLookup(x => x.Key, x => x, StringComparer.OrdinalIgnoreCase);
                do
                {
                    constructorParameters.Clear();
                    var ctorParamIndex = 0;
                    foreach (IParameterSymbol item in ctor.Parameters)
                    {
                        MemberSerializationInfo paramMember;

                        IEnumerable<KeyValuePair<string, MemberSerializationInfo>> hasKey = constructorLookupDictionary[item.Name];
                        var len = hasKey.Count();
                        if (len != 0)
                        {
                            if (len != 1)
                            {
                                if (ctorEnumerator != null)
                                {
                                    ctor = null;
                                    continue;
                                }
                                else
                                {
                                    throw new MessagePackGeneratorResolveFailedException("duplicate matched constructor parameter name:" + type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) + " parameterName:" + item.Name + " paramterType:" + item.Type.Name);
                                }
                            }

                            paramMember = hasKey.First().Value;
                            if (item.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == paramMember.Type && paramMember.IsReadable)
                            {
                                constructorParameters.Add(paramMember);
                            }
                            else
                            {
                                if (ctorEnumerator != null)
                                {
                                    ctor = null;
                                    continue;
                                }
                                else
                                {
                                    throw new MessagePackGeneratorResolveFailedException("can't find matched constructor parameter, parameterType mismatch. type:" + type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) + " parameterName:" + item.Name + " paramterType:" + item.Type.Name);
                                }
                            }
                        }
                        else
                        {
                            if (ctorEnumerator != null)
                            {
                                ctor = null;
                                continue;
                            }
                            else
                            {
                                throw new MessagePackGeneratorResolveFailedException("can't find matched constructor parameter, index not found. type:" + type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) + " parameterName:" + item.Name);
                            }
                        }

                        ctorParamIndex++;
                    }
                }
                while (TryGetNextConstructor(ctorEnumerator, ref ctor));

                if (ctor == null)
                {
                    throw new MessagePackGeneratorResolveFailedException("can't find matched constructor. type:" + type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
                }
            }

            //var hasIMessagePackSerializationCallbackReceiver = type.AllInterfaces.Any(x => x.ApproximatelyEqual(this.typeReferences.IMessagePackSerializationCallbackReceiver));
            //var needsCastOnBefore = true;
            //var needsCastOnAfter = true;
            //if (hasIMessagePackSerializationCallbackReceiver)
            //{
            //    needsCastOnBefore = !type.GetMembers("OnBeforeSerialize").Any();
            //    needsCastOnAfter = !type.GetMembers("OnAfterDeserialize").Any();
            //}

            var info = new ObjectSerializationInfo
            {
                IsClass = isClass,
                ConstructorParameters = constructorParameters.ToArray(),
                Members = stringMembers.Values.ToArray(),
                Name = type.ToDisplayString(ShortTypeNameFormat).Replace(".", "_"),
                FullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Namespace = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToDisplayString(),
                //HasIMessagePackSerializationCallbackReceiver = hasIMessagePackSerializationCallbackReceiver,
                //NeedsCastOnAfter = needsCastOnAfter,
                //NeedsCastOnBefore = needsCastOnBefore,
            };
            this.collectedObjectInfo.Add(info);
        }

        private static bool TryGetNextConstructor(IEnumerator<IMethodSymbol> ctorEnumerator, ref IMethodSymbol ctor)
        {
            if (ctorEnumerator == null || ctor != null)
            {
                return false;
            }

            if (ctorEnumerator.MoveNext())
            {
                ctor = ctorEnumerator.Current;
                return true;
            }
            else
            {
                ctor = null;
                return false;
            }
        }

        private bool IsAllowAccessibility(ITypeSymbol symbol)
        {
            do
            {
                if (symbol.DeclaredAccessibility != Accessibility.Public)
                {
                    if (this.disallowInternal)
                    {
                        return false;
                    }

                    if (symbol.DeclaredAccessibility != Accessibility.Internal)
                    {
                        return true;
                    }
                }

                symbol = symbol.ContainingType;
            }
            while (symbol != null);

            return true;
        }
    }
}
