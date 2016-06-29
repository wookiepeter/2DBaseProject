using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using GameProject2D;

class SweatDrop
{
    public CircleShape circle;
    Sprite sprite;
    Vector2 position { get { return circle.Position; } set { circle.Position = value; } }
    Vector2 move = new Vector2(0F, 0F);
    Vector2 gravity = new Vector2(0F, 2F);

    public SweatDrop(Vector2 spawnPosition)
    {
        sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Drop));
        sprite.Origin = new Vector2(sprite.Texture.Size.X / 2-5, sprite.Texture.Size.Y / 2);
        sprite.Scale = sprite.Scale * 0.3F;
        this.circle = new CircleShape(10.0F);
        this.position = spawnPosition;
        this.circle.FillColor = new Color(250, 0, 0);

    }

    public void Update(float deltaTime)
    {
        move += (gravity * deltaTime) /2;
        position += move;
        //position = new Vector2((position.X + Program.win.Size.X) % (Program.win.Size.X), position.Y);

        sprite.Rotation = Helper.RadianToDegree*(float)Math.Atan2(-move.X, move.Y);

        float sideBuffer = 0;
        float actualFieldSize = Program.win.Size.X + 2 * sideBuffer;
        position = new Vector2((position.X + sideBuffer + actualFieldSize) % (actualFieldSize) - sideBuffer, position.Y);

    }

    public void bounceOff(Vector2 collisionPoint)
    {
        move = (position + circle.Radius * Vector2.One - collisionPoint).normalized;
        position += move;
    }

    public void draw(RenderWindow win, View view)
    {
        sprite.Position = position + Vector2.One * circle.Radius;
        Vector2 realPositoin = sprite.Position;
        win.Draw(sprite);
        
        if(sprite.Position.X < 0)
        {
            sprite.Position = new Vector2(Program.win.Size.X + (-sprite.Position.X), sprite.Position.Y);
        }
        if(sprite.Position.X > Program.win.Size.X)
        {
            sprite.Position = new Vector2(-(sprite.Position.X-Program.win.Size.X), sprite.Position.Y);
        }

        win.Draw(sprite);

        sprite.Position = realPositoin;

        // win.Draw(circle);
    }
}


