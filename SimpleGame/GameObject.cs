using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Класс игрового объекта
    abstract class GameObject
    {
        protected Position position;

        protected GameObject()
        {
            position = new Position();
        }

        protected GameObject(float x, float y, float angle)
        {
            position = new Position(x, y, angle);
        }

        protected GameObject(Position position)
        {
            this.position = new Position(position);
        }

        public void SetPosition(Position position)
        {
            this.position.SetX(position.GetX());
            this.position.SetY(position.GetY());
            this.position.SetAngle(position.GetAngle());
        }

        public Position GetPosition()
        {
            return position;
        }

        public virtual System.Drawing.Drawing2D.Matrix GetTransformData()
        {
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            matrix.Translate(position.GetX(), position.GetY());
            matrix.Rotate(position.GetAngle());

            return matrix;
        }

        public abstract void Draw(System.Drawing.Graphics drawer);

        public virtual System.Drawing.Drawing2D.GraphicsPath GetRegion()
        {
            return new System.Drawing.Drawing2D.GraphicsPath();
        }

        public bool CheckOverlap(GameObject gameObject, System.Drawing.Graphics graphicsArgs)
        {
            System.Drawing.Drawing2D.GraphicsPath actualRegion = GetRegion();
            System.Drawing.Drawing2D.GraphicsPath objectRegion = gameObject.GetRegion();

            actualRegion.Transform(GetTransformData());
            objectRegion.Transform(gameObject.GetTransformData());

            System.Drawing.Region region = new System.Drawing.Region(actualRegion);
            region.Intersect(objectRegion);

            return !region.IsEmpty(graphicsArgs);
        }
    }
}
