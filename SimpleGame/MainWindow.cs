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
        private Player player;
        private DestinationPoint destinationPoint;
        private bool isStarted;
        public MainWindow()
        {
            InitializeComponent();
            isStarted = false;
            lifeLine.Maximum = 100;
            timeLine.Maximum = 60;   
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
            isStarted = true;
            scoreValue.Text = "0";          
            lifeLine.Value = lifeLine.Maximum;
            timeLine.Value = timeLine.Maximum;
            logField.Text = $"{DateTime.Now:HH:mm:ss} - Новая игра началась!\n";
            player = new Player(viewPort.Width / 2, viewPort.Height / 2, 0);
            destinationPoint = new DestinationPoint(viewPort.Width / 2 + 10, viewPort.Height / 2 + 10, 0);
            player.Overlap += (player, gameObject) => logField.Text += $"{DateTime.Now:HH:mm:ss} - Игрок пересекся с {gameObject}!\n";
            player.DestinationPointOverlap += (destinationPoint) => destinationPoint = null;
        }

        //Обновление игровой механики
        private void time_Tick(object sender, EventArgs e)
        {
            if (isStarted)
            {
                --timeLine.Value;
                if (timeLine.Value == 0)
                {
                    timeLine.Value = timeLine.Maximum;
                    lifeLine.Value -= 20;
                    logField.Text += $"{DateTime.Now:HH:mm:ss} - Игрок не успел до контрольной точки и потерял 20 очков здоровья!\n";
                }
                if (lifeLine.Value <= 0)
                {
                    isStarted = false;
                    logField.Text += $"{DateTime.Now:HH:mm:ss} - Игра окончена, игрок потерял все очки здоровья!\n";
                }
                Position updatedPlayersPosition = VectorManager.normalize(player.GetPosition(), destinationPoint.GetPosition());
                player.SetSpeedX(player.GetSpeedX() + (destinationPoint.GetPosition().GetX() - player.GetPosition().GetX()) * 0.1f);
                player.SetSpeedY(player.GetSpeedY() + (destinationPoint.GetPosition().GetY() - player.GetPosition().GetY()) * 0.1f);
                updatedPlayersPosition.SetX(updatedPlayersPosition.GetX() + player.GetSpeedX());
                updatedPlayersPosition.SetY(updatedPlayersPosition.GetY() + player.GetSpeedY());
                updatedPlayersPosition.SetAngle(90 - MathF.Atan2(destinationPoint.GetPosition().GetX() - player.GetPosition().GetX(), destinationPoint.GetPosition().GetY() - player.GetPosition().GetY()) * 180 / MathF.PI);
                player.SetPosition(updatedPlayersPosition);

                viewPort.Invalidate();
            }
            else
            {
                timeLine.Value = 0;
                scoreValue.Text = "";
            }          
        }

        //Отрисовка игры
        private void viewPort_Paint(object sender, PaintEventArgs e)
        {
            if (isStarted)
            {
                e.Graphics.Clear(Color.White);
                e.Graphics.Transform = player.GetTransformData();
                player.Draw(e.Graphics);
                e.Graphics.Transform = destinationPoint.GetTransformData();
                destinationPoint.Draw(e.Graphics);
            }
            else
            {
                e.Graphics.Clear(Color.WhiteSmoke);
            }
        }

        //Обновление точки прибытия игрока
        private void viewPort_MouseClick(object sender, MouseEventArgs e)
        {
            if (isStarted)
                destinationPoint.SetPosition(new Position(e.X, e.Y, 0));
        }       
    }
}
