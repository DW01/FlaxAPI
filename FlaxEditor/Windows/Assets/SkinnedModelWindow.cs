////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2018 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using FlaxEditor.Content;
using FlaxEditor.Content.Import;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEditor.CustomEditors.Elements;
using FlaxEditor.GUI;
using FlaxEditor.Viewport.Cameras;
using FlaxEditor.Viewport.Previews;
using FlaxEngine;
using FlaxEngine.GUI;

namespace FlaxEditor.Windows.Assets
{
	/// <summary>
	/// Editor window to view/modify <see cref="SkinnedModel"/> asset.
	/// </summary>
	/// <seealso cref="SkinnedModel" />
	/// <seealso cref="FlaxEditor.Windows.Assets.AssetEditorWindow" />
	public sealed class SkinnedModelWindow : AssetEditorWindowBase<SkinnedModel>
	{
		// TODO: debug model UVs channel
		// TODO: adding/removing material slots
		// TODO: refresh material slots comboboxes on material slot rename
		// TODO: add button to draw model/bone bounds

		/// <summary>
		/// The model properties proxy object.
		/// </summary>
		[CustomEditor(typeof(ProxyEditor))]
		private sealed class PropertiesProxy
		{
			[EditorOrder(10), EditorDisplay("Materials", EditorDisplayAttribute.InlineStyle), MemberCollection(CanReorderItems = true, NotNullItems = true, ReadOnly = true)]
			public MaterialSlot[] MaterialSlots
			{
				get => Asset?.MaterialSlots;
				set
				{
					if (Asset != null)
						Asset.MaterialSlots = value;
				}
			}

			[EditorOrder(20), CustomEditor(typeof(MeshessEditor))]
			public PropertiesProxy Meshes
			{
				get => this;
				set { }
			}

			private SkinnedModelWindow Window;
			private SkinnedModel Asset;
			private ModelImportSettings ImportSettings = new ModelImportSettings();

			[HideInEditor]
			public int IsolateIndex = -1;

			[HideInEditor]
			public int HighlightIndex = -1;

			private bool _skipEffectsGuiEvents;
			private readonly List<ComboBox> _materialSlotComboBoxes = new List<ComboBox>();
			private readonly List<CheckBox> _isolateCheckBoxes = new List<CheckBox>();
			private readonly List<CheckBox> _highlightCheckBoxes = new List<CheckBox>();

			public void OnLoad(SkinnedModelWindow window)
			{
				// Link
				Window = window;
				Asset = window.Asset;
				IsolateIndex = -1;
				HighlightIndex = -1;
				Window.UpdateEffectsOnAsset();

				// Try to restore target asset import options (usefull for fast reimport)
				ModelImportSettings.TryRestore(ref ImportSettings, window.Item.Path);
			}

			public void OnClean()
			{
				// Unlink
				Window = null;
				Asset = null;
				IsolateIndex = -1;
				HighlightIndex = -1;
			}

			public void Reimport()
			{
				Editor.Instance.ContentImporting.Reimport((BinaryAssetItem)Window.Item, ImportSettings);
			}

			/// <summary>
			/// Updates the highlight/isolate effects on UI.
			/// </summary>
			public void UpdateEffectsOnUI()
			{
				_skipEffectsGuiEvents = true;

				for (int i = 0; i < _isolateCheckBoxes.Count; i++)
				{
					var checkBox = _isolateCheckBoxes[i];
					checkBox.Checked = IsolateIndex == ((SkinnedMesh)checkBox.Tag).MaterialSlotIndex;
				}

				for (int i = 0; i < _highlightCheckBoxes.Count; i++)
				{
					var checkBox = _highlightCheckBoxes[i];
					checkBox.Checked = HighlightIndex == ((SkinnedMesh)checkBox.Tag).MaterialSlotIndex;
				}

				_skipEffectsGuiEvents = false;
			}

			/// <summary>
			/// Updates the material slots UI parts. Should be called after material slot rename.
			/// </summary>
			public void UpdateMaterialSlotsUI()
			{
				_skipEffectsGuiEvents = true;

				// Generate material slots labels (with index prefix)
				var slots = Asset.MaterialSlots;
				var slotsLabels = new string[slots.Length];
				for (int i = 0; i < slots.Length; i++)
				{
					slotsLabels[i] = string.Format("[{0}] {1}", i, slots[i].Name);
				}

				// Update comboboxes
				for (int i = 0; i < _materialSlotComboBoxes.Count; i++)
				{
					var comboBox = _materialSlotComboBoxes[i];
					comboBox.SetItems(slotsLabels);
					comboBox.SelectedIndex = ((SkinnedMesh)comboBox.Tag).MaterialSlotIndex;
				}

				_skipEffectsGuiEvents = false;
			}

