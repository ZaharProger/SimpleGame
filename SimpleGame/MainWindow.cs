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
        private DarkArea darkArea;
        private List<GameObject> objects;
        private bool isStarted;
        public MainWindow()
        {
            InitializeComponent();
            isStarted = false;
            lifeLine.Maximum = 100;
            timeLine.Maximum = 60;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            else if (e.KeyCode == Keys.S)
            {
                player.SwitchHiddenStatus();
                logField.Text += player.IsHidden()? $"{DateTime.Now:HH:mm:ss} - Игрок надел маскировку!\n" : $"{DateTime.Now:HH:mm:ss} - Игрок снял маскировку!\n";
            }
        }

        //Открытие окна Правила
        private void infoButton_Click(object sender, EventArgs e)
        {
            Form infoForm = new InfoWindow();
            Hide();
            infoForm.ShowDialog();          
        }

        //Открытие таблицы рекордов
        private void recordButton_Click(object sender, EventArgs e)
        {
            RecordWindow recordWindow = new RecordWindow();
            Hide();
            recordWindow.ShowDialog();
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
            darkArea = new DarkArea(new Position(-100, random.Next(0, viewPort.Height + 1), 0), 1);

            player.overlapAction += (gameObject) =>
            {
                logField.Text += ((gameObject is DarkArea && !player.IsHidden()) || !(gameObject is DarkArea)) ? $"{DateTime.Now:HH:mm:ss} - Игрок пересекся с объектом {gameObject}!\n" : "";
            };
            player.destinationPointOverlapAction += (gameObject) =>
            {
                objects.Remove(gameObject);
                destinationPoint = null;
            };
            player.checkPointOverlapAction += () =>
            {
                checkPoint.SetPosition(new Position(random.Next(0, viewPort.Width + 1), random.Next(0, viewPort.Height + 1), 0));
                checkPoint.SetScale(1);
                timeLine.Value = timeLine.Maximum;
                scoreValue.Text = (Convert.ToInt32(scoreValue.Text) + 1).ToString();
            };
            player.darkAreaOverlapPosition += () =>
            {
                if (!player.IsHidden())
                {
                    int remainedHealth = lifeLine.Value - 5;
                    if (remainedHealth < 0)
                        remainedHealth = 0;
                    lifeLine.Value = remainedHealth;
                }
            };

            objects = new List<GameObject>
            {
                player,
                destinationPoint,
                checkPoint,
                darkArea
            };
        }

        //Обновление игровой механики
        private void time_Tick(object sender, EventArgs e)
        {           
            if (isStarted)
            {
                --timeLine.Value;               
                if (timeLine.Value == 0)
                {
                    player.SetHiddenStatus(false);
                    int remainedHealth = lifeLine.Value - 10;
                    if (remainedHealth < 0)
                        remainedHealth = 0;
                    lifeLine.Value = remainedHealth;
                    timeLine.Value = timeLine.Maximum;
                    logField.Text += $"{DateTime.Now:HH:mm:ss} - Игрок не успел до контрольной точки и потерял 10 очков здоровья!\n";
                }
                if (lifeLine.Value <= 0)
                {
                    isStarted = false;
                    logField.Text += $"{DateTime.Now:HH:mm:ss} - Игра окончена, игрок потерял все очки здоровья!\n";

                    Properties.Settings.Default.recordString += $"{DateTime.Now:dd:MM:yyyy}    {DateTime.Now:HH:mm:ss}    Игрок набрал {scoreValue.Text} очков\n";
                    Properties.Settings.Default.Save();
                }

                MovementLogic.UpdatePlayersPosition(player, destinationPoint);
                MovementLogic.UpdateCheckPointScale(checkPoint, random.Next(0, viewPort.Width + 1), random.Next(0, viewPort.Height + 1));
                MovementLogic.UpdateDarkAreaPosition(darkArea);
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
                    if (gameObject != player && player.CheckOverlap(gameObject, e.Graphics))
                        player.Overlap(gameObject);                 

                foreach (GameObject gameObject in objects)
                {
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
