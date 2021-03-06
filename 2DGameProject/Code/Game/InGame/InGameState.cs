﻿using System;
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

        Sprite LifeSprite;
        readonly int PlantsPerPlayer = 5;
        int lifeLeft = 5;
        int lifeRight = 5;

        int GameOverLife = 2;

        public InGameState()
        {
            player = new Player(new Vector2f(50F, 10F),1);
            player2 = new Player(new Vector2f(680F, 10F),2); //neuer Spieler erstellt
            background = new Background();

            Plant randomPlant = new Plant(-100);
            plants = new List<Plant>();
            
            List<float> left = new List<float>();
            List<float> right = new List<float>();
            for (int i = 0; i < PlantsPerPlayer; i++)
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

            for (int j = 0; j < PlantsPerPlayer; j++)
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

            LifeSprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Blossom));
            LifeSprite.Origin = 0.5F * (Vector2)LifeSprite.Texture.Size;
        }

        public GameState Update(float deltaTime)
        {
            player.update(deltaTime);
            player2.update(deltaTime);
            //Console.WriteLine(GamePadInputManager.GetLeftStick(0).Y);

            if (countdown <= 0)
            {
                countdown = 1.0f;
                drops.Add(new SweatDrop(new Vector2(Rand.Value(1.0F, Program.win.Size.X), -100.0F)));

            }
            countdown -= deltaTime;

            List<SweatDrop> cachedForDelete = new List<SweatDrop>();
            foreach (SweatDrop drop in drops)
            {
                drop.Update(deltaTime);
                if (DoCollide(player.circle, drop.circle, out collisionPoint))
                {
                    drop.bounceOff(collisionPoint);
                }

                if (DoCollide(player2.circle, drop.circle, out collisionPoint))
                {
                    drop.bounceOff(collisionPoint);
                }

                if (drop.circle.Position.X < 0 || drop.circle.Position.X > Program.win.Size.X || drop.circle.Position.Y > Program.win.Size.Y)
                {
                    cachedForDelete.Add(drop);
                }

                for (int i = 0; i < plants.Count; i++)
                {
                   foreach(CircleShape cs in plants[i].collider)
                    {
                        if (DoCollide(drop.circle, cs, out collisionPoint))
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

            lifeLeft = 0;
            lifeRight = 0;

            for (int i = 0; i < plants.Count; i++)
            {
                if(plants[i].variable < Program.win.Size.X * 0.5f)
                {
                    lifeLeft += (int)Helper.Clamp(plants[i].Life, 0, 1);
                }
                else
                {
                    lifeRight += (int)Helper.Clamp(plants[i].Life, 0, 1);
                }
            }

            if (lifeRight <= GameOverLife)
            {
                winnerOne = true;
                return GameState.EndScreen;
            }

            if (lifeLeft <= GameOverLife)
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
            
            LifeSprite.Rotation += deltaTime * 16;
            for (int i = 0; i < PlantsPerPlayer - GameOverLife; i++)
            {
                LifeSprite.Color = i < lifeLeft - GameOverLife ? Color.White : Color.Black;

                LifeSprite.Position = new Vector2(win.Size.X * 0.05F + win.Size.X * 0.05F * i, win.Size.Y * 0.1F);
                win.Draw(LifeSprite);
            }
            LifeSprite.Rotation = -LifeSprite.Rotation;
            for (int i = 0; i < PlantsPerPlayer - GameOverLife; i++)
            {
                LifeSprite.Color = i < lifeRight - GameOverLife ? Color.White : Color.Black;
                LifeSprite.Position = new Vector2(win.Size.X * (1F - 0.05F) - win.Size.X * 0.05F * i, win.Size.Y * 0.1F);
                win.Draw(LifeSprite);
            }
            LifeSprite.Rotation = -LifeSprite.Rotation;
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
