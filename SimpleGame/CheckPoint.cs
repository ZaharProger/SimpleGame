using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Класс контрольной точки
    class CheckPoint : GameObject
    {
        private float scale;

        public CheckPoint() : base()
        {
            scale = 1;
        }

        public CheckPoint(float x, float y, float angle) : base(x, y, angle)
        {
            scale = 1;
        }

        public CheckPoint(Position position) : base(position)
        {
            scale = 1;
        }

        public void SetScale(float scale)
        {
            this.scale = scale;
        }

        public float GetScale()
        {
            return scale;
        }

        public override void Draw(System.Drawing.Graphics drawer)
        {
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.LimeGreen), -15, -15, 30, 30);
            drawer.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.LimeGreen, 2), -20, -20, 40, 40);
        }


        public override System.Drawing.Drawing2D.GraphicsPath GetRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath objectRegion = base.GetRegion();
            objectRegion.AddEllipse(-20, -20, 40, 40);

            return objectRegion;
        }

        public override System.Drawing.Drawing2D.Matrix GetTransformData()
        {
            System.Drawing.Drawing2D.Matrix matrix = base.GetTransformData();
            matrix.Scale(scale, scale);

            return matrix;
        }

        public override string ToString()
        {
            return "Контрольная точка";
        }
    }
}
