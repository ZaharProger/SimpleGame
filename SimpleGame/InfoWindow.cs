using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleGame
{
    public partial class InfoWindow : Form
    {
        public InfoWindow()
        {
            InitializeComponent();
        }

        //Закрытие формы
        private void InfoWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        //Открытие свернутой формы
        private void InfoWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form hiddenForm = Application.OpenForms[0];
            hiddenForm.Show();          
        }
    }
}
