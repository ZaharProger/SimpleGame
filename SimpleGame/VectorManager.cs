﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Логика работы с векторами
    class VectorManager
    {
        public static float calculateLength(Position begin, Position end)
        {
            return (float)Math.Sqrt(Math.Pow(end.GetX() - begin.GetX(), 2) + Math.Pow(end.GetY() - begin.GetY(), 2));
        }

        public static Position normalize(Position begin, Position end)
        {
            float length = calculateLength(begin, end);

            return new Position((end.GetX() - begin.GetX()) / length, (end.GetY() - begin.GetY()) / length);
        }
    }
}