			/// <summary>
			/// Sets the material slot index to the mesh.
			/// </summary>
			/// <param name="mesh">The mesh.</param>
			/// <param name="newSlotIndex">New index of the material slot to use.</param>
			public void SetMaterialSlot(SkinnedMesh mesh, int newSlotIndex)
			{
				if (_skipEffectsGuiEvents)
					return;

				mesh.MaterialSlotIndex = newSlotIndex == -1 ? 0 : newSlotIndex;
				Window.UpdateEffectsOnAsset();
				UpdateEffectsOnUI();
				Window.MarkAsEdited();
			}

			/// <summary>
			/// Sets the material slot to isolate.
			/// </summary>
			/// <param name="mesh">The mesh.</param>
			public void SetIsolate(SkinnedMesh mesh)
			{
				if (_skipEffectsGuiEvents)
					return;

				IsolateIndex = mesh?.MaterialSlotIndex ?? -1;
				Window.UpdateEffectsOnAsset();
				UpdateEffectsOnUI();
			}

			/// <summary>
			/// Sets the material slot index to highlight.
			/// </summary>
			/// <param name="mesh">The mesh.</param>
			public void SetHighlight(SkinnedMesh mesh)
			{
				if (_skipEffectsGuiEvents)
					return;

				HighlightIndex = mesh?.MaterialSlotIndex ?? -1;
				Window.UpdateEffectsOnAsset();
				UpdateEffectsOnUI();
			}

			private class ProxyEditor : GenericEditor
			{
				/// <inheritdoc />
				public override void Initialize(LayoutElementsContainer layout)
				{
					var proxy = (PropertiesProxy)Values[0];

					if (proxy.Asset == null || !proxy.Asset.IsLoaded)
					{
						layout.Label("Loading...");
						return;
					}

					base.Initialize(layout);
				}
			}

			private class MeshessEditor : CustomEditor
			{
				public override DisplayStyle Style => DisplayStyle.InlineIntoParent;

