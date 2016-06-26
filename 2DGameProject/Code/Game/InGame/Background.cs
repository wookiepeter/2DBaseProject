using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class Background
    {
        RectangleShape rect;
        RectangleShape rect2;
        Sprite island;
        Sprite island2;
        Sprite background;

        public Background()
        {
            this.rect = new RectangleShape(new Vector2f (Program.win.Size.X*0.4F,35F));
            this.rect.Position = new Vector2f(Program.win.Size.X * 0.03F, Program.win.Size.Y * 0.7F);
            this.rect.FillColor = Color.Black;
            this.rect2 = new RectangleShape(new Vector2f(Program.win.Size.X * 0.4F, 35F));
            this.rect2.Position = new Vector2f(((1F-0.43F)*Program.win.Size.X), Program.win.Size.Y * 0.7F);
            this.rect2.FillColor = Color.Black;

            island = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Island));
            island.Position = new Vector2f(Program.win.Size.X * 0.03F, Program.win.Size.Y * 0.67F);
            
            island2 = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Island));
            island2.Position = new Vector2f(((1F - 0.43F) * Program.win.Size.X), Program.win.Size.Y * 0.67F);

            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.ingameBackGround));
        }

    

        public void Draw(RenderWindow win, View view)
        {
            //win.Draw(rect);
            //win.Draw(rect2);

            win.Draw(background);

            win.Draw(island);
            win.Draw(island2);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }
    }
}
