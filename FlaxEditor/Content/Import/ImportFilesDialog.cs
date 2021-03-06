////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2018 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using FlaxEditor.CustomEditors;
using FlaxEditor.GUI.Dialogs;
using FlaxEngine;
using FlaxEngine.GUI;

namespace FlaxEditor.Content.Import
{
    /// <summary>
    /// Dialog used to edit import files settings.
    /// </summary>
    /// <seealso cref="FlaxEditor.GUI.Dialogs.Dialog" />
    public class ImportFilesDialog : Dialog
    {
        private TreeNode _rootNode;
        private CustomEditorPresenter _settingsEditor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportFilesDialog"/> class.
        /// </summary>
        /// <param name="entries">The entries to edit settings.</param>
        public ImportFilesDialog(List<ImportFileEntry> entries)
            : base("Import files settings")
        {
            if (entries == null || entries.Count < 1)
                throw new ArgumentNullException();

            const float TotalWidth = 920;
            const float EditorHeight = 450;
            Width = TotalWidth;

            // Header and help description
            var headerLabel = new Label(0, 0, TotalWidth, 40)
            {
                Text = "Import settings",
                DockStyle = DockStyle.Top,
                Parent = this,
                Font = Style.Current.FontTitle
            };
            var infoLabel = new Label(10, headerLabel.Bottom + 5, TotalWidth - 20, 40)
            {
                Text = "Specify options for importing files. Every file can have different settings. Select entries on the left panel to modify them.\nPro Tip: hold CTRL key and select entries to edit multiple at once.",
                HorizontalAlignment = TextAlignment.Near,
                Margin = new Margin(7),
                DockStyle = DockStyle.Top,
                Parent = this
            };

            // Buttons
            const float ButtonsWidth = 60;
            const float ButtonsMargin = 8;
            var importButton = new Button(TotalWidth - ButtonsMargin - ButtonsWidth, infoLabel.Bottom - 30, ButtonsWidth)
            {
                Text = "Import",
                AnchorStyle = AnchorStyle.UpperRight,
                Parent = this
            };
            importButton.Clicked += OnImport;
            var cancelButton = new Button(importButton.Left - ButtonsMargin - ButtonsWidth, importButton.Y, ButtonsWidth)
            {
                Text = "Cancel",
                AnchorStyle = AnchorStyle.UpperRight,
                Parent = this
            };
            cancelButton.Clicked += OnCancel;

            // Split panel for entries list and settings editor
            var splitPanel = new SplitPanel(Orientation.Horizontal, ScrollBars.Vertical, ScrollBars.Vertical)
            {
                Y = infoLabel.Bottom,
                Size = new Vector2(TotalWidth, EditorHeight),
                DockStyle = DockStyle.Fill,
                Parent = this
            };

            // Settings editor
            _settingsEditor = new CustomEditorPresenter(null);
            _settingsEditor.Panel.Parent = splitPanel.Panel2;

            // Setup tree
            var tree = new Tree(true);
            tree.Parent = splitPanel.Panel1;
            _rootNode = new TreeNode(false);
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];

                // TODO: add icons for textures/models/etc from FileEntry to tree node??
                var node = new TreeNode(false)
                {
                    Text = Path.GetFileName(entry.SourceUrl),
                    Tag = entry,
                    Parent = _rootNode
                };
                node.LinkTooltip(entry.SourceUrl);
            }
            _rootNode.Expand();
            _rootNode.Parent = tree;
            tree.SelectedChanged += OnSelectedChanged;

            // Select the first item
            tree.Select(_rootNode.Children[0] as TreeNode);

            Size = new Vector2(TotalWidth, splitPanel.Bottom);
        }

        private void OnImport()
        {
            var entries = new List<ImportFileEntry>(_rootNode.ChildrenCount);
            for (int i = 0; i < _rootNode.ChildrenCount; i++)
            {
                var fileEntry = _rootNode.Children[i].Tag as ImportFileEntry;
                if (fileEntry != null)
                    entries.Add(fileEntry);
            }
            Editor.Instance.ContentImporting.LetThemBeImportedxD(entries);

            Close(DialogResult.OK);
        }

        private void OnCancel()
        {
            Close(DialogResult.Cancel);
        }

        private void OnSelectedChanged(List<TreeNode> before, List<TreeNode> after)
        {
            var selection = new List<object>(after.Count);
            for (int i = 0; i < after.Count; i++)
            {
                if (after[i].Tag is ImportFileEntry fileEntry && fileEntry.HasSettings)
                    selection.Add(fileEntry.Settings);
            }

            _settingsEditor.Select(selection);
        }

        /// <inheritdoc />
        protected override void SetupWindowSettings(ref CreateWindowSettings settings)
        {
            base.SetupWindowSettings(ref settings);

            settings.MinimumSize = new Vector2(300, 400);
            settings.HasSizingFrame = true;
        }
    }
}
