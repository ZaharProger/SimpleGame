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

        public Action<GameObject> overlapAction;
        public Action<DestinationPoint> destinationPointOverlapAction;
        public Action checkPointOverlapAction;
        public Action darkAreaOverlapPosition;

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

        public void SwitchHiddenStatus()
        {
            if (isHidden)
                isHidden = false;
            else
                isHidden = true;
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
            if (isHidden)
                drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Black), -15, -15, 30, 30);
            else
                drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.RoyalBlue), -15, -15, 30, 30);
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 3, -10, 5, 5);
            drawer.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 3, 6, 5, 5);
        }

        public override System.Drawing.Drawing2D.GraphicsPath GetRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath objectRegion = base.GetRegion();
            objectRegion.AddEllipse(-15, -15, 30, 30);

            return objectRegion;
        }

        public void Overlap(GameObject gameObject)
        {
            if (gameObject != null)
                overlapAction(gameObject);

            if (gameObject is DestinationPoint)
                destinationPointOverlapAction(gameObject as DestinationPoint);
            else if (gameObject is CheckPoint)
                checkPointOverlapAction();
            else if (gameObject is DarkArea)
                darkAreaOverlapPosition();
        }
    }
}
