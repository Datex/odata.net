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

namespace Microsoft.OData.Core
{
    #region Namespaces

    using System;
    using System.Diagnostics;
    using Microsoft.OData.Core.Metadata;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Edm.Library;
    #endregion Namespaces

    /// <summary>
    /// Class with utility methods for writing OData content.
    /// </summary>
    internal static class WriterUtils
    {
        /// <summary>
        /// Determines if a property should be written or skipped.
        /// </summary>
        /// <param name="projectedProperties">The projected properties annotation to use (can be null).</param>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>true if the property should be skipped, false to write the property.</returns>
        internal static bool ShouldSkipProperty(this ProjectedPropertiesAnnotation projectedProperties, string propertyName)
        {
            if (projectedProperties == null)
            {
                return false;
            }
            else if (object.ReferenceEquals(ProjectedPropertiesAnnotation.EmptyProjectedPropertiesInstance, projectedProperties))
            {
                return true;
            }
            else if (object.ReferenceEquals(ProjectedPropertiesAnnotation.AllProjectedPropertiesInstance, projectedProperties))
            {
                return false;
            }

            return !projectedProperties.IsPropertyProjected(propertyName);
        }

        /// <summary>
        /// Remove the Edm. prefix from the type name if it is primitive type.
        /// </summary>
        /// <param name="typeName">The type name to remove the Edm. prefix</param>
        /// <returns>The type name without the Edm. Prefix</returns>
        internal static string RemoveEdmPrefixFromTypeName(string typeName)
        {
            if (!string.IsNullOrEmpty(typeName))
            {
                string itemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(typeName);
                if (itemTypeName == null)
                {
                    IEdmSchemaType primitiveType = EdmLibraryExtensions.ResolvePrimitiveTypeName(typeName);
                    if (primitiveType != null)
                    {
                        return primitiveType.ShortQualifiedName();
                    }
                }
                else
                {
                    IEdmSchemaType primitiveType = EdmLibraryExtensions.ResolvePrimitiveTypeName(itemTypeName);
                    if (primitiveType != null)
                    {
                        return EdmLibraryExtensions.GetCollectionTypeName(primitiveType.ShortQualifiedName());
                    }
                }
            }

            return typeName;
        }
    }
}
