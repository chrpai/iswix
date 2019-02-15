﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FireworksFramework.Interfaces;
using FireworksFramework.Managers;
using IsWiXAutomationInterface;

namespace ShortCutsDesigner
{
    public partial class ComponentPicker : Form
    {
        string _fileKey = string.Empty;
        string _fileName = string.Empty;
        IsWiXShortCuts _shortCuts;

        public string FileKey  
        { 
            get
            { 
                return _fileKey;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public ComponentPicker(IsWiXShortCuts shortCuts)
        {
            _shortCuts = shortCuts;
            InitializeComponent();
            PopulateListBox();
            if(treeView1.Nodes.Count==0)
            {
                label1.Text = "No files were found to be suitable for creating a shortcut.";
                treeView1.Visible = false;
            }
        }


        private void PopulateListBox()
        {

            foreach (var candidate in _shortCuts.GetShortCutCandidates())
            {
                TreeNode node = treeView1.Nodes.Add(candidate.DestinationFilePath);
                node.Tag = candidate;
            }

        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShortCutCandidate scc = treeView1.SelectedNode.Tag as ShortCutCandidate;
            _fileKey = scc.FileKey;
            _fileName = scc.FileName;
            buttonSelect.Enabled = true;
        }

    }
}
