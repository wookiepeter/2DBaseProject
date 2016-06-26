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
        public List<CircleShape> collider = new List<CircleShape>();
        public float SpriteWidth { get { return sprite.Texture.Size.X * sprite.Scale.X; } }
        float SpriteHeigh { get { return sprite.Texture.Size.Y * sprite.Scale.Y; } }
        public int Life;
        public float variable;

        List<CircleShape> cachedForDelete = new List<CircleShape>();

        public Plant(float x)
        {
            variable = x;
            AssetManager.GetTexture(AssetManager.TextureName.Crop4); //greift auf die Texture zu
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Crop4));
            this.sprite.Position = new Vector2f(x, (Program.win.Size.Y * 0.7F)- SpriteHeigh);
            sprite.Scale = new Vector2f(0.8F, 1F);
            this.Life = 4;
            for (int i = 0; i < Life; i++)
            {
                collider.Add(new CircleShape(SpriteWidth / 2));
                this.collider[i].FillColor = Color.Green;
                this.collider[i].Position = new Vector2f(x, (Program.win.Size.Y * 0.7F) - collider[i].Radius * ((i+1)*2));
            }
         
            
            
        }
       
        public void update (float deltaTime)
        {
           foreach (CircleShape cl in cachedForDelete)
            {
                collider.Remove(cl);
            }
                /* for (int i = 0; i < cachedForDelete.Count; i++)
            {
                collider.RemoveAt(i);
            }*/
        }

       
        public void getHit()
        {
            Life -= 1;
            cachedForDelete.Add(collider[collider.Count - 1]);

            ChangeTexture();
        }

        private void ChangeTexture()
        {
            switch (Life)
            {
                case 4:
                    sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.Crop4);
                    break;

                case 3:
                    sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.Crop3);
                    break;

                case 2:
                    sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.Crop2);
                    break;

                case 1:
                    sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.Crop);
                    break;

                default:
                    sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.WhitePixel);
                    break;
            }
        }

        public void Draw(RenderWindow win, View view)
        {
            if(Life > 0)
                win.Draw(sprite);
            /* foreach (CircleShape t in collider) //t - variable
            {
               win.Draw(t);
            }*/
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }

    }
}
