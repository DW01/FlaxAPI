////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2017 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FlaxEditor.CustomEditors.Elements;
using FlaxEngine;

namespace FlaxEditor.CustomEditors.Editors
{
    /// <summary>
    /// Default implementation of the inspector used when no specified inspector is provided for the type. Inspector 
    /// displays GUI for all the inspectable fields in the object.
    /// </summary>
    public sealed class GenericEditor : CustomEditor
    {
        private readonly List<CustomEditor> children = new List<CustomEditor>();

        private struct PropertyItemInfo : IComparable
        {
            public PropertyInfo Info;
            public EditorIndexAttribute Index;
            public EditorDisplayAttribute Display;

            /// <summary>
            /// Gets the display name.
            /// </summary>
            /// <value>
            /// The display name.
            /// </value>
            public string DisplayName => Display?.Name ?? Info.Name;

            /// <summary>
            /// Gets a value indicating whether use dedicated group.
            /// </summary>
            /// <value>
            ///   <c>true</c> if use group; otherwise, <c>false</c>.
            /// </value>
            public bool UseGroup => Display?.Group != null;

            /// <inheritdoc />
            public int CompareTo(object obj)
            {
                if (obj is PropertyItemInfo other)
                {
                    // By group
                    if (Display?.Group != null)
                    {
                        if (other.Display?.Group != null)
                        {
                            return string.Compare(Display.Group, other.Display.Group, StringComparison.InvariantCulture);
                        }
                        return -1;
                    }

                    // By index
                    if (Index != null)
                    {
                        if (other.Index != null)
                            return Index.Index - other.Index.Index;
                        return -1;
                    }

                    // By name
                    return string.Compare(Info.Name, other.Info.Name, StringComparison.InvariantCulture);
                }

                return 0;
            }
        }

        /// <inheritdoc />
        public override void Initialize(LayoutElementsContainer layout)
        {
            if (Values == null)
                return;

            // TODO: for structures get all public fields
            // TODO: for objects get all public properties
            // TODO: support attribues
            // TODO: spawn custom editors for every editable thing
            // TODO; use shared properties/fields across all selected objects values

            // Faster path for the same objects selected
            if (HasDiffrentTypes == false)
            {
                var type = Values[0].GetType();

                //layout.Button("Type " + type.Name);

                if (type.IsClass)
                {
                    layout.Button("Type " + type.Name);
                    layout.Space(10);

                    // TODO: promote children to other base class like CustomEditorContainer ?

                    // Process the properties
                    var properties = type.GetProperties();
                    var propertyItems = new List<PropertyItemInfo>(properties.Length);
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var p = properties[i];

                        var attributes = p.GetCustomAttributes(true);
                        if (attributes.Any(x => x is HideInEditorAttribute))
                        {
                            continue;
                        }

                        PropertyItemInfo item;
                        item.Info = p;
                        item.Index = (EditorIndexAttribute)attributes.FirstOrDefault(x => x is EditorIndexAttribute);
                        item.Display = (EditorDisplayAttribute)attributes.FirstOrDefault(x => x is EditorDisplayAttribute);
                        
                        propertyItems.Add(item);
                    }

                    // Sort items
                    propertyItems.Sort();

                    // Add items
                    GroupElement lastGroup = null;
                    for (int i = 0; i < propertyItems.Count; i++)
                    {
                        var item = propertyItems[i];
                        
                        // Check if use group
                        if (item.UseGroup)
                        {
                            if (lastGroup == null)
                                lastGroup = layout.Group(item.Display.Group);

                            // TODO: spawn proper layout for that item
                            lastGroup.Button(item.DisplayName + " order: " + (item.Index != null ? item.Index.Index.ToString() : "?"));
                        }
                        else
                        {
                            lastGroup = null;

                            var button = layout.Button(item.DisplayName + " order: " + (item.Index != null ? item.Index.Index.ToString() : "?"));
                            button.Button.Height = 12;
                        }

                        //var pValues = new ValueContainer() { p.GetValue(Values[0]) };

                        //var child = layout.Object(pValues);
                        //children.Add(child);
                    }
                }
                else
                {
                    layout.Button("No class type: " + type.Name);
                }
            }
            else
            {
                layout.Button("More than object selected");
            }

            /*// test code for building editor layout
            layout.Button("My button");
            var group = layout.Group("Super Group");
            group.Button("Inner button 1");
            group.Space(10);
            group.Button("Inner button 2");
            group.Space(10);
            group.Button("Inner button 3");
            group.Space(10);
            group.Button("Inner button 4");
            */
        }

        /// <inheritdoc />
        public override void Refresh()
        {
            for (int i = 0; i < children.Count; i++)
                children[i].Refresh();
        }
    }
}