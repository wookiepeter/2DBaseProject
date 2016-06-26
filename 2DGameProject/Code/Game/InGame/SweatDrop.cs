﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
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
        sprite.Origin = (Vector2)sprite.Texture.Size / 2;
        sprite.Scale = sprite.Scale * 1F;
        this.circle = new CircleShape(10.0F);
        this.position = spawnPosition;
        this.circle.FillColor = new Color(200, 250, 250);
    }

    public void Update(float deltaTime)
    {
        move += (gravity * deltaTime) / 2;
        position += move;

    }

    public void bounceOff(Vector2 collisionPoint)
    {
        move = (position + circle.Radius * Vector2.One - collisionPoint).normalized;
    }

    public void draw(RenderWindow win, View view)
    {
        sprite.Position = position + Vector2.One * circle.Radius;
        win.Draw(sprite);
        win.Draw(circle);
    }
}


