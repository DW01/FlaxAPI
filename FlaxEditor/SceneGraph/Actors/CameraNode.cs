////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2018 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using FlaxEngine;

namespace FlaxEditor.SceneGraph.Actors
{
    /// <summary>
    /// Scene tree node for <see cref="Camera"/> actor type.
    /// </summary>
    /// <seealso cref="ActorNode" />
    public sealed class CameraNode : ActorNode
    {
        /// <inheritdoc />
        public CameraNode(Actor actor)
            : base(actor)
        {
        }

        /// <inheritdoc />
        public override bool RayCastSelf(ref Ray ray, out float distance)
        {
            return Camera.Internal_IntersectsItselfEditor(Object.GetUnmanagedPtr(_actor), ref ray, out distance);
        }
    }
}
