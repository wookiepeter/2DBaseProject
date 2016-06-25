using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class Background
    {
        RectangleShape island;
        RectangleShape island2;
      

        public Background()
        {
            this.island = new RectangleShape(new Vector2f (Program.win.Size.X*0.4F,35F));
            this.island.Position = new Vector2f(Program.win.Size.X * 0.03F, Program.win.Size.Y * 0.7F);
            this.island.FillColor = Color.Black;
            this.island2 = new RectangleShape(new Vector2f(Program.win.Size.X * 0.4F, 35F));
            this.island2.Position = new Vector2f(((1F-0.43F)*Program.win.Size.X), Program.win.Size.Y * 0.7F);
            this.island2.FillColor = Color.Black;
        }

    

        public void Draw(RenderWindow win, View view)
        {
            win.Draw(island);
            win.Draw(island2);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }
    }
}
