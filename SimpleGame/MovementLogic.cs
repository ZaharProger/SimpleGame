using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Класс для работы с логикой передвижения и динамикой объектов
    class MovementLogic
    {
        private static float newX = -100;
        //Обновление позиции игрока
        public static void UpdatePlayersPosition(Player player, DestinationPoint destinationPoint)
        {
            Position updatedPlayersPosition = new Position();

            if (destinationPoint != null)
            {
                updatedPlayersPosition.SetX(destinationPoint.GetPosition().GetX() - player.GetPosition().GetX());
                updatedPlayersPosition.SetY(destinationPoint.GetPosition().GetY() - player.GetPosition().GetY());

                float length = MathF.Sqrt(MathF.Pow(updatedPlayersPosition.GetX(), 2) + MathF.Pow(updatedPlayersPosition.GetY(), 2));
                updatedPlayersPosition.SetX(updatedPlayersPosition.GetX() / length);
                updatedPlayersPosition.SetY(updatedPlayersPosition.GetY() / length);

                player.SetSpeedX(player.GetSpeedX() + updatedPlayersPosition.GetX() * 1.2f);
                player.SetSpeedY(player.GetSpeedY() + updatedPlayersPosition.GetY() * 1.2f);

                updatedPlayersPosition.SetAngle(90 - MathF.Atan2(player.GetSpeedX(), player.GetSpeedY()) * 180 / MathF.PI);
            }

            player.SetSpeedX(player.GetSpeedX() + (-player.GetSpeedX() * 0.1f));
            player.SetSpeedY(player.GetSpeedY() + (-player.GetSpeedY() * 0.1f));

            updatedPlayersPosition.SetX(player.GetPosition().GetX() + player.GetSpeedX());
            updatedPlayersPosition.SetY(player.GetPosition().GetY() + player.GetSpeedY());

            player.SetPosition(updatedPlayersPosition);
        }

        //Масштабирование контрольной точки
        public static void UpdateCheckPointScale(CheckPoint checkPoint, float newX, float newY)
        {
            checkPoint.SetScale(checkPoint.GetScale() - 0.01f);
            if (checkPoint.GetScale() <= 0)
            {
                checkPoint.SetPosition(new Position(newX, newY, 0));
                checkPoint.SetScale(1);
            }
        }

        //Движение черной области
        public static void UpdateDarkAreaPosition(DarkArea darkArea)
        {
            if (newX == -100)
                darkArea.SetPosition(new Position(darkArea.GetPosition().GetX() + 10, darkArea.GetPosition().GetY(), 0));
            else if (newX == 700)
                darkArea.SetPosition(new Position(darkArea.GetPosition().GetX() - 10, darkArea.GetPosition().GetY(), 0));

            if (darkArea.GetPosition().GetX() == 700 || darkArea.GetPosition().GetX() == -100)
            {
                newX = darkArea.GetPosition().GetX();
                darkArea.SetPosition(new Position(newX, darkArea.GetPosition().GetY(), 0));
            }                
        }
    }
}
