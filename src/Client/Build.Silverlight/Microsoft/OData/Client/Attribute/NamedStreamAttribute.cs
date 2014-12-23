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

namespace Microsoft.OData.Client
{
    using System;

    /// <summary>Indicates that a class that is an entity type has a related named binary stream.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class NamedStreamAttribute : Attribute
    {
        /// <summary>Creates a new instance of the <see cref="T:Microsoft.OData.Client.NamedStreamAttribute" /> class.</summary>
        /// <param name="name">The name of a binary stream that belongs to the attributed entity.</param>
        public NamedStreamAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>The name of a binary stream that belongs to the attributed entity.</summary>
        /// <returns>The name of the binary stream.</returns>
        public string Name
        {
            get;
            private set;
        }
    }
}
