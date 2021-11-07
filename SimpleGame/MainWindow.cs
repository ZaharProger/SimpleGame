using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleGame
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            scoreValue.Text = "0";
            lifeLine.Maximum = 100;
            lifeLine.Value = lifeLine.Maximum;
            timeLine.Maximum = 60;
            timeLine.Value = timeLine.Maximum;
        }

        //Закрытие формы
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        //Открытие окна Правила
        private void infoButton_Click(object sender, EventArgs e)
        {
            Form infoForm = new InfoWindow();
            Hide();
            infoForm.ShowDialog();          
        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }
    }
}
