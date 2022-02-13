using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

using OBBAlgorithm.interfaces;
using OBBAlgorithm.utils;

namespace OBBAlgorithm.shapes
{
    public class AngledRectangle : ISmartObject 
    {
        public bool isColliding;

        public AngledRectangle(Rectangle rect, float speed, ControllerMapping mapping)
            : base(rect, speed, numSides: 4, mapping)
        {
            this.isColliding = false;
        }

        public override void CollisionUpdate(bool status)
        {
            isColliding = status;
        }

        public override void Render()
        {
            Raylib.DrawRectanglePro(rect, center - pos, angle, isColliding ? Color.RED : Color.GRAY);
        }
    }
}