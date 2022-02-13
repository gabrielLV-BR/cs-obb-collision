using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

using OBBAlgorithm.utils;

namespace OBBAlgorithm.interfaces {
    public abstract class ISmartObject {
        protected int numSides;
        protected Rectangle rect; 
        protected float speed, angle;
        protected ControllerMapping mapping;

        // Propriedades calculadas
        public Vector2 pos {
            get { return new Vector2(rect.x, rect.y); }
            set { 
                rect.x = value.X;
                rect.y = value.Y;
            }
        }

        public Vector2 size {
            get { return new Vector2(rect.width, rect.height); }
        }

        public Vector2 center {
            get { return pos + size / 2; }
        }

        // Construtor
        public ISmartObject(Rectangle rect, float speed, int numSides, ControllerMapping mapping)
        {
            this.numSides = numSides;
            this.rect = rect;
            this.speed = speed;
            this.angle = 0f;
            this.mapping = mapping;
        }
        
        // Método base pra todo mundo se mover igual
        public void Move()
        {
            var movement = Vector2.Zero;

            if (Raylib.IsKeyDown(mapping.LEFT))  movement.X -= 1f;
            if (Raylib.IsKeyDown(mapping.RIGHT)) movement.X += 1f;
            if (Raylib.IsKeyDown(mapping.DOWN))  movement.Y += 1f;
            if (Raylib.IsKeyDown(mapping.UP))    movement.Y -= 1f;

            // Console.Out.WriteLine($"X = {movementX}, Y = {movementY}");

            pos += movement * speed;

            float rotationSpeed = 0f;

            if (Raylib.IsKeyDown(mapping.ALT_LEFT))  rotationSpeed -= 1f;
            if (Raylib.IsKeyDown(mapping.ALT_RIGHT)) rotationSpeed += 1f;

            angle += (rotationSpeed * 100f) * Globals.toRadians;
        }

        public void Update() { Move(); }
        
        public abstract void Render();
        public abstract void CollisionUpdate(bool status);

        public virtual List<Vector2> GetVertices() {
            var points = new List<Vector2>(numSides);
            float angleStep = 360f / numSides;
            Vector2 localDown = new Vector2(rect.width / 2f, rect.height / 2f - 1f);

            for (int i = 0; i < numSides; i++)
            {
                Vector2 rotatedDown = VectorUtils.Rotate(
                    localDown,
                    angle + angleStep * i
                ) + pos;

                points.Add(rotatedDown);
            }

            return points;
        }
        
        public List<Vector2> GetEdges()
        {
            var vertices = GetVertices();
            int numVertices = vertices.Count;
            var edges = new List<Vector2>(numVertices);

            for (int i = 0; i < numVertices; i++)
            {
                edges.Add(vertices[(i + 1) % numVertices] - vertices[i]);
            }

            return edges;
        }
    }
}