using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
     class Plant
    {
        
        Sprite sprite;
        float SpriteWidth { get { return sprite.Texture.Size.X * sprite.Scale.X; } }
        float SpriteHeigh { get { return sprite.Texture.Size.Y * sprite.Scale.Y; } }

        public Plant(float x)
        {
            AssetManager.GetTexture(AssetManager.TextureName.Crop); //greift auf die Texture zu
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Crop));
            this.sprite.Position = new Vector2f(x, (Program.win.Size.Y * 0.7F)- SpriteHeigh);
            sprite.Scale = new Vector2f(0.5F, 1F);
            
        }

    

        public void Draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }
    }
}
