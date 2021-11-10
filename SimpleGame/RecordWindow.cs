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

        //Запись рекорда
        public void WriteRecord(string score)
        {
            recordField.Text += $"{DateTime.Now:dd::MM::yyyy}\t{DateTime.Now:HH:mm:ss}\t{score}\n";
        }
    }
}
