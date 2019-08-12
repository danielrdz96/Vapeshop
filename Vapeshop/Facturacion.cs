using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriaDll;

namespace Vapeshop
{
    public partial class Facturacion : Procesos
    {
        public Facturacion()
        {
            InitializeComponent();
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            string vendedor = "select * from Usuarios Where id_Usuario = " + Login.Codigo;

            DataSet ds;

            ds = Biblioteca.Herramientas(vendedor);

            lbVendedor.Text = ds.Tables[0].Rows[0]["username"].ToString().Trim();
        }

        private void BtBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtCodigoClient.Text.Trim()) == false)
                {
                    string cmd = string.Format("select Nombre_cliente from Clientes where id_clientes = '{0}'", TxtCodigoClient.Text.Trim());
                    DataSet ds = Biblioteca.Herramientas(cmd);

                    TxtCliente.Text = ds.Tables[0].Rows[0]["Nombre_cliente"].ToString().Trim();

                    TxtCodigoProducto.Focus();
                }
            }
            catch(Exception error)
            {
                MessageBox.Show("Ha ocurrido un error : " + error.Message);
            }
        }

        public static int contadorFila;
        public static double total;

        private void BtColocar_Click(object sender, EventArgs e)
        {
            if(Biblioteca.ValidarFormulario(this, errorProvider1) == false)
            {
                bool existe = false;
                int numeroFila = 0;


                if(contadorFila == 0)
                {
                    dataGridView1.Rows.Add(TxtCodigoProducto.Text, TxtDescripcion.Text, TxtPrecio.Text, TxtCantidad.Text);
                    double importe = Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[3].Value);
                    dataGridView1.Rows[contadorFila].Cells[4].Value = importe;

                    contadorFila++;
                }
                else
                {
                    foreach (DataGridViewRow Fila in dataGridView1.Rows)
                    {
                        if (Fila.Cells[0].Value.ToString() == TxtCodigoProducto.Text)
                        {
                        existe = true;
                        numeroFila = Fila.Index;
                        }
                    }
                    if (existe == true)
                    {
                        dataGridView1.Rows[numeroFila].Cells[3].Value = (Convert.ToDouble(TxtCantidad.Text) + Convert.ToDouble(dataGridView1.Rows[numeroFila].Cells[3].Value)).ToString();
                        double importe = Convert.ToDouble(dataGridView1.Rows[numeroFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[numeroFila].Cells[3].Value);
                        dataGridView1.Rows[numeroFila].Cells[4].Value = importe;
                    }
                    else
                    {
                        dataGridView1.Rows.Add(TxtCodigoProducto.Text, TxtDescripcion.Text, TxtPrecio.Text, TxtCantidad.Text);
                        double importe = Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[3].Value);
                        dataGridView1.Rows[contadorFila].Cells[4].Value = importe;

                        contadorFila++;
                    }
                }
            }

            total = 0;
            foreach (DataGridViewRow Fila in dataGridView1.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);
            }
            lbTotal.Text = "MXN$ " + total.ToString();
        }

        private void BtEliminar_Click(object sender, EventArgs e)
        {
            if (contadorFila > 0)
            {
                total = total - (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value));
                lbTotal.Text = "MXN$ " + total.ToString();

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                contadorFila--;
            }
        }

        private void BtClientes_Click(object sender, EventArgs e)
        {
            ConsultarClientes ConsulClien = new ConsultarClientes();
            ConsulClien.ShowDialog();

            if(ConsulClien.DialogResult == DialogResult.OK)
            {
                TxtCodigoClient.Text = ConsulClien.dataGridView1.Rows[ConsulClien.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                TxtCliente.Text = ConsulClien.dataGridView1.Rows[ConsulClien.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();

                TxtCodigoProducto.Focus();
            }
        }

        private void BtProductos_Click(object sender, EventArgs e)
        {
            ConsultarProductos ConsulPro = new ConsultarProductos();
            ConsulPro.ShowDialog();

            if (ConsulPro.DialogResult == DialogResult.OK)
            {
                TxtCodigoProducto.Text = ConsulPro.dataGridView1.Rows[ConsulPro.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                TxtDescripcion.Text = ConsulPro.dataGridView1.Rows[ConsulPro.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                TxtPrecio.Text = ConsulPro.dataGridView1.Rows[ConsulPro.dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

                TxtCantidad.Focus();
            }
        }

        private void BtNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        public override void Nuevo()
        {
            TxtCodigoClient.Text = "";
            TxtCliente.Text = "";
            TxtCodigoProducto.Text = "";
            TxtDescripcion.Text = "";
            TxtPrecio.Text = "";
            TxtCantidad.Text = "";
            lbTotal.Text = "MXN$ 0";
            dataGridView1.Rows.Clear();
            contadorFila = 0;
            total = 0;

            TxtCodigoClient.Focus();
        }

        private void BtFacturar_Click(object sender, EventArgs e)
        {
            if (contadorFila != 0)
            {
                try
                {
                    string cmd = string.Format("Exec ActualizarFacturas '{0}'", TxtCodigoClient.Text.Trim());

                    DataSet DS = Biblioteca.Herramientas(cmd);

                    string NumeroFactura = DS.Tables[0].Rows[0]["NumeroFactura"].ToString().Trim();

                    foreach (DataGridViewRow Fila in dataGridView1.Rows)
                    {
                        cmd = string.Format("Exec ActualizarDetalles '{0}','{1}','{2}','{3}'", NumeroFactura, Fila.Cells[0].Value.ToString(), Fila.Cells[2].Value.ToString(), Fila.Cells[3].Value.ToString());
                        DS = Biblioteca.Herramientas(cmd);
                    }
                    cmd = "Exec DatosFactura " + NumeroFactura;

                    DS = Biblioteca.Herramientas(cmd);

                    Factura fac = new Factura();
                    fac.reportViewer1.LocalReport.DataSources[0].Value = DS.Tables[0];
                    fac.ShowDialog();

                    Nuevo();

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: " + error.Message);
                }
            }
        }
    }
}
