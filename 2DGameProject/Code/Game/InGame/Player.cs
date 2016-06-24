﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    public class Player
    {
        RectangleShape sprite;
        Vector2f position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2f movement { get; set; }
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }

        Vector2f gravity = new Vector2f(0F, 2F);
        bool isJumping = true;


        public Player(Vector2f position)
        {
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.FillColor = Color.Black;

            this.position = position;
            this.movement = new Vector2f(0F, 0F);
            
            this.size = new Vector2f(100F, 100F);
        }

        public void update(float deltaTime)
        {
            Console.WriteLine(position.Y);
            float speed = 0.005F;
            
            Vector2f inputMovement = new Vector2f(0F, 0F);

           //inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Down) ? 10 : 0F;
            //inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Up) ? -speed : 0F;
          
            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Left) ? -1 : 0F;
            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Right) ? 1 : 0F;

            if ((sprite.Position.Y + sprite.Size.Y)>Program.win.Size.Y-1)
            {
                isJumping = false;
            }

            if (!isJumping && KeyboardInputManager.IsPressed(Keyboard.Key.Up))
            {
                inputMovement.Y -= 200F;
                isJumping = true;
            }

            if(inputMovement.Y != 0F || inputMovement.X != 0F)
            {
                movement += inputMovement * speed; 
                    // (float)Math.Sqrt(inputMovement.X * inputMovement.X + inputMovement.Y * inputMovement.Y);
            }

            movement *= (1F - deltaTime * 4F);    // friction
   
            movement += gravity * deltaTime;

            position += movement;



            if(position.X < 0)
            {
                position = new Vector2f(0,position.Y);
                //movement *= Vector2.Left;
            }

           if (position.Y < 0)
            {
                position = new Vector2f(position.X,0);
               // movement *= Vector2.Up;
            }            

             if (position.Y > Program.win.Size.Y - sprite.Size.Y)
            {
                position = new Vector2f (position.X, Program.win.Size.Y - sprite.Size.Y);
                //movement *= Vector2.Up;
            }


             if (position.X > Program.win.Size.X - sprite.Size.X)
            {
                position = new Vector2f (Program.win.Size.X - sprite.Size.X, position.Y);
                //movement *= Vector2.Left;
            }


        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}
