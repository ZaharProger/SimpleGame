using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Класс точки прибытия игрока
    class DestinationPoint : GameObject
    {
        public DestinationPoint() : base()
        {}

        public DestinationPoint(float x, float y, float angle) : base(x, y, angle)
        {}

        public DestinationPoint(Position position) : base(position)
        {}

        public override string ToString()
        {
            return "Метка";
        }

        public override void Draw(System.Drawing.Graphics drawer)
        {
            base.Draw(drawer);
            if (isGlow)
            {
                drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.White), -2.5f, -2.5f, 5, 5);
                drawer.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.White, 2), -5, -5, 10, 10);
            }
            else
            {
                drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Red), -2.5f, -2.5f, 5, 5);
                drawer.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Red, 2), -5, -5, 10, 10);
            }
        }

        public override System.Drawing.Drawing2D.GraphicsPath GetRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath objectRegion = base.GetRegion();
            objectRegion.AddEllipse(-5, -5, 10, 10);

            return objectRegion;
        }
    }
}
