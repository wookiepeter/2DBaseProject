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
            this.island = new RectangleShape(new Vector2f(300F, 45F));
            this.island.Position = new Vector2f(0F, Program.win.Size.Y * 0.8F);
            this.island.FillColor = Color.Black;
            this.island2 = new RectangleShape(new Vector2f(300F, 45F));
            this.island2.Position = new Vector2f(500F, Program.win.Size.Y * 0.8F);
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
