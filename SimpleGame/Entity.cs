using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Класс сущности
    abstract class Entity
    {
        private Position position;

        protected Entity()
        {
            position = new Position();
        }

        protected Entity(float x, float y)
        {
            position = new Position(x, y);
        }

        protected Entity(Position position)
        {
            this.position = new Position(position);
        }

        public abstract void Draw(System.Drawing.Graphics graphics);
    }
}