				public override void Initialize(LayoutElementsContainer layout)
				{
					var proxy = (PropertiesProxy)Values[0];
					proxy._materialSlotComboBoxes.Clear();
					proxy._isolateCheckBoxes.Clear();
					proxy._highlightCheckBoxes.Clear();
					var meshes = proxy.Asset.Meshes;
					var skeleton = proxy.Asset.Skeleton;

					// General properties
					{
						var group = layout.Group("General");

						var minScreenSize = group.FloatValue("Min Screen Size", "The minimum screen size to draw model (the bottom limit). Used to cull small models. Set to 0 to disable this feature.");
						minScreenSize.FloatValue.MinValue = 0.0f;
						minScreenSize.FloatValue.MaxValue = 1.0f;
						minScreenSize.FloatValue.Value = proxy.Asset.MinScreenSize;
						minScreenSize.FloatValue.ValueChanged += () =>
						{
							proxy.Asset.MinScreenSize = minScreenSize.FloatValue.Value;
							proxy.Window.MarkAsEdited();
						};

						int triangleCount = 0, vertexCount = 0;
						for (int meshIndex = 0; meshIndex < meshes.Length; meshIndex++)
						{
							var mesh = meshes[meshIndex];
							triangleCount += mesh.Triangles;
							vertexCount += mesh.Vertices;
						}

						group.Label(string.Format("Triangles: {0:N0}   Vertices: {1:N0}", triangleCount, vertexCount));
						group.Label("Bones: " + skeleton.Length);
					}

					// Group per mesh
					var meshesGroup = layout.Group("Meshes");
					meshesGroup.Panel.Close(false);
					for (int meshIndex = 0; meshIndex < meshes.Length; meshIndex++)
					{
						var mesh = meshes[meshIndex];

						var group = meshesGroup.Group("Mesh " + meshIndex);
						group.Label(string.Format("Triangles: {0:N0}   Vertices: {1:N0}", mesh.Triangles, mesh.Vertices));

						// Material Slot
						var materialSlot = group.ComboBox("Material Slot", "Material slot used by this mesh during rendering");
						materialSlot.ComboBox.Tag = mesh;
						materialSlot.ComboBox.SelectedIndexChanged += comboBox => proxy.SetMaterialSlot((SkinnedMesh)comboBox.Tag, comboBox.SelectedIndex);
						proxy._materialSlotComboBoxes.Add(materialSlot.ComboBox);

						// Isolate
						var isolate = group.Checkbox("Isolate", "Shows only this mesh (and meshes using the same material slot)");
						isolate.CheckBox.Tag = mesh;
						isolate.CheckBox.CheckChanged += (box) => proxy.SetIsolate(box.Checked ? (SkinnedMesh)box.Tag : null);
						proxy._isolateCheckBoxes.Add(isolate.CheckBox);

						// Highlight
						var highlight = group.Checkbox("Highlight", "Highlights this mesh with a tint color (and meshes using the same material slot)");
						highlight.CheckBox.Tag = mesh;
						highlight.CheckBox.CheckChanged += (box) => proxy.SetHighlight(box.Checked ? (SkinnedMesh)box.Tag : null);
						proxy._highlightCheckBoxes.Add(highlight.CheckBox);
					}

					// Skeleton
					{
						var group = layout.Group("Skeleton");
						group.Panel.Close(false);

						var tree = group.Tree();
						var root = tree.Node("Root");
						for (int i = 0; i < skeleton.Length; i++)
						{
							if (skeleton[i].ParentIndex == -1)
							{
								var node = root.Node(skeleton[i].Name);
								BuildSkeletonNodeTree(skeleton, node, i);
							}
						}

						root.TreeNode.ExpandAll();
					}

					// Import Settings
					{
						var group = layout.Group("Import Settings");

						var importSettingsField = typeof(PropertiesProxy).GetField("ImportSettings", BindingFlags.NonPublic | BindingFlags.Instance);
						var importSettingsValues = new ValueContainer(importSettingsField) { proxy.ImportSettings };
						group.Object(importSettingsValues);

						layout.Space(5);
						var reimportButton = group.Button("Reimport");
						reimportButton.Button.Clicked += () => ((PropertiesProxy)Values[0]).Reimport();
					}

					// Refresh UI
					proxy.UpdateMaterialSlotsUI();
				}

				private void BuildSkeletonNodeTree(SkeletonBone[] skeleton, TreeNodeElement layout, int boneIndex)
				{
					for (int i = 0; i < skeleton.Length; i++)
					{
						if (skeleton[i].ParentIndex == boneIndex)
						{
							var node = layout.Node(skeleton[i].Name);
							BuildSkeletonNodeTree(skeleton, node, i);
						}
					}
				}
			}
		}

		private readonly SplitPanel _split;
		private readonly AnimatedModelPreview _preview;
		private readonly CustomEditorPresenter _propertiesPresenter;
		private readonly PropertiesProxy _properties;
		private readonly ToolStripButton _saveButton;
		private AnimatedModel _highlightActor;
		private bool _refreshOnMeshesLoaded;

		/// <inheritdoc />
		public SkinnedModelWindow(Editor editor, AssetItem item)
			: base(editor, item)
		{
			// Toolstrip
			_saveButton = (ToolStripButton)_toolstrip.AddButton(editor.UI.GetIcon("Save32"), Save).LinkTooltip("Save asset to the file");
			_toolstrip.AddSeparator();
			_toolstrip.AddButton(editor.UI.GetIcon("Bone32"), () => _preview.ShowBones = !_preview.ShowBones).SetAutoCheck(true).LinkTooltip("Show animated model bones debug view");

			// Split Panel
			_split = new SplitPanel(Orientation.Horizontal, ScrollBars.None, ScrollBars.Vertical)
			{
				DockStyle = DockStyle.Fill,
				SplitterValue = 0.7f,
				Parent = this
			};

			// Model preview
			_preview = new AnimatedModelPreview(true)
			{
				ViewportCamera = new FPSCamera(),
				Parent = _split.Panel1
			};

			// Model properties
			_propertiesPresenter = new CustomEditorPresenter(null);
			_propertiesPresenter.Panel.Parent = _split.Panel2;
			_properties = new PropertiesProxy();
			_propertiesPresenter.Select(_properties);
			_propertiesPresenter.Modified += MarkAsEdited;

			// Highlight actor (used to highlight selected material slot, see UpdateEffectsOnAsset)
			_highlightActor = AnimatedModel.New();
			_highlightActor.IsActive = false;
			_preview.Task.CustomActors.Add(_highlightActor);
		}
		
