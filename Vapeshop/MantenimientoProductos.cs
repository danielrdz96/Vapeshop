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
    public partial class MantenimientoProductos : Mantenimiento
    {
        public MantenimientoProductos()
        {
            InitializeComponent();
        }

        private void MantenimientoProductos_Load(object sender, EventArgs e)
        {

        }

        public override Boolean Guardar()
        {
            if (Biblioteca.ValidarFormulario(this, errorProvider1) == false)
            {
                try
                {
                    string Insertar = string.Format("EXEC ActualizarProductos '{0}','{1}','{2}'", textId_Producto.Text.Trim(), textDescripcion.Text.Trim(), textPrecio.Text.Trim());
                    Biblioteca.Herramientas(Insertar);
                    MessageBox.Show("El producto se ha guardado correctamente");
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
            string eliminar = string.Format("EXEC EliminarProductos '{0}'", textId_Producto.Text.Trim());
            Biblioteca.Herramientas(eliminar);
            MessageBox.Show("El producto se ha eliminado correctamente");
            }

            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error" + error);
            }
        }

        private void TextId_Producto_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textId_Producto.Text.Trim()) == false && string.IsNullOrEmpty(textDescripcion.Text.Trim()) == false && string.IsNullOrEmpty(textPrecio.Text.Trim()) == false)
            {
                textId_Producto.Text = "";
                textDescripcion.Text = "";
                textPrecio.Text = "";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ConsultarProductos ConsulPro = new ConsultarProductos();
            ConsulPro.Show();
        }
    }
}
