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
    public partial class MantenimientoClientes : Mantenimiento
    {
        public MantenimientoClientes()
        {
            InitializeComponent();
        }

        private void MantenimientoClientes_Load(object sender, EventArgs e)
        {

        }

        public override Boolean Guardar()
        {
            if(Biblioteca.ValidarFormulario(this, errorProvider1) == false)
            {
                try
                {
                    string Insertar = string.Format("EXEC ActualizarClientes '{0}','{1}','{2}'", textId_Cliente.Text.Trim(), textNombre_Cliente.Text.Trim(), textApellido_Cliente.Text.Trim());
                    Biblioteca.Herramientas(Insertar);
                    MessageBox.Show("El cliente se ha guardado correctamente");
                    return true;
                }
                catch (Exception error)
                {
                    MessageBox.Show("Ha ocurrido un error" + error);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override void Eliminar()
        {
            try
            {
                string eliminar = string.Format("EXEC EliminarClientes '{0}'", textId_Cliente.Text.Trim());
                Biblioteca.Herramientas(eliminar);
                MessageBox.Show("El cliente se ha eliminado correctamente");
            }

            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error" + error);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextId_Cliente_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textId_Cliente.Text.Trim()) == false && string.IsNullOrEmpty(textNombre_Cliente.Text.Trim()) == false && string.IsNullOrEmpty(textApellido_Cliente.Text.Trim()) == false)
            {
                textId_Cliente.Text = "";
                textNombre_Cliente.Text = "";
                textApellido_Cliente.Text = "";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ConsultarClientes ConsulClien = new ConsultarClientes();
            ConsulClien.Show();
        }
    }
}
