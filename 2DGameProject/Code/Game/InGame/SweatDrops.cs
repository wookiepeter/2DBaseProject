using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
    class SweatDrops
    {
        public CircleShape sprite;
        Vector2 position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2 move = new Vector2(0F, 0F);
        Vector2 gravity = new Vector2(0F, 2F);

        public SweatDrops(Vector2 spawnPosition)
        {
            this.sprite = new CircleShape(10.0F);
            this.position = spawnPosition;
        this.sprite.FillColor = new Color(150, 5, 2);
        }

        public void Update(float deltaTime)
        {
            move += gravity * deltaTime;
            position += move;
            
        }

        public void bounceOff(Vector2 collisionPoint)
        {
            move = (position - collisionPoint).normalized*20.0F;
        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }


