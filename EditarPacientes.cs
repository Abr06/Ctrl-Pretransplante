﻿using Capa_Negocio;
using System;
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
    public partial class EditarPacientes : Form
    {
        public EditarPacientes()
        {
            InitializeComponent();
        }

        string genero;
        public bool vacio; // Variable utilizada para saber si hay algún TextBox vacio.
        CN_Paciente objFormaPciente = new CN_Paciente(); //Se crea una instancia de la clase Paciente
        public string id_paciente = null; //Se crea la variable id_paciente para su manipulación 

        private void rdb_masculino_CheckedChanged(object sender, EventArgs e)
        {
            genero = "M";
        }
        private void rdb_femenino_CheckedChanged(object sender, EventArgs e)
        {
            genero = "F";
        }

        private void validar()
        {
            if (rdb_masculino.Checked == true)
            {
                genero = "M";
            }
            else
            {
                genero = "F";
            }

        }

        private Boolean validar(Panel formulario)
        {
            if (rdb_masculino.Checked == true)
            {
                genero = "M";
            }
            else
            {
                genero = "F";
            }
            foreach (Control oControls in formulario.Controls) // Buscamos en cada TextBox de nuestro Formulario.
            {
                if (oControls is TextBox & oControls.Text == String.Empty) // Verificamos que no este vacio.
                {
                    return false; // Si esta vacio el TextBox asignamos el valor True a nuestra variable.
                }
                else
                {

                }
            }
            return true;
        }

        private void btn_fecha_Click(object sender, EventArgs e)
        {
            txt_fecha.Text = monthCalendar1.SelectionStart.Year.ToString() + "-" +
                monthCalendar1.SelectionStart.Month.ToString() + "-" +
                monthCalendar1.SelectionStart.Day.ToString();
        }

        private void txt_fecha_Click(object sender, EventArgs e)
        {
            txt_fecha.Clear();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (validar(panel3) && validar(panel5))
            {
                try
                {
                    if (MessageBox.Show($"Desea editar la informacion del paciente con NSS: {txt_numseg.Text} ", "Advertencia",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        objFormaPciente.Insertar(txt_numseg.Text, txt_curp.Text, txt_nombres.Text, txt_apellidoP.Text, txt_apellidoM.Text, genero, txt_fecha.Text);
                        MessageBox.Show("Se edito correctamente");
                        Close();
                    }
                    else
                    {

                    }
                }
                catch 
                {
                    MessageBox.Show("Error en edicion intente mas tarde.");
                }
            }
            else
            {
                MessageBox.Show("Por favor verifique la informacion.");
            }
        }

        private void btnborrar_MouseEnter(object sender, EventArgs e)
        {
            btnborrar.BackColor = Color.Red;
            btnborrar.ForeColor = Color.White;
            btnborrar.IconColor = Color.White;
        }

        private void btnborrar_MouseLeave(object sender, EventArgs e)
        {
            btnborrar.BackColor = Color.Transparent;
            btnborrar.ForeColor = Color.Black;
            btnborrar.IconColor = Color.Black;
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Desea borrar al paciente con NSS: {txt_numseg.Text} ", "Advertencia",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Capa_Negocio.CN_Paciente eliminar = new CN_Paciente();
                eliminar.EliminarProd(id_paciente);
                MessageBox.Show($"Se elimino al paciente con NSS: {txt_numseg.Text}");
                Close();
            }
            else
            {

            }
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            txt_fecha.Text = e.Start.Year.ToString() + "-" +
                e.Start.Month.ToString() + "-" +
                e.Start.Day.ToString();
        }
    }
}
