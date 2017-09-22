////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2017 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

namespace FlaxEngine
{
    /// <summary>
    /// Represents a part of the model actor mesh infos collection. Contains information about how to render <see cref="Mesh"/>.
    /// </summary>
    public sealed class MeshInfo
    {
        [Serialize]
        internal ModelActor _modelActor;
        [Serialize]
        internal readonly int _index;

        /// <summary>
        /// Gets the parent model actor.
        /// </summary>
        /// <value>
        /// The parent model actor.
        /// </value>
        [HideInEditor]
        public ModelActor ParentActor => _modelActor;

        /// <summary>
        /// Gets or sets the mesh local transform.
        /// </summary>
        /// <value>
        /// The lcoal transform.
        /// </value>
        [EditorOrder(40), EditorDisplay("Mesh")]
        public Transform Transform
        {
            get
            {
                Transform resultAsRef;
                ModelActor.Internal_GetMeshTransform(_modelActor.unmanagedPtr, _index, out resultAsRef);
                return resultAsRef;
            }
            set => ModelActor.Internal_SetMeshTransform(_modelActor.unmanagedPtr, _index, ref value);
        }
        
        /// <summary>
        /// Gets or sets the material used to render the mesh.
        /// If value if null then model asset mesh default material will be used as a fallback.
        /// </summary>
        /// <value>
        /// The material.
        /// </value>
        [EditorOrder(10), EditorDisplay("Mesh")]
        public MaterialBase Material
        {
            get => ModelActor.Internal_GetMeshMaterial(_modelActor.unmanagedPtr, _index);
            set => ModelActor.Internal_SetMeshMaterial(_modelActor.unmanagedPtr, _index, Object.GetUnmanagedPtr(value));
        }

        /// <summary>
        /// Gets or sets the scale in lightmap (per mesh).
        /// Final mesh scale in lightmap is alsow multiplied by <see cref="ModelActor.ScaleInLightmap"/> and global scene scale parameter.
        /// </summary>
        /// <value>
        /// The scale in lightmap.
        /// </value>
        [EditorOrder(20), EditorDisplay("Mesh"), Limit(0, 10000, 0.1f)]
        public float ScaleInLightmap
        {
            get => ModelActor.Internal_GetMeshScaleInLightmap(_modelActor.unmanagedPtr, _index);
            set => ModelActor.Internal_SetMeshScaleInLightmap(_modelActor.unmanagedPtr, _index, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MeshInfo"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        [EditorOrder(30), EditorDisplay("Mesh")]
        public bool Visible
        {
            get => ModelActor.Internal_GetMeshVisible(_modelActor.unmanagedPtr, _index);
            set => ModelActor.Internal_SetMeshVisible(_modelActor.unmanagedPtr, _index, value);
        }

        /// <summary>
        /// Gets the mesh index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [HideInEditor]
        public int Index => _index;

        internal MeshInfo()
        {
            // Used by the serialization system
        }

        internal MeshInfo(ModelActor model, int index)
        {
            _modelActor = model;
            _index = index;
        }
        
        /// <summary>
        /// Determines if there is an intersection between the model actor mesh and a ray.
        /// If mesh data is available on the CPU performs exact intersection check with the geometry.
        /// Otherwise performs simple <see cref="BoundingBox"/> vs <see cref="Ray"/> test.
        /// For more efficient collisions detection and ray casting use physics.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <param name="distance">When the method completes and returns true, contains the distance of the intersection.</param>
        /// <returns>True if the actor is intersected by the ray, otherwise false.</returns>
        public bool Intersects(Ray ray, out float distance)
        {
            return ModelActor.Internal_IntersectsMesh(_modelActor.unmanagedPtr, _index, ref ray, out distance);
        }
    }
}