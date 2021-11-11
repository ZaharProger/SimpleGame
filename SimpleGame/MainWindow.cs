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
        private Random random;
        private Player player;
        private DestinationPoint destinationPoint;
        private CheckPoint checkPoint;
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
            random = new Random();
            isStarted = true;
            scoreValue.Text = "0";          
            lifeLine.Value = lifeLine.Maximum;
            timeLine.Value = timeLine.Maximum;
            logField.Text = $"{DateTime.Now:HH:mm:ss} - Новая игра началась!\n";
            player = new Player(viewPort.Width / 2, viewPort.Height / 2, 0);
            destinationPoint = new DestinationPoint(viewPort.Width / 2 + 10, viewPort.Height / 2 + 10, 0);
            checkPoint = new CheckPoint(new Position(random.Next(0, viewPort.Width + 1), random.Next(0, viewPort.Height + 1), 0));
            objects = new List<GameObject>
            {
                player,
                destinationPoint,
                checkPoint
            };
        }

        //Обновление игровой механики
        private void time_Tick(object sender, EventArgs e)
        {           
            if (isStarted)
            {
                --timeLine.Value;
                if (checkPoint.IsOverlapped())
                {
                    timeLine.Value = timeLine.Maximum;
                    scoreValue.Text = (Convert.ToInt32(scoreValue.Text) + 1).ToString();
                }                  
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

                MovementLogic.UpdatePlayersPosition(player, destinationPoint);
                MovementLogic.UpdateCheckPointScale(checkPoint);

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
                if (checkPoint.GetScale() <= 0)
                {
                    checkPoint.SetPosition(new Position(random.Next(0, viewPort.Width + 1), random.Next(0, viewPort.Height + 1), 0));
                    checkPoint.SetScale(1);
                }
                foreach (GameObject gameObject in objects.ToArray())
                {
                    player.CheckOverlap(gameObject, e.Graphics);
                    if (gameObject != player && player.IsOverlapped())
                    {
                        logField.Text += $"{DateTime.Now:HH:mm:ss} - Игрок пересекся с объектом {gameObject}!\n";
                        if (gameObject == destinationPoint)
                        {
                            objects.Remove(gameObject);
                            destinationPoint = null;
                        }
                        else if (gameObject == checkPoint)
                        {
                            checkPoint.SetPosition(new Position(random.Next(0, viewPort.Width + 1), random.Next(0, viewPort.Height + 1), 0));
                            checkPoint.SetScale(1);
                        }
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

        //Прокрутка лог поля в конец
        private void logField_TextChanged(object sender, EventArgs e)
        {
            logField.SelectionStart = logField.Text.Length;
            logField.ScrollToCaret();
        }
    }
}
