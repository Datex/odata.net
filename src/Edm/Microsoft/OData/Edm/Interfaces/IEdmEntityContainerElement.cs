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

namespace Microsoft.OData.Edm
{
    /// <summary>
    /// Defines EDM container element types.
    /// </summary>
    public enum EdmContainerElementKind
    {
        /// <summary>
        /// Represents an element where the container kind is unknown or in error.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents an element implementing <see cref="IEdmEntitySet"/>. 
        /// </summary>
        EntitySet,

        /// <summary>
        /// Represents an element implementing <see cref="IEdmActionImport"/>.
        /// </summary>
        ActionImport,

        /// <summary>
        /// Represents an element implementing <see cref="IEdmFunctionImport"/>. 
        /// </summary>
        FunctionImport,

        /// <summary>
        /// Represents an element implementing <see cref="IEdmSingleton"/>.
        /// </summary>
        Singleton
    }

    /// <summary>
    /// Represents the common elements of all EDM entity container elements.
    /// </summary>
    public interface IEdmEntityContainerElement : IEdmNamedElement, IEdmVocabularyAnnotatable
    {
        /// <summary>
        /// Gets the kind of element of this container element.
        /// </summary>
        EdmContainerElementKind ContainerElementKind { get; }

        /// <summary>
        /// Gets the container that contains this element.
        /// </summary>
        IEdmEntityContainer Container { get; }
    }
}
