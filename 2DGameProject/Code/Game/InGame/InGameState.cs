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
        public static bool winnerOne;


        public InGameState()
        {
            player = new Player(new Vector2f(50F, 10F),1);
            player2 = new Player(new Vector2f(680F, 10F),2); //neuer Spieler erstellt
            background = new Background();

            Plant randomPlant = new Plant(-100);
            plants = new List<Plant>();

            int plantCount = 5;
            List<float> left = new List<float>();
            List<float> right = new List<float>();
            for (int i = 0; i < plantCount; i++)
            {
                bool canPlace = true;
                float place = (Rand.Value(Program.win.Size.X * 0.03F, Program.win.Size.X * 0.43F - randomPlant.SpriteWidth));
                foreach (float l in left)
                {
                    if (Math.Abs(place - l) < randomPlant.SpriteWidth)
                    {
                        canPlace = false;
                        break;
                    }
                }
                if (canPlace)
                {
                    plants.Add(new Plant(place));
                    left.Add(place);
                }
                else i--;
            }

            for (int j = 0; j < plantCount; j++)
            {
                bool canPlace = true;
                float place = Rand.Value((1F - 0.43F) * Program.win.Size.X, Program.win.Size.X * (1F - 0.03F) - randomPlant.SpriteWidth);
                foreach (float r in right)
                {
                    if (Math.Abs(place - r) < randomPlant.SpriteWidth)
                    {
                        canPlace = false;
                        break;
                    }
                }
                if (canPlace)
                {
                    plants.Add(new Plant(place));
                    right.Add(place);
                }
                else j--;
            }

           /* plants.Add(new Plant(Rand.Value(Program.win.Size.X*0.03F, Program.win.Size.X*0.43F - randomPlant.SpriteWidth)));
            plants.Add(new Plant(Rand.Value(Program.win.Size.X * 0.03F, Program.win.Size.X * 0.43F - randomPlant.SpriteWidth)));
            plants.Add(new Plant(Rand.Value((1F - 0.43F) * Program.win.Size.X, Program.win.Size.X * (1F -0.03F) - randomPlant.SpriteWidth)));
            plants.Add(new Plant(Rand.Value((1F - 0.43F) * Program.win.Size.X, Program.win.Size.X * (1F - 0.03F) - randomPlant.SpriteWidth)));
            plants.Add(new Plant(200F));
            plants.Add(new Plant(550F));
            plants.Add(new Plant(700F));*/

            drops = new List<SweatDrop>();
            

        }

        public GameState Update(float deltaTime)
        {
            player.update(deltaTime);
            player2.update(deltaTime);
            //Console.WriteLine(GamePadInputManager.GetLeftStick(0).Y);

            if (countdown <= 0)
            {
                countdown = 1.0f;
                drops.Add(new SweatDrop(new Vector2(Rand.Value(1.0F, Program.win.Size.X-1.0F), -100.0F)));

            }
            countdown -= deltaTime;

            List<SweatDrop> cachedForDelete = new List<SweatDrop>();
            foreach (SweatDrop drop in drops)
            {
                drop.Update(deltaTime);
                if (DoCollide(player.circle, drop.sprite, out collisionPoint))
                {
                    drop.bounceOff(collisionPoint);
                }

                if (DoCollide(player2.circle, drop.sprite, out collisionPoint))
                {
                    drop.bounceOff(collisionPoint);
                }

                if (drop.sprite.Position.X < 0 || drop.sprite.Position.X > Program.win.Size.X || drop.sprite.Position.Y > Program.win.Size.Y)
                {
                    cachedForDelete.Add(drop);
                }

                for (int i = 0; i < plants.Count; i++)
                {
                   foreach(CircleShape cs in plants[i].collider)
                    {
                        if (DoCollide(drop.sprite, cs, out collisionPoint))
                        {

                            plants[i].getHit();
                            cachedForDelete.Add(drop);
                        }
                    }
                }
            }


            foreach (SweatDrop drop in cachedForDelete)
            {
                drops.Remove(drop);
            }
            foreach(Plant plant in plants)
            {
                plant.update(deltaTime);
            }

            int lifeLeft = 0;
            int lifeRight = 0;

            for (int i = 0; i < plants.Count; i++)
            {
                if(plants[i].variable < Program.win.Size.X * 0.5f)
                {
                    lifeLeft += plants[i].Life;
                }
                else
                {
                    lifeRight += plants[i].Life;
                }
            }

            if (lifeRight == 0)
            {
                winnerOne = true;
                return GameState.EndScreen;
            }

            if (lifeLeft == 0)
            {
                winnerOne = false;
                return GameState.EndScreen;
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
            player.draw(win, view, deltaTime);
            player2.draw(win, view, deltaTime);
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
