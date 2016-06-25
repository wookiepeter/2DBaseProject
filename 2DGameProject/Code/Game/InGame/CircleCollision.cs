using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace _2DGameProject
{
    public class CircleCollision
    {
        CircleShape sprite;
        Vector2 position { get { return sprite.Position; } set { sprite.Position = value; } }

        public CircleCollision(Vector2 vector)
        {
            this.sprite = new CircleShape();
            this.position = position;

            Vector2 collisionPoint;
            if(DoCollide(sprite, sprite, out collisionPoint))
            {
                
            }
        }

        private bool DoCollide(CircleShape a, CircleShape b, out Vector2 collisionPoint)
        {
            Vector2 deltaA = b.Position - a.Position; //Streckenlänge berechnen
            Vector2 deltaB = a.Position - b.Position;
            float radiusSum = a.Radius + b.Radius;

            if (deltaA.lengthSqr <= radiusSum * radiusSum) //radiusSumme quadrieren, um Math.sqrt zu umgehen
            {
                //Schnittpunkt = Kreismittelpunkt + r * Vektor mit Länge 1
                Vector2 pointA = (Vector2)a.Position + a.Radius * deltaA.normalized;
                Vector2 pointB = (Vector2)b.Position + b.Radius * deltaB.normalized;
                collisionPoint = (pointB + pointA) / 2;
                return true;
            }
            else
            {
                collisionPoint = Vector2.Zero; //Random Wert
                return false;
            }
        }
        
    }
}
