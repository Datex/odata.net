//   OData .NET Libraries ver. 6.9
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 
//   MIT License
//   Permission is hereby granted, free of charge, to any person obtaining a copy of
//   this software and associated documentation files (the "Software"), to deal in
//   the Software without restriction, including without limitation the rights to use,
//   copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the
//   Software, and to permit persons to whom the Software is furnished to do so,
//   subject to the following conditions:

//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.

//   THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//   FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//   COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//   CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Microsoft.OData.Core.Json
{
    #region Namespaces
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Collections;
    using Microsoft.OData.Core.Metadata;
    using Microsoft.OData.Edm.Library;
    using ODataErrorStrings = Microsoft.OData.Core.Strings;
    using ODataPlatformHelper = Microsoft.OData.Core.PlatformHelper;
    #endregion Namespaces

    /// <summary>
    /// Extension methods for the JSON writer.
    /// </summary>
    internal static class JsonWriterExtensions
    {
        /// <summary>
        /// Writes the json object value to the <paramref name="jsonWriter"/>.
        /// </summary>
        /// <param name="jsonWriter">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="jsonObjectValue">Writes the given json object value to the underlying json writer.</param>
        /// <param name="injectPropertyAction">Called when the top-level object is started to possibly inject first property into the object.</param>
        internal static void WriteJsonObjectValue(this IJsonWriter jsonWriter, IDictionary<string, object> jsonObjectValue, Action<IJsonWriter> injectPropertyAction)
        {
            Debug.Assert(jsonWriter != null, "jsonWriter != null");
            Debug.Assert(jsonObjectValue != null, "jsonObjectValue != null");

            jsonWriter.StartObjectScope();

            if (injectPropertyAction != null)
            {
                injectPropertyAction(jsonWriter);
            }

            foreach (KeyValuePair<string, object> property in jsonObjectValue)
            {
                jsonWriter.WriteName(property.Key);
                jsonWriter.WriteJsonValue(property.Value);
            }

            jsonWriter.EndObjectScope();
        }

        /// <summary>
        /// Writes a primitive value.
        /// </summary>
        /// <param name="jsonWriter">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value to write.</param>
        internal static void WritePrimitiveValue(this IJsonWriter jsonWriter, object value)
        {
            TypeCode typeCode = ODataPlatformHelper.GetTypeCode(value.GetType());
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    jsonWriter.WriteValue((bool)value);
                    break;

                case TypeCode.Byte:
                    jsonWriter.WriteValue((byte)value);
                    break;

                case TypeCode.Decimal:
                    jsonWriter.WriteValue((decimal)value);
                    break;

                case TypeCode.Double:
                    jsonWriter.WriteValue((double)value);
                    break;

                case TypeCode.Int16:
                    jsonWriter.WriteValue((Int16)value);
                    break;

                case TypeCode.Int32:
                    jsonWriter.WriteValue((Int32)value);
                    break;

                case TypeCode.Int64:
                    jsonWriter.WriteValue((Int64)value);
                    break;

                case TypeCode.SByte:
                    jsonWriter.WriteValue((sbyte)value);
                    break;

                case TypeCode.Single:
                    jsonWriter.WriteValue((Single)value);
                    break;

                case TypeCode.String:
                    jsonWriter.WriteValue((string)value);
                    break;

                default:
                    {
                        byte[] valueAsByteArray = value as byte[];
                        if (valueAsByteArray != null)
                        {
                            jsonWriter.WriteValue(valueAsByteArray);
                            break;
                        }

                        if (value is DateTimeOffset)
                        {
                            jsonWriter.WriteValue((DateTimeOffset)value);
                            break;
                        }

                        if (value is Guid)
                        {
                            jsonWriter.WriteValue((Guid)value);
                            break;
                        }

                        if (value is TimeSpan)
                        {
                            jsonWriter.WriteValue((TimeSpan)value);
                            break;
                        }

                        if (value is Date)
                        {
                            jsonWriter.WriteValue((Date)value);
                            break;
                        }

                        if (value is TimeOfDay)
                        {
                            jsonWriter.WriteValue((TimeOfDay)value);
                            break;
                        }
                    }

                    throw new ODataException(ODataErrorStrings.ODataJsonWriter_UnsupportedValueType(value.GetType().FullName));
            }
        }

        /// <summary>
        /// Writes the json array value.
        /// </summary>
        /// <param name="jsonWriter">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="arrayValue">Writes the json array value to the underlying json writer.</param>
        private static void WriteJsonArrayValue(this IJsonWriter jsonWriter, IEnumerable arrayValue)
        {
            Debug.Assert(arrayValue != null, "arrayValue != null");

            jsonWriter.StartArrayScope();

            foreach (object element in arrayValue)
            {
                jsonWriter.WriteJsonValue(element);
            }

            jsonWriter.EndArrayScope();
        }

        /// <summary>
        /// Writes the json value (primitive, IDictionary or IEnumerable) to the underlying json writer.
        /// </summary>
        /// <param name="jsonWriter">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="propertyValue">value to write.</param>
        private static void WriteJsonValue(this IJsonWriter jsonWriter, object propertyValue)
        {
            if (propertyValue == null)
            {
                jsonWriter.WriteValue((string)null);
            }
            else if (EdmLibraryExtensions.IsPrimitiveType(propertyValue.GetType()))
            {
                jsonWriter.WritePrimitiveValue(propertyValue);
            }
            else
            {
                IDictionary<string, object> objectValue = propertyValue as IDictionary<string, object>;
                if (objectValue != null)
                {
                    jsonWriter.WriteJsonObjectValue(objectValue, null /*typeName */);
                }
                else
                {
                    IEnumerable arrayValue = propertyValue as IEnumerable;
                    Debug.Assert(arrayValue != null, "arrayValue != null");
                    jsonWriter.WriteJsonArrayValue(arrayValue);
                }
            }
        }
    }
}
