using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Позиция на поле
    class Position
    {
        private float x;
        private float y;

        public Position()
        {
            x = 0;
            y = 0;
        }

        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Position(Position position)
        {
            x = position.GetX();
            y = position.GetY();
        }

        public void SetX(float x)
        {
            this.x = x;
        }

        public void SetY(float y)
        {
            this.y = y;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }
    }
}
