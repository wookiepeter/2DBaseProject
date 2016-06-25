using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
    class SweatDrop
    {
        public CircleShape sprite;
        Vector2 position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2 move = new Vector2(0F, 0F);
        Vector2 gravity = new Vector2(0F, 2F);

        public SweatDrop(Vector2 spawnPosition)
        {
            this.sprite = new CircleShape(10.0F);
            this.position = spawnPosition;
            this.sprite.FillColor = new Color(200, 250, 250);
        }

        public void Update(float deltaTime)
        {
            move += (gravity * deltaTime)/2;
            position += move/2;
            
        }

        public void bounceOff(Vector2 collisionPoint)
        {
            move = (position+sprite.Radius*Vector2.One - collisionPoint).normalized;
        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }


