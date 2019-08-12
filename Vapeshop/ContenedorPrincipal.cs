﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vapeshop
{
    public partial class ContenedorPrincipal : Form
    {
        private int childFormNumber = 0;

        public ContenedorPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ClientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MantenimientoClientes ManCli = new MantenimientoClientes();
            ManCli.MdiParent = this;
            ManCli.Show();
        }

        private void ProductosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MantenimientoProductos ManPro = new MantenimientoProductos();
            ManPro.MdiParent = this;
            ManPro.Show();
        }

        private void ProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultarProductos ConsulPro = new ConsultarProductos();
            ConsulPro.MdiParent = this;
            ConsulPro.Show();
        }

        private void ClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultarClientes ConsulClien = new ConsultarClientes();
            ConsulClien.MdiParent = this;
            ConsulClien.Show();
        }

        private void ConsultarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void VentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas ConsulVen = new Ventas();
            ConsulVen.MdiParent = this;
            ConsulVen.Show();
        }

        private void FacturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facturacion factu = new Facturacion();
            factu.MdiParent = this;
            factu.Show();
        }

        private void ContenedorPrincipal_Load(object sender, EventArgs e)
        {
       
        }

        private void ContenedorPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