		/// <summary>
		/// Updates the highlight/isolate effects on a model asset.
		/// </summary>
		private void UpdateEffectsOnAsset()
		{
			var entries = _preview.PreviewModelActor.Entries;
			if (entries != null)
			{
				for (int i = 0; i < entries.Length; i++)
				{
					entries[i].Visible = _properties.IsolateIndex == -1 || _properties.IsolateIndex == i;
				}
			}

			if (_properties.HighlightIndex != -1)
			{
				_highlightActor.IsActive = true;

				var highlightMaterial = FlaxEngine.Content.LoadAsyncInternal<MaterialBase>(EditorAssets.HighlightMaterial);
				entries = _highlightActor.Entries;
				if (entries != null)
				{
					for (int i = 0; i < entries.Length; i++)
					{
						entries[i].Material = highlightMaterial;
						entries[i].Visible = _properties.HighlightIndex == i;
					}
				}
			}
			else
			{
				_highlightActor.IsActive = false;
			}
		}

		/// <inheritdoc />
		public override void Update(float deltaTime)
		{
			// Sync highlight actor size with actual preview model (preview scales model for better usage experiance)
			if (_highlightActor && _highlightActor.IsActive)
			{
				_highlightActor.Transform = _preview.PreviewModelActor.Transform;
			}
			
			// Model is loaded but meshes data may be during streaming so refresh proeprties on fully loaded
			if (_refreshOnMeshesLoaded && _asset && _asset.HasMeshesLoaded)
			{
				_refreshOnMeshesLoaded = false;
				_propertiesPresenter.BuildLayout();
			}

			base.Update(deltaTime);
		}

		/// <inheritdoc />
		public override void Save()
		{
			if (!IsEdited)
				return;

			// Wait until model asset file be fully loaded
			if (_asset.WaitForLoaded())
			{
				// Error
				return;
			}

			// Call asset saving
			if (_asset.Save())
			{
				// Error
				Editor.LogError("Failed to save model " + _item.Name);
				return;
			}

			// Update
			ClearEditedFlag();
			_item.RefreshThumbnail();
		}

		/// <inheritdoc />
		protected override void UpdateToolstrip()
		{
			_saveButton.Enabled = IsEdited;

			base.UpdateToolstrip();
		}

		/// <inheritdoc />
		protected override void UnlinkItem()
		{
			_properties.OnClean();
			_preview.SkinnedModel = null;
			_highlightActor.SkinnedModel = null;

			base.UnlinkItem();
		}

		/// <inheritdoc />
		protected override void OnAssetLinked()
		{
			_preview.SkinnedModel = _asset;
			_highlightActor.SkinnedModel = _asset;

			base.OnAssetLinked();
		}

		/// <inheritdoc />
		protected override void OnAssetLoaded()
		{
			_properties.OnLoad(this);
			_propertiesPresenter.BuildLayout();
			ClearEditedFlag();

			base.OnAssetLoaded();
		}

		/// <inheritdoc />
		public override void OnItemReimported(ContentItem item)
		{
			// Refresh the properties (will get new data in OnAssetLoaded)
			_properties.OnClean();
			_propertiesPresenter.BuildLayout();
			ClearEditedFlag();
			_refreshOnMeshesLoaded = true;

			base.OnItemReimported(item);
		}

		/// <inheritdoc />
		public override void OnDestroy()
		{
			base.OnDestroy();

			FlaxEngine.Object.Destroy(ref _highlightActor);
		}

		/// <inheritdoc />
		public override bool UseLayoutData => true;

		/// <inheritdoc />
		public override void OnLayoutSerialize(XmlWriter writer)
		{
			writer.WriteAttributeString("Split", _split.SplitterValue.ToString());
		}

		/// <inheritdoc />
		public override void OnLayoutDeserialize(XmlElement node)
		{
			float value1;

			if (float.TryParse(node.GetAttribute("Split"), out value1))
				_split.SplitterValue = value1;
		}

		/// <inheritdoc />
		public override void OnLayoutDeserialize()
		{
			_split.SplitterValue = 0.7f;
		}
	}
}