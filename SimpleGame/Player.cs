using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGame
{
    //Игрок
    class Player : GameObject
    {
        private bool isHidden;
        private float speedX;
        private float speedY;

        public Action<DestinationPoint> DestinationPointOverlap;

        public Player() : base()
        {
            isHidden = false;
            speedX = 0;
            speedY = 0;
        }

        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            isHidden = false;
            speedX = 0;
            speedY = 0;
        }

        public Player(Position position) : base(position)
        {
            isHidden = false;
            speedX = 0;
            speedY = 0;
        }

        public void SetHiddenStatus(bool status)
        {
            isHidden = status;
        }

        public void SetSpeedX(float speedX)
        {
            this.speedX = speedX;
        }

        public void SetSpeedY(float speedY)
        {
            this.speedY = speedY;
        }

        public float GetSpeedX()
        {
            return speedX;
        }

        public float GetSpeedY()
        {
            return speedY;
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

        public override void Overlaps(GameObject gameObject)
        {
            base.Overlaps(gameObject);
            if (gameObject is DestinationPoint)
                DestinationPointOverlap(gameObject as DestinationPoint);
        }
    }
}
