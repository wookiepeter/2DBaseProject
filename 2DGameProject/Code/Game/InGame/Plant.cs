using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace GameProject2D
{
     class Plant
    {
        
        Sprite sprite;
        List<CircleShape> collider = new List<CircleShape>();
        float SpriteWidth { get { return sprite.Texture.Size.X * sprite.Scale.X; } }
        float SpriteHeigh { get { return sprite.Texture.Size.Y * sprite.Scale.Y; } }
        float Life;

        public Plant(float x)
        {
            AssetManager.GetTexture(AssetManager.TextureName.Crop); //greift auf die Texture zu
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Crop));
            this.sprite.Position = new Vector2f(x, (Program.win.Size.Y * 0.7F)- SpriteHeigh);
            sprite.Scale = new Vector2f(0.5F, 1F);

            for (int i = 0; i < 4; i++)
            {
                collider.Add(new CircleShape(SpriteWidth / 2));
                this.collider[i].FillColor = Color.Yellow;
                this.collider[i].Position = new Vector2f(x, (Program.win.Size.Y * 0.7F) - collider[i].Radius * ((i+1)*2));
            }
         
            this.Life = 100;
            
        }

    

        public void Draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
            foreach (CircleShape t in collider) //t - variable
            {
               win.Draw(t);
            }
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }
    }
}
