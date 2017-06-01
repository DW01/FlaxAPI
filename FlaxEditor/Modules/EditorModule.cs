﻿// Flax Engine scripting API

namespace FlaxEditor.Modules
{
    /// <summary>
    /// Base class for all Editor modules.
    /// </summary>
    public abstract class EditorModule
    {
        /// <summary>
        /// Gets the initialization order. Lower first ordering.
        /// </summary>
        /// <value>
        /// The initialization order.
        /// </value>
        public int InitOrder { get; protected set; }

        /// <summary>
        /// Gets the editor object.
        /// </summary>
        public readonly Editor Editor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorModule"/> class.
        /// </summary>
        /// <param name="editor">The editor.</param>
        protected EditorModule(Editor editor)
        {
            Editor = editor;
        }

        /// <summary>
        /// Called when Editor is startup up. Performs module initialization.
        /// </summary>
        public virtual void OnInit()
        {
        }

        /// <summary>
        /// Called when Editor is ready and winn start work.
        /// </summary>
        public virtual void OnEndInit()
        {
        }

        /// <summary>
        /// Called when every Editor update tick.
        /// </summary>
        public virtual void OnUpdate()
        {
        }

        /// <summary>
        /// Called when Editor is closing. Performs module cleanup.
        /// </summary>
        public virtual void OnExit()
        {
        }
    }
}