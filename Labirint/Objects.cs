using System;
using System.Drawing;

namespace Maze
{
    class Objects
    {
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY, CHAR, AID };
        public enum MazeObjectDirection { UP, DOWN, LEFT, RIGHT }

        public Bitmap[] images = {
            new Bitmap(@"C:\Users\3\source\repos\Maze\Maze\bin\Debug\net5.0-windows\Resources\hall.png"),
            new Bitmap(@"C:\Users\3\source\repos\Maze\Maze\bin\Debug\net5.0-windows\Resources\wall.png"),
            new Bitmap(@"C:\Users\3\source\repos\Maze\Maze\bin\Debug\net5.0-windows\Resources\medal.png"),
            new Bitmap(@"C:\Users\3\source\repos\Maze\Maze\bin\Debug\net5.0-windows\Resources\enemy.png"),
            new Bitmap(@"C:\Users\3\source\repos\Maze\Maze\bin\Debug\net5.0-windows\Resources\player.png"),
            new Bitmap(@"C:\Users\3\source\repos\Maze\Maze\bin\Debug\net5.0-windows\Resources\aid.png"),

        };

        public MazeObjectType type;
        public int width;
        public int height;
        private Image _texture;
        public Image texture
        {
            get
            {
                return images[(int)type];
            }
        }

        public Objects(MazeObjectType type)
        {
            this.type = type;
            width = 16;
            height = 16;
            _texture = images[(int)type];
        }

    }
}