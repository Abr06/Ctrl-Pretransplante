﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_PreTransplante_V2
{
    public partial class Lista_P : Form
    {
        string[] datos;
        Capa_Negocio.CN_Paciente objforma;
        Form formulario;

        public Lista_P()
        {
            InitializeComponent();
        }

        private void MostrarPa()//Método para vizualizar los registros de la DB
        {
            objforma = new Capa_Negocio.CN_Paciente();
            Lista.DataSource = objforma.MostrarPaci();
            Lista.Columns[0].Visible = false;
        }

        private void Lista_P_Load(object sender, EventArgs e)//Actualisa la lista de pacientes
        {
            MostrarPa();
            datos = new string[7];
        }

        private void iconButton1_Click(object sender, EventArgs e)//boton de actualizar lista de pacientes
        {
            MostrarPa();
        }

        private void Estudios_Click(object sender, EventArgs e)//Seleccionamos un apcientes y mandamos la informacion a otro formulario en un arreglo 
        {
            if (Filas())
            {
                datos[0] = Lista.Rows[Lista.SelectedRows[0].Index].Cells[0].Value.ToString();
                datos[1] = Lista.Rows[Lista.SelectedRows[0].Index].Cells[1].Value.ToString();
                datos[2] = Lista.Rows[Lista.SelectedRows[0].Index].Cells[2].Value.ToString();
                datos[3] = Lista.Rows[Lista.SelectedRows[0].Index].Cells[3].Value.ToString();
                datos[4] = Lista.Rows[Lista.SelectedRows[0].Index].Cells[4].Value.ToString();
                datos[5] = Lista.Rows[Lista.SelectedRows[0].Index].Cells[5].Value.ToString();
                datos[6] = Lista.Rows[Lista.SelectedRows[0].Index].Cells[6].Value.ToString();
                formulario = pacientes.Controls.OfType<Paciente>().FirstOrDefault();//buscaen la coleccion el formulario
                                                                                    //si el formulario/instancia no existe
                if (formulario == null)
                {
                    formulario = new Paciente(datos);
                    formulario.TopLevel = false;
                    pacientes.Controls.Add(formulario);
                    pacientes.Tag = formulario;
                    formulario.Dock = DockStyle.Fill;
                    formulario.Show();
                }
                else//Si el formulario existe
                {
                    formulario.Close();
                    formulario = new Paciente(datos);
                    formulario.TopLevel = false;
                    pacientes.Controls.Add(formulario);
                    pacientes.Tag = formulario;
                    formulario.Dock = DockStyle.Fill;
                    formulario.Show();
                }
                tap.DeselectTab(0);//Cambia a la siguiente pagina del tap desde la pagina 0
            }
            else { }
        }

        private void btneditar_Click(object sender, EventArgs e)//Seleccionamos un pacientes y pasomos los datos a otro formulario con propiedades publicas
        {
            if (Filas())
            {
                EditarPacientes editar = new EditarPacientes();
                editar.txt_nombres.Text = Lista.Rows[Lista.SelectedRows[0].Index].Cells[1].Value.ToString();
                editar.txt_apellidoP.Text = Lista.Rows[Lista.SelectedRows[0].Index].Cells[2].Value.ToString();
                editar.txt_apellidoM.Text = Lista.Rows[Lista.SelectedRows[0].Index].Cells[3].Value.ToString();
                editar.txt_curp.Text = Lista.Rows[Lista.SelectedRows[0].Index].Cells[4].Value.ToString();
                editar.txt_numseg.Text = Lista.Rows[Lista.SelectedRows[0].Index].Cells[5].Value.ToString();
                editar.txt_fecha.Text = Lista.Rows[Lista.SelectedRows[0].Index].Cells[6].Value.ToString();
                editar.id_paciente = Lista.Rows[Lista.SelectedRows[0].Index].Cells[0].Value.ToString();
                if (Lista.Rows[Lista.SelectedRows[0].Index].Cells[7].Value.ToString() != "F")
                {
                    editar.rdb_masculino.Checked = true;
                }
                else
                {
                    editar.rdb_femenino.Checked = true;
                }
                editar.TopMost = true;
                editar.TopLevel = true;
                editar.ShowDialog();
                MostrarPa();
            }
            else
            {

            }
        }

        private void Lista_Resize(object sender, EventArgs e)//Adaptamos el tamaño de letra segun el tamaño de la ventana
        {
            if(this.Size.Width < 1120)
            {
                Lista.ColumnHeadersDefaultCellStyle.Font = new Font(Lista.ColumnHeadersDefaultCellStyle.Font.Name, 10);
                Lista.RowsDefaultCellStyle.Font = new Font(Lista.RowsDefaultCellStyle.Font.Name, 12);
            }
            else
            {
                Lista.ColumnHeadersDefaultCellStyle.Font = new Font(Lista.ColumnHeadersDefaultCellStyle.Font.Name, 16);
                Lista.RowsDefaultCellStyle.Font = new Font(Lista.RowsDefaultCellStyle.Font.Name, 16);
            }   
        }

        private Boolean Filas()//Verificar que solo sekeccione una fila y esta no sea fila null
        {
            if (Lista.SelectedRows.Count < 2 && Lista.SelectedRows[0].Index < (Lista.Rows.Count - 1))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Seleccione solo una fila");
                return false;
            }
        }

        private void txt_buscar_KeyDown(object sender, KeyEventArgs e)//Metodo de filtrar paceintes por textbox
        {
            int c_filas = Lista.Rows.Count;
            CurrencyManager cm = (CurrencyManager)BindingContext[Lista.DataSource];
            cm.SuspendBinding();
            if (e.KeyData == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    foreach (DataGridViewRow oControls in Lista.Rows) // Ocultamos todas las filas
                    {
                        if(oControls.Index != c_filas - 1)
                        {
                            oControls.Visible = false;
                        }
                        else { }
                    }
                    foreach (DataGridViewRow oControls in Lista.Rows) //Mostramos las que coinciden con la busqueda
                    {
                        if (oControls.Index != c_filas - 1)
                        {
                            foreach (DataGridViewCell cell in oControls.Cells)
                            {
                                if (cell.Value.ToString().Contains(txt_buscar.Text))
                                {
                                    oControls.Visible = true;
                                    break;
                                }
                                else { }
                            }
                        }
                        else { }
                    }
                }
                else { }
            }
            else { }
        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            int c_filas = Lista.Rows.Count;
            foreach (DataGridViewRow oControls in Lista.Rows) // Ocultamos todas las filas
            {
                if (oControls.Index != c_filas - 1)
                {
                    oControls.Visible = true;
                }
                else { }
            }
        }
    }
}
