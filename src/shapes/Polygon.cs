using OBBAlgorithm.interfaces;
using Raylib_cs;

namespace OBBAlgorithm.shapes
{
    public class Polygon : ISmartObject
    {
        bool isColliding;
        public Polygon(Rectangle rect, int sides, float speed, ControllerMapping mapping) 
        : base(rect, speed, sides, mapping)
        {
            isColliding = false;
        }

        public override void CollisionUpdate(bool status)
        {
            isColliding = status;
        }

        public override void Render()
        {
            var vertices = GetVertices();
            for (int i = 0; i < numSides; i++) {
                Raylib.DrawLineV(
                    vertices[i],
                    vertices[(i + 1) % numSides],
                    isColliding ? Color.RED : Color.GRAY
                );
            }
        }
    }
}