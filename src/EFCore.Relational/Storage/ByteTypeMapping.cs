// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    ///     <para>
    ///         Represents the mapping between a .NET <see cref="byte" /> type and a database type.
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public class ByteTypeMapping : RelationalTypeMapping<byte>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ByteTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="System.Data.DbType" /> to be used. </param>
        public ByteTypeMapping(
            [NotNull] string storeType,
            [CanBeNull] DbType? dbType = null)
            : base(storeType, dbType, unicode: false, size: null, hasNonDefaultUnicode: false, hasNonDefaultSize: false)
        {
        }

        /// <summary>
        ///     Creates a copy of this mapping.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="size"> The size of data the property is configured to store, or null if no size is configured. </param>
        /// <returns> The newly created mapping. </returns>
        public override RelationalTypeMapping CreateCopy(string storeType, int? size)
            => new ByteTypeMapping(storeType, DbType);
    }
}
