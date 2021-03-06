// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    ///     <para>
    ///         Represents the mapping between a .NET <see cref="byte" /> array type and a database type.
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public class ByteArrayTypeMapping : RelationalTypeMapping<byte[]>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ByteArrayTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="System.Data.DbType" /> to be used. </param>
        /// <param name="size"> The size of data the property is configured to store, or null if no size is configured. </param>
        /// <param name="hasNonDefaultSize"> A value indicating whether the size setting has been manually configured to a non-default value. </param>
        public ByteArrayTypeMapping(
            [NotNull] string storeType,
            [CanBeNull] DbType? dbType = null,
            int? size = null,
            bool hasNonDefaultSize = false)
            : base(storeType, dbType, false, size, false, hasNonDefaultSize)
        {
        }

        /// <summary>
        ///     Creates a copy of this mapping.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="size"> The size of data the property is configured to store, or null if no size is configured. </param>
        /// <returns> The newly created mapping. </returns>
        public override RelationalTypeMapping CreateCopy(string storeType, int? size)
            => new ByteArrayTypeMapping(
                storeType,
                DbType,
                size,
                hasNonDefaultSize: size != Size);

        /// <summary>
        ///     Generates the SQL representation of a literal value.
        /// </summary>
        /// <param name="value">The literal value.</param>
        /// <returns>
        ///     The generated string.
        /// </returns>
        protected override string GenerateNonNullSqlLiteral(object value)
        {
            return GenerateByteArraySqlLiteral((byte[])value);
        }

        /// <summary>
        ///     Generates the SQL representation of a byte[] literal value.
        /// </summary>
        /// <param name="value">The literal value.</param>
        /// <returns>
        ///     The generated string.
        /// </returns>
        public static string GenerateByteArraySqlLiteral([NotNull]byte[] value)
        {
            Check.NotNull(value, nameof(value));

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("X'");

            foreach (var @byte in value)
            {
                stringBuilder.Append(@byte.ToString("X2", CultureInfo.InvariantCulture));
            }

            stringBuilder.Append("'");
            return stringBuilder.ToString();
        }
    }
}
