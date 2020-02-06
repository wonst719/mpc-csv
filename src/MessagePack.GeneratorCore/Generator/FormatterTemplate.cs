﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전: 16.0.0.0
//  
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace MessagePackCompiler.Generator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class FormatterTemplate : FormatterTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write(@"
// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            this.Write("\n{\n    using System;\n    using System.Buffers;\n    using MessagePack;\n");
 foreach(var objInfo in ObjectSerializationInfos) { 
            this.Write("\n\n    public sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            this.Write("Formatter : global::MessagePack.Formatters.IMessagePackFormatter<");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));
            this.Write(">\n    {\n");
 foreach(var item in objInfo.Members) { 
            this.Write("\n");
 if(item.CustomFormatterTypeName != null) { 
            this.Write("\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.CustomFormatterTypeName));
            this.Write(" __");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("CustomFormatter__ = new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.CustomFormatterTypeName));
            this.Write("();\n");
 } 
            this.Write("\n");
 } 
            this.Write("\n\n        private readonly global::MessagePack.Internal.AutomataDictionary ____ke" +
                    "yMapping;\n        private readonly byte[][] ____stringByteKeys;\n\n        public " +
                    "");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            this.Write("Formatter()\n        {\n            this.____keyMapping = new global::MessagePack.I" +
                    "nternal.AutomataDictionary()\n            {\n");
 foreach(var x in objInfo.Members) { 
            this.Write("\n                { \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.StringKey));
            this.Write("\", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.IntKey));
            this.Write(" },\n");
 } 
            this.Write("\n            };\n\n            this.____stringByteKeys = new byte[][]\n            {" +
                    "\n");
 foreach(var x in objInfo.Members.Where(x => x.IsReadable)) { 
            this.Write("\n                global::MessagePack.Internal.CodeGenHelpers.GetEncodedStringByte" +
                    "s(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.StringKey));
            this.Write("\"),\n");
 } 
            this.Write("\n            };\n        }\n\n        public void Serialize(ref MessagePackWriter wr" +
                    "iter, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));
            this.Write(" value, global::MessagePack.MessagePackSerializerOptions options)\n        {\n");
 if( objInfo.IsClass) { 
            this.Write("\n            if (value == null)\n            {\n                writer.WriteNil();\n" +
                    "                return;\n            }\n\n");
 } 
            this.Write("\n            IFormatterResolver formatterResolver = options.Resolver;\n");
if(objInfo.HasIMessagePackSerializationCallbackReceiver && objInfo.NeedsCastOnBefore) { 
            this.Write("\n            ((IMessagePackSerializationCallbackReceiver)value).OnBeforeSerialize" +
                    "();\n");
 } else if(objInfo.HasIMessagePackSerializationCallbackReceiver) { 
            this.Write("\n            value.OnBeforeSerialize();\n");
 } 
            this.Write("\n            writer.WriteMapHeader(");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.WriteCount));
            this.Write(");\n");
 var index = 0; foreach(var x in objInfo.Members) { 
            this.Write("\n            writer.WriteRaw(this.____stringByteKeys[");
            this.Write(this.ToStringHelper.ToStringWithCulture(index++));
            this.Write("]);\n            ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.GetSerializeMethodString()));
            this.Write(";\n");
 } 
            this.Write("\n        }\n\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));
            this.Write(" Deserialize(ref MessagePackReader reader, global::MessagePack.MessagePackSeriali" +
                    "zerOptions options)\n        {\n            if (reader.TryReadNil())\n            {" +
                    "\n");
 if( objInfo.IsClass) { 
            this.Write("\n                return null;\n");
 } else { 
            this.Write("\n                throw new InvalidOperationException(\"typecode is null, struct no" +
                    "t supported\");\n");
 } 
            this.Write("\n            }\n\n            options.Security.DepthStep(ref reader);\n            I" +
                    "FormatterResolver formatterResolver = options.Resolver;\n            var length =" +
                    " reader.ReadMapHeader();\n");
 foreach(var x in objInfo.Members) { 
            this.Write("\n            var __");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write("__ = default(");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Type));
            this.Write(");\n");
 } 
            this.Write(@"

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                int key;
                if (!this.____keyMapping.TryGetValue(stringKey, out key))
                {
                    reader.Skip();
                    continue;
                }

                switch (key)
                {
");
 foreach(var x in objInfo.Members) { 
            this.Write("\n                    case ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.IntKey));
            this.Write(":\n                        __");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write("__ = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.GetDeserializeMethodString()));
            this.Write(";\n                        break;\n");
 } 
            this.Write("\n                    default:\n                        reader.Skip();\n            " +
                    "            break;\n                }\n            }\n\n            var ____result =" +
                    " new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.GetConstructorString()));
            this.Write(";\n");
 foreach(var x in objInfo.Members.Where(x => x.IsWritable)) { 
            this.Write("\n            ____result.");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write(" = __");
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            this.Write("__;\n");
 } 
            this.Write("\n");
if(objInfo.HasIMessagePackSerializationCallbackReceiver && objInfo.NeedsCastOnAfter) { 
            this.Write("\n            ((IMessagePackSerializationCallbackReceiver)____result).OnAfterDeser" +
                    "ialize();\n");
 } else if(objInfo.HasIMessagePackSerializationCallbackReceiver) { 
            this.Write("\n            ____result.OnAfterDeserialize();\n");
 } 
            this.Write("\n            reader.Depth--;\n            return ____result;\n        }\n    }\n");
 } 
            this.Write(@"
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1200 // Using directives should be placed correctly
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name
");
            return this.GenerationEnvironment.ToString();
        }
    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class FormatterTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
