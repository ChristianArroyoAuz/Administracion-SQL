// ******************************************************************************************
// Arroyo Auz Christian Xavier.                                                             *
// 24/06/2016.                                                                              *
// ******************************************************************************************


using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.Linq;
using System.Drawing;
using System.Data;
using System.Linq;
using System;

namespace Administracion_SQL
{
    public partial class Cliente : Form
    {
        //Haciendo referencia a la clase miDB donde se halla la cadena de conexion a la base de datos y
        //creando una variable para poder cargar el ID y hacerlo autonumerado
        miDB miBase = new miDB();
        int idenfificadorUsuario;

        public Cliente()
        {
            InitializeComponent();
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            //Carga el ID autonomerado
            CargarIdentificador();
            iniciarFormulario();
            limpiarCampos();
        }

        public void iniciarFormulario()
        {
            //Bloquenado cierto elementos del formulario
            txtDescripcion.Enabled = true;
            txtNombre.Enabled = true;
            txtCosto.Enabled = true;
            txtBuscar.Enabled = true;
            boton_Agregar.Enabled = true;
            boton_Mostrar.Enabled = true;
            boton_Buscar.Enabled = true;
            boton_Modificar.Enabled = false;
            boton_Eliminar.Enabled = false;
        }

        public void CargarIdentificador()
        {
            //consulta a la base de datos usando LINQ para obtener el ID mas alto, sumarle 1 y presentar
            // en el formulario para el ingreso de de datos
            var consulta = (from piezas in miBase.Piezas
                            where piezas.Id > 0
                            orderby piezas.Id descending
                            select (int)piezas.Id).Take(1);
            foreach (var identificador in consulta)
            {
                idenfificadorUsuario = identificador;
                txtID.Text = Convert.ToString(identificador + 1);
            }
        }

