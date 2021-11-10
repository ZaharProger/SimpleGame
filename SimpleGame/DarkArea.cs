using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Темная область
    class DarkArea : GameObject
    {
        private byte type;
        public DarkArea() : base()
        {
            type = 1;
        }

        public DarkArea(float x, float y, float angle, byte type) : base(x, y, angle)
        {
            this.type = type;
        }

        public DarkArea(Position position, byte type) : base(position)
        {
            this.type = type;
        }

        public void SetType(byte type)
        {
            this.type = type;
        }

        public byte getType()
        {
            return type;
        }

        public override void Draw(System.Drawing.Graphics drawer)
        {
            drawer.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), -50, -200, 100, 500);
        }

        public override System.Drawing.Drawing2D.GraphicsPath GetRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath objectRegion = base.GetRegion();
            objectRegion.AddRectangle(new System.Drawing.Rectangle(-50, -200, 100, 400));

            return objectRegion;
        }

        public override string ToString()
        {
            return "Черная область";
        }
    }
}
