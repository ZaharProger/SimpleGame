using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Игрок
    class Player : Entity
    {
        private bool isHidden;

        public Player() : base()
        {
            isHidden = false;
        }

        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            isHidden = false;
        }

        public Player(Position position) : base(position)
        {
            isHidden = false;
        }

        public bool IsHidden()
        {
            return isHidden;
        }

        public override void Draw(System.Drawing.Graphics drawer)
        {
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.RoyalBlue), position.GetX(), position.GetY(), 20, 20);
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Red), position.GetX(), position.GetY() - 10, 5, 5);
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Red), position.GetX(), position.GetY() + 10, 5, 5);
        }
    }
}
