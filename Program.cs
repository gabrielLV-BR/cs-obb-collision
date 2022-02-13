using System;
using Raylib_cs;
using OBBAlgorithm.shapes;
using OBBAlgorithm.interfaces;
using System.Diagnostics;

namespace OBBAlgorithm
{
    class Program
    {
        // Node[,] nodes;

        ISmartObject[] rects = new ISmartObject[2];

        void Run() {
            {
                ControllerMapping[] mappings = new ControllerMapping[] {
                    new ControllerMapping(
                        left : KeyboardKey.KEY_A,
                        right: KeyboardKey.KEY_D,
                        up   : KeyboardKey.KEY_W,
                        down : KeyboardKey.KEY_S,
                        alt_left: KeyboardKey.KEY_Q,
                        alt_right: KeyboardKey.KEY_E
                    ),
                    new ControllerMapping(
                        left : KeyboardKey.KEY_LEFT,
                        right: KeyboardKey.KEY_RIGHT,
                        up   : KeyboardKey.KEY_UP,
                        down : KeyboardKey.KEY_DOWN,
                        alt_left: KeyboardKey.KEY_RIGHT_SHIFT,
                        alt_right: KeyboardKey.KEY_RIGHT_CONTROL
                    )
                };

                rects[0] = new AngledRectangle(
                    rect: new Rectangle(
                        200f,
                        200f,
                        50f,
                        50f
                    ),
                    speed: 2.5f,
                    mapping: mappings[0]
                );

                rects[1] = new Polygon(
                    rect: new Rectangle(
                        200f,
                        200f,
                        50f,
                        50f
                    ),
                    3,
                    speed: 2.5f,
                    mapping: mappings[1]
                );
            }

            Raylib.InitWindow(500, 500, "OBB Algorithm Test");

            Raylib.SetTargetFPS(60);

            while(!Raylib.WindowShouldClose()) {
                Raylib.BeginDrawing();
                    Raylib.ClearBackground(new Color(200, 210, 220, 255));

                    // Esse loop tá uma bagunça pq ele não é o foco D=<

                    foreach(var rect in rects) {
                        rect.Update();
                    }

                    foreach(var rect1 in rects) {
                        foreach(var rect2 in rects) {
                            if(rect1 == rect2) continue;
                            
                            bool areColliding = OrientedBoundingBox.IsColliding(rect1, rect2);

                            rect1.CollisionUpdate(areColliding); 
                            rect2.CollisionUpdate(areColliding);
                        }
                    }

                    foreach(var rect in rects) {
                        rect.Render();
                    }
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static void Main()
        {
            var program = new Program();
            program.Run();
        }
    }
}