        public void cargarDatos()
        {
            //Consulta a la base de datos usando LINQ para presentar todos los datos cuyo ID sea mayor o igual 0
            //Los datos se presentaran un datagrridview de forma ascendente
            var consulta = from piezas in miBase.Piezas
                           where piezas.Id >= 0
                           orderby piezas.Id ascending
                           select new
                           {
                               ID = piezas.Id,
                               NOMBRE = piezas.Nombre_Pieza,
                               DESCRIPCION = piezas.Descripcion,
                               COSTO = piezas.Costo
                           };
            dataGridView_Datos.DataSource = consulta.ToList();
            //Desactivar seleccion automatica de la primera celda
            dataGridView_Datos.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        public void limpiarCampos()
        {
            //Me permitira limpiar los textbox y cargando el ID
            txtDescripcion.Clear();
            txtNombre.Clear();
            txtBuscar.Clear();
            txtNombre.Focus();
            txtCosto.Clear();
            CargarIdentificador();
        }

        private void boton_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcion.Text == "" || txtNombre.Text == "" || txtCosto.Text == "")
                {
                    //Control delos textbox no esten vacios
                    MessageBox.Show("Debe ingresar todos los datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Aumentando en 1 el ID para su almacenamiento
                    idenfificadorUsuario = idenfificadorUsuario + 1;
                    //Agregando los diferentes parametros a las tabla de piezas
                    Piezas nuevo = new Piezas();
                    nuevo.Id = idenfificadorUsuario;
                    nuevo.Nombre_Pieza = txtNombre.Text;
                    nuevo.Descripcion = txtDescripcion.Text;
                    nuevo.Costo = Convert.ToInt32(txtCosto.Text);
                    miBase.Piezas.InsertOnSubmit(nuevo);
                    miBase.SubmitChanges();
                    //Consulta a la base de datos usando LINQ para presentar todos los datos cuyo ID sea mayor o igual 0
                    //Los datos se presentaran un datagrridview de forma ascendente
                    var consulta = from piezas in miBase.Piezas
                                   where piezas.Id == idenfificadorUsuario
                                   select new
                                   {
                                       ID = piezas.Id,
                                       NOMBRE = piezas.Nombre_Pieza,
                                       DESCRIPCION = piezas.Descripcion,
                                       COSTO = piezas.Costo
                                   };
                    dataGridView_Datos.DataSource = consulta.ToList();
                    //Desactivar seleccion automatica de la primera celda
                    dataGridView_Datos.ClearSelection();
                    //limpiando los diferentes campos
                    limpiarCampos();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo guardar los datos debido a un error.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void boton_Modificar_Click(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Seguro desea modificar el elemnto?", "Confirmacion", MessageBoxButtons.YesNo);
            if (pregunta == DialogResult.Yes)
            {
                //Codigo que me permite la modificacion de los parametros del elemento seleccionado
                miBase.ExecuteCommand("Update Piezas set Id =" + "'" + txtID.Text + "'" + "where Id =" + Convert.ToString(txtID.Text) + ";");
                miBase.ExecuteCommand("Update Piezas set Nombre_Pieza =" + "'" + txtNombre.Text + "'" + "where Id =" + Convert.ToString(txtID.Text) + ";");
                miBase.ExecuteCommand("Update Piezas set Descripcion =" + "'" + txtDescripcion.Text + "'" + "where Id =" + Convert.ToString(txtID.Text) + ";");
                miBase.ExecuteCommand("Update Piezas set Costo =" + "'" + txtCosto.Text + "'" + "where Id =" + Convert.ToString(txtID.Text) + ";");
                //Actualizando la tabla de la base de datos
                miBase.SubmitChanges();
                //Mostrando los datos en el datagridview
                cargarDatos();
                limpiarCampos();
                iniciarFormulario();
                dataGridView_Datos.ClearSelection();
            }
            else
            {
                return;
            }
        }

        private void boton_Eliminar_Click(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Seguro desea eliminar el elemnto?", "Confirmacion", MessageBoxButtons.YesNo);
            if (pregunta == DialogResult.Yes)
            {
                //Codigo que me permite la eliminacion del elemto seleccionado de la tabla en la base de datos
                miBase.ExecuteCommand("Delete from Piezas where Id = " + Convert.ToString(txtID.Text) + ";");
                miBase.SubmitChanges();
                cargarDatos();
                limpiarCampos();
                CargarIdentificador();
                iniciarFormulario();
                dataGridView_Datos.ClearSelection();
            }
            else
            {
                return;
            }
        }

        private void boton_Mostrar_Click(object sender, EventArgs e)
        {
            //Me permite mostrar todos los elemntos dentro de la tabla Piezas en la la base de datos
            CargarIdentificador();
            cargarDatos();
            limpiarCampos();
        }

        private void boton_Buscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                //mensaje de advertencia si el cuadro esta vacio
                MessageBox.Show("Debe ingresar el ID para la busqueda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    //realizando la consulta a la base de datos mediante LINQ con el ID deseado
                    var consulta = from piezas in miBase.Piezas
                                   where piezas.Id == Convert.ToInt32(txtBuscar.Text)
                                   select new
                                   {
                                       ID = piezas.Id,
                                       NOMBRE = piezas.Nombre_Pieza,
                                       DESCRIPCION = piezas.Descripcion,
                                       COSTO = piezas.Costo
                                   };
                    if (consulta.Count() > 0)
                    {
                        dataGridView_Datos.DataSource = consulta.ToList();
                        //Desactivar seleccion automatica de la primera celda
                        dataGridView_Datos.ClearSelection();
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No hubo resultados en la busqueda.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        limpiarCampos();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo buscar los datos debido a un error.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    limpiarCampos();
                }
            }
        }

        private void dataGridView_Datos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView_Datos.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNombre.Text = dataGridView_Datos.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDescripcion.Text = dataGridView_Datos.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCosto.Text = dataGridView_Datos.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Codigo que solo permite el ingreso de caracteres
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Codigo que solo permite el ingreso de caracteres
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Codigo que permite solo ingreso de numeros en el textbox
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Codigo que permite solo ingreso de numeros en el textbox
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void dataGridView_Datos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Verificando que haya elemntos seleccionados
            if (dataGridView_Datos.SelectedCells.Count > 0)
            {
                boton_Modificar.Enabled = true;
                boton_Eliminar.Enabled = true;
                boton_Agregar.Enabled = false;
                boton_Mostrar.Enabled = false;
                boton_Buscar.Enabled = false;
            }
            else
            {
                //mensaje de advertencia si no hay elementos seleccionados
                MessageBox.Show("Debe seleccionar un elemento para modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
