using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Класс сущности
    abstract class Entity
    {
        protected Position position;

        protected Entity()
        {
            position = new Position();
        }

        protected Entity(float x, float y, float angle)
        {
            position = new Position(x, y, angle);
        }

        protected Entity(Position position)
        {
            this.position = new Position(position);
        }

        public Position GetPosition()
        {
            return position;
        }

        public abstract void Draw(System.Drawing.Graphics drawer);
    }
}
