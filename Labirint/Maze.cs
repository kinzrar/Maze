using System;
using System.Windows.Forms;
using System.Drawing;

namespace Maze
{
    class Maze
    {
        public int height; 
        public int width; 

        public Objects[,] maze;
        public PictureBox[,] images;

        public static Random random = new Random();
        public Form parent;

        Point charCoords;

        private int MedalsGenerated = 0;
        private int MedalsCollected = 0;

        private int Health = 100;

        public Maze(Form parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;

            maze = new Objects[height, width];
            images = new PictureBox[height, width];

            Generate();
        }

        private void Generate()
        {
            int smileX = 0;
            int smileY = 2;

            charCoords = new Point(smileX, smileY);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Objects.MazeObjectType current = Objects.MazeObjectType.HALL;

                    if (random.Next(5) == 0)
                    {
                        current = Objects.MazeObjectType.WALL;
                    }

                    var number = random.Next(250);
                    if (number == 0)
                    {
                        current = Objects.MazeObjectType.MEDAL;
                        MedalsGenerated++;
                    }

                    else if (number == 1)
                    {
                        current = Objects.MazeObjectType.ENEMY;
                    }
                    else if (number == 2)
                    {
                        current = Objects.MazeObjectType.AID;
                    }

                    if (y == 0 || x == 0 || y == height - 1 | x == width - 1)
                    {
                        current = Objects.MazeObjectType.WALL;
                    }

                    if (x == smileX && y == smileY)
                    {
                        current = Objects.MazeObjectType.CHAR;

                    }

                    if (x == smileX + 1 && y == smileY || x == width - 1 && y == height - 3)
                    {
                        current = Objects.MazeObjectType.HALL;
                    }

                    maze[y, x] = new Objects(current);
                    images[y, x] = new PictureBox();
                    images[y, x].Location = new Point(x * maze[y, x].width, y * maze[y, x].height);
                    images[y, x].Parent = parent;
                    images[y, x].Width = maze[y, x].width;
                    images[y, x].Height = maze[y, x].height;
                    images[y, x].BackgroundImage = maze[y, x].texture;
                    images[y, x].Visible = false;
                }
            }
        }

        public void Show()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    images[y, x].Visible = true;
                }
            }
        }

        public bool IsMoveAvailable(Objects.MazeObjectDirection dir)
        {
            int x = charCoords.X;
            int y = charCoords.Y;

            switch (dir)
            {
                case Objects.MazeObjectDirection.UP:
                    y--;
                    break;
                case Objects.MazeObjectDirection.DOWN:
                    y++;
                    break;
                case Objects.MazeObjectDirection.LEFT:
                    x--;
                    break;
                case Objects.MazeObjectDirection.RIGHT:
                    x++;
                    break;
            }
            try
            {
                switch (maze[y, x].type)
                {
                    case Objects.MazeObjectType.HALL:
                        return true;
                    case Objects.MazeObjectType.MEDAL:
                        MedalsCollected++;
                        return true;
                    case Objects.MazeObjectType.AID:
                        if (Health >= 100)
                        {
                            return false;
                        }
                        else
                        {
                            Health += 5;
                            if (Health > 100)
                            {
                                Health = 100;
                            }
                            return true;
                        }
                    case Objects.MazeObjectType.ENEMY:
                        Health -= random.Next(20, 25);
                        return true;
                    default:
                        return false;
                }

            }
            catch
            {
                return false;
            }
        }

        public bool GameStatusCheck()
        {
            parent.Text = "Health: " + Health + "%" + " Medals: " + MedalsCollected + "/" + MedalsGenerated;

            if (MedalsCollected == MedalsGenerated && MedalsGenerated != 0)
            {
                MessageBox.Show("You've collected all medals!\n\nYou won!");
                return false;
            }
            if (Health <= 0)
            {
                MessageBox.Show("You died!\n\nGame Over!");
                return false;
            }
            return true;
        }

        public void Move(Objects.MazeObjectDirection dir)
        {
            if (IsMoveAvailable(dir))
            {
                switch (dir)
                {
                    case Objects.MazeObjectDirection.UP:
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.HALL;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        charCoords.Y--;
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.CHAR;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        break;
                    case Objects.MazeObjectDirection.DOWN:
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.HALL;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        charCoords.Y++;
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.CHAR;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        break;
                    case Objects.MazeObjectDirection.LEFT:
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.HALL;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        charCoords.X--;
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.CHAR;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        break;
                    case Objects.MazeObjectDirection.RIGHT:
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.HALL;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        charCoords.X++;
                        maze[charCoords.Y, charCoords.X].type = Objects.MazeObjectType.CHAR;
                        images[charCoords.Y, charCoords.X].BackgroundImage = maze[charCoords.Y, charCoords.X].texture;
                        break;
                }
            }
            GameStatusCheck();
        }
    }
}