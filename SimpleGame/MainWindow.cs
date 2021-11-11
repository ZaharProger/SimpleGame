﻿using System;
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
        private List<GameObject> objects;
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

        //Запуск игры
        private void startButton_Click(object sender, EventArgs e)
        {
            isStarted = true;
            scoreValue.Text = "0";          
            lifeLine.Value = lifeLine.Maximum;
            timeLine.Value = timeLine.Maximum;
            logField.Text = $"{DateTime.Now:HH:mm:ss} - Новая игра началась!\n";
            player = new Player(viewPort.Width / 2, viewPort.Height / 2, 0);
            destinationPoint = new DestinationPoint(viewPort.Width / 2 + 10, viewPort.Height / 2 + 10, 0);
            objects = new List<GameObject>();
            objects.Add(player);
            objects.Add(destinationPoint);
        }

        //Обновление позиции игрока
        private void UpdatePlayersPosition()
        {
            Position updatedPlayersPosition = new Position();

            if (destinationPoint != null)
            {
                updatedPlayersPosition.SetX(destinationPoint.GetPosition().GetX() - player.GetPosition().GetX());
                updatedPlayersPosition.SetY(destinationPoint.GetPosition().GetY() - player.GetPosition().GetY());

                float length = MathF.Sqrt(MathF.Pow(updatedPlayersPosition.GetX(), 2) + MathF.Pow(updatedPlayersPosition.GetY(), 2));
                updatedPlayersPosition.SetX(updatedPlayersPosition.GetX() / length);
                updatedPlayersPosition.SetY(updatedPlayersPosition.GetY() / length);

                player.SetSpeedX(player.GetSpeedX() + updatedPlayersPosition.GetX() * 0.5f);
                player.SetSpeedY(player.GetSpeedY() + updatedPlayersPosition.GetY() * 0.5f);

                updatedPlayersPosition.SetAngle(90 - MathF.Atan2(player.GetSpeedX(), player.GetSpeedY()) * 180 / MathF.PI);
            }
            
            player.SetSpeedX(player.GetSpeedX() * (-0.1f));
            player.SetSpeedY(player.GetSpeedY() * (-0.1f));
            
            updatedPlayersPosition.SetX(player.GetPosition().GetX() + player.GetSpeedX());
            
            updatedPlayersPosition.SetY(player.GetPosition().GetY() + player.GetSpeedY());
            player.SetPosition(updatedPlayersPosition);
        }

        //Обновление игровой механики
        private void time_Tick(object sender, EventArgs e)
        {
            if (isStarted)
            {
                //--timeLine.Value;
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

                UpdatePlayersPosition();
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
                foreach (GameObject gameObject in objects.ToArray())
                {
                    if (gameObject != player && player.IsOverlapped(gameObject, e.Graphics))
                    {
                        logField.Text += $"{DateTime.Now:HH:mm:ss} - Игрок переместился в заданную позицию!\n";
                        objects.Remove(gameObject);
                        destinationPoint = null;
                    }
                    e.Graphics.Transform = gameObject.GetTransformData();
                    gameObject.Draw(e.Graphics);
                }
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
            {
                Position mousePosition = new Position(e.X, e.Y, 0);
                if (destinationPoint != null)
                    destinationPoint.SetPosition(mousePosition);
                else
                {
                    destinationPoint = new DestinationPoint(mousePosition);
                    objects.Add(destinationPoint);
                }
            }    
        }       
    }
}
