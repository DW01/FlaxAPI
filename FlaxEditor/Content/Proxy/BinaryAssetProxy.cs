////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2018 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System;

namespace FlaxEditor.Content
{
    /// <summary>
    /// Base class for all binary asset proxy objects used to manage <see cref="BinaryAssetItem"/>.
    /// </summary>
    /// <seealso cref="FlaxEditor.Content.AssetProxy" />
    public abstract class BinaryAssetProxy : AssetProxy
    {
        /// <summary>
        /// The binary asset files extension.
        /// </summary>
        public static readonly string Extension = "flax";
        
        /// <inheritdoc />
        public override bool IsProxyFor(ContentItem item)
        {
            return item is BinaryAssetItem binaryAssetItem && TypeName == binaryAssetItem.TypeName;
        }

        /// <inheritdoc />
        public override string FileExtension => Extension;

        /// <inheritdoc />
        public override AssetItem ConstructItem(string path, string typeName, ref Guid id)
        {
            return new BinaryAssetItem(path, id, typeName, Domain);
        }
    }
}
