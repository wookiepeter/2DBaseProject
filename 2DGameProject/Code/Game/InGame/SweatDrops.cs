using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace _2DGameProject
{
    class SweatDrops
    {
        CircleShape sprite;
        Vector2 position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2 spawnPosition { get; set; }
        Vector2 move = new Vector2(0F, 0F);
        Vector2 gravity = new Vector2(0F, 2F);

        public SweatDrops(Vector2 vector)
        {
            this.sprite = new CircleShape(10.0F);
            this.position = position;
            this.spawnPosition = spawnPosition;
        }

        public void Update(float deltaTime)
        {
            move += gravity * deltaTime;
            position += move;
            

        }
    }

}
