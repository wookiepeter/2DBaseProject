using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace GameProject2D
{
    class InGameState : IGameState
    {
        Player player;
        Player player2;
        Background background;
        List<Plant> plants;
        Vector2 collisionPoint;
        //SweatDrops drops;
        List<SweatDrop> drops;
        float countdown;


        public InGameState()
        {
            player = new Player(new Vector2f(50F, 10F),1);
            player2 = new Player(new Vector2f(680F, 10F),2); //neuer Spieler erstellt
            background = new Background();
            plants = new List<Plant>();

            plants.Add(new Plant(25F));
            plants.Add(new Plant(200F));
            plants.Add(new Plant(550F));
            plants.Add(new Plant(700F));

            drops = new List<SweatDrop>();
            

        }

        public GameState Update(float deltaTime)
        {
            player.update(deltaTime);
            player2.update(deltaTime);
            Console.WriteLine(countdown);

            if (countdown <= 0)
            {
       
                countdown = 1.0f;
                drops.Add(new SweatDrop(new Vector2(Rand.Value(0.5F, Program.win.Size.X-0.5F), -100.0F)));

            }
            countdown -= deltaTime;

            List<SweatDrop> cachedForDelete = new List<SweatDrop>();
            foreach (SweatDrop drop in drops)
            {
                drop.Update(deltaTime);
                if (DoCollide(player.sprite, drop.sprite, out collisionPoint))
                {
                    drop.bounceOff(collisionPoint);
                }

                if (DoCollide(player2.sprite, drop.sprite, out collisionPoint))
                {
                    drop.bounceOff(collisionPoint);
                }
                if (drop.sprite.Position.X < 0 || drop.sprite.Position.X > Program.win.Size.X || drop.sprite.Position.Y > Program.win.Size.Y)
                {
                    cachedForDelete.Add(drop);
                }
            }

            foreach(SweatDrop drop in cachedForDelete)
            {
                drops.Remove(drop);
            }

            return GameState.InGame;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            background.Draw(win, view);
            foreach (Plant t in plants) //t - variable
            {
                t.Draw(win, view);
            }
            player.draw(win, view);
            player2.draw(win, view);
            foreach (SweatDrop drop in drops)
            {
                drop.draw(win, view);
            }
            background.Draw(win, view);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }
        private bool DoCollide(CircleShape a, CircleShape b, out Vector2 collisionPoint)
        {
            Vector2 midPointA = (Vector2) a.Position + a.Radius * Vector2.One;
            Vector2 midPointB = (Vector2) b.Position + b.Radius * Vector2.One;

            Vector2 deltaA = midPointB - midPointA; //Streckenlänge berechnen
            Vector2 deltaB = midPointA - midPointB;
            float radiusSum = a.Radius + b.Radius;

            if (deltaA.lengthSqr <= radiusSum * radiusSum) //radiusSumme quadrieren, um Math.sqrt zu umgehen
            {
                //Schnittpunkt = Kreismittelpunkt + r * Vektor mit Länge 1
                Vector2 pointA = (Vector2)midPointA + a.Radius * deltaA.normalized;
                Vector2 pointB = (Vector2)midPointB + b.Radius * deltaB.normalized;
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
