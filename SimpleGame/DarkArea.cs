using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Темная область
    class DarkArea : GameObject
    {
        public Action<Player> playerOverlapAction;

        public DarkArea() : base()
        {}

        public DarkArea(float x, float y, float angle) : base(x, y, angle)
        {}

        public DarkArea(Position position) : base(position)
        {}

        public override void Draw(System.Drawing.Graphics drawer)
        {
            base.Draw(drawer);
            drawer.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), -100, -200, 200, 400);
        }

        public override System.Drawing.Drawing2D.GraphicsPath GetRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath objectRegion = base.GetRegion();
            objectRegion.AddRectangle(new System.Drawing.Rectangle(-100, -200, 200, 400));

            return objectRegion;
        }

        public override string ToString()
        {
            return "Черная область";
        }

        public override void Overlap(GameObject gameObject)
        {
            if (gameObject is Player)
                playerOverlapAction(gameObject as Player);

            base.Overlap(gameObject);
        }
    }
}
