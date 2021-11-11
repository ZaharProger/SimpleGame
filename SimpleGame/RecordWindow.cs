using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleGame
{
    public partial class RecordWindow : Form
    {
        public RecordWindow()
        {
            InitializeComponent();
            recordField.Text = Properties.Settings.Default.recordString;
        }

        //Закрытие формы
        private void RecordWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        //Открытие главного окна
        private void RecordWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form hiddenForm = Application.OpenForms[0];
            hiddenForm.Show();
        }

        //Очистка таблицы
        private void resetButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            recordField.Text = "";
        }
    }
}
