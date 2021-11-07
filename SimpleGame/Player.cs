using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Игрок
    class Player : GameObject
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

        public void SetHiddenStatus(bool status)
        {
            isHidden = status;
        }


        public bool IsHidden()
        {
            return isHidden;
        }

        public override void Draw(System.Drawing.Graphics drawer)
        {
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.RoyalBlue), -15, -15, 30, 30);
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 3, -10, 5, 5);
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 3, 6, 5, 5);
        }
    }
}
