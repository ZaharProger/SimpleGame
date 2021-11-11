using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Класс для работы с логикой передвижения и динамикой объектов
    class MovementLogic
    {
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
        public static void UpdateCheckPointScale(CheckPoint checkPoint)
        {
            checkPoint.SetScale(checkPoint.GetScale() - 0.01f);             
        }
    }
}
