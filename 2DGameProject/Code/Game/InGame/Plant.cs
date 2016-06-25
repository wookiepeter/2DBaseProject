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
        public Plant()
        {
            AssetManager.GetTexture(AssetManager.TextureName.Crop); //greift auf die Texture zu
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Crop));
            this.sprite.Position = new Vector2f(50F, (Program.win.Size.Y * 0.8F)- SpriteHeigh);
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
