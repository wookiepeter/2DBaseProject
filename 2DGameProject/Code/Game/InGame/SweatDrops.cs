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
        Vector2 fallDown = new Vector2(0F, 2F);

        public SweatDrops(Vector2 vector)
     {
            this.sprite = new CircleShape();
            this.position = position;
     }
        
    }

}
