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
    
    #line 1 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class CsvSerializerTemplate : CsvSerializerTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("// <auto-generated>\r\n// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). " +
                    "DO NOT MODIFY.\r\n// </auto-generated>\r\nnamespace ");
            
            #line 10 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    using System;\r\n    using System.Collections.Generic;\r\n    using Foundati" +
                    "on.Serialization.Csv;\r\n    using Foundation.Serialization.Csv.Internal;\r\n\r\n");
            
            #line 17 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 foreach(var objInfo in ObjectSerializationInfos) { 
            
            #line default
            #line hidden
            this.Write("    public sealed class ");
            
            #line 18 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            
            #line default
            #line hidden
            this.Write("Serializer\r\n    {\r\n        List<string> headerColumns = new List<string>();\r\n    " +
                    "    List<");
            
            #line 21 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            
            #line default
            #line hidden
            this.Write("> rows = new List<");
            
            #line 21 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            
            #line default
            #line hidden
            this.Write(">(32);\r\n        ");
            
            #line 22 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            
            #line default
            #line hidden
            this.Write(" currentInst = null;\r\n\r\n");
            
            #line 24 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 foreach(var item in objInfo.Members) { 
            
            #line default
            #line hidden
            
            #line 25 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 if(item.CustomFormatterTypeName != null) { 
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 26 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.CustomFormatterTypeName));
            
            #line default
            #line hidden
            this.Write(" __");
            
            #line 26 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            
            #line default
            #line hidden
            this.Write("CustomFormatter__ = new ");
            
            #line 26 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.CustomFormatterTypeName));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 27 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 28 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"
        void OnValueEnd(ArraySegment<byte> value, int row, int col)
        {
            if (row == 1)
            {
                headerColumns.Add(StringUtil.MakeString(value));
            }
            else
            {
                if (col == 1)
                {
                    currentInst = new ");
            
            #line 40 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            
            #line default
            #line hidden
            this.Write("();\r\n                }\r\n\r\n                var columnName = headerColumns[col - 1]" +
                    ";\r\n\r\n                switch (columnName)\r\n                {\r\n");
            
            #line 47 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 foreach(var x in objInfo.Members) { 
            
            #line default
            #line hidden
            this.Write("                    case \"");
            
            #line 48 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(x.StringKey));
            
            #line default
            #line hidden
            this.Write("\": // ");
            
            #line 48 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            
            #line default
            #line hidden
            this.Write("\r\n                        {\r\n                            var __val = ");
            
            #line 50 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(x.GetDeserializeMethodString()));
            
            #line default
            #line hidden
            this.Write(";\r\n                            currentInst.");
            
            #line 51 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));
            
            #line default
            #line hidden
            this.Write(" = __val;\r\n                        }\r\n                        break;\r\n");
            
            #line 54 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"                }
            }
        }
        
        void OnEntryEnd(int row)
        {
            if (row > 1)
            {
                rows.Add(currentInst);
                currentInst = null;
            }
        }

        public List<");
            
            #line 68 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));
            
            #line default
            #line hidden
            this.Write(@"> Deserialize(byte[] data)
        {
            var parser = new CsvSerializer.Parser();
            parser.OnValueEnd = OnValueEnd;
            parser.OnEntryEnd = OnEntryEnd;

            parser.Parse(data, 0, data.Length);

            return rows;
        }
    }
");
            
            #line 79 "D:\Projects\mpc-csv\src\MessagePack.GeneratorCore\Generator\CsvSerializerTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class CsvSerializerTemplateBase
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
