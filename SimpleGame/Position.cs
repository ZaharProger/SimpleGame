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
        private float angle;

        public Position()
        {
            x = 0;
            y = 0;
            angle = 0;
        }

        public Position(float x, float y, float angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public Position(Position position)
        {
            x = position.GetX();
            y = position.GetY();
            angle = position.GetAngle();
        }

        public void SetX(float x)
        {
            this.x = x;
        }

        public void SetY(float y)
        {
            this.y = y;
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public float GetAngle()
        {
            return angle;
        }
    }
}
