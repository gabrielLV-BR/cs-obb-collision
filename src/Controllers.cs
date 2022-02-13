using Raylib_cs;

namespace OBBAlgorithm {
    public class ControllerMapping {
        public KeyboardKey LEFT, RIGHT, UP, DOWN, ALT_LEFT, ALT_RIGHT;

        public ControllerMapping(
            KeyboardKey left, 
            KeyboardKey right, 
            KeyboardKey up, 
            KeyboardKey down,
            KeyboardKey alt_left,
            KeyboardKey alt_right
        )
        {
            LEFT      = left;
            RIGHT     = right;
            UP        = up;
            DOWN      = down;
            ALT_LEFT  = alt_left;
            ALT_RIGHT = alt_right;
        }
    }
}