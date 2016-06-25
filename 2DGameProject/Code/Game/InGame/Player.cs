using System;
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
        CircleShape sprite;
        Vector2f position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2f movement { get; set; }
        //Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }

        Vector2f gravity = new Vector2f(0F, 2F);
        bool isJumping = true;
        int index; //Nummerirung der Spieler

        

        public Player(Vector2f position, int index) //Konstruktor
        {
            this.sprite = new CircleShape(30.0F);
            if (index == 2) { 
            this.sprite.FillColor = new Color(150, 0, 2);
            }
            else
            {
                this.sprite.FillColor = new Color(0, 50, 255);
            }

            this.position = position;
            this.movement = new Vector2f(0F, 0F);
            this.index = index; 
            
            //this.size = new Vector2f(50F, 50F);
        }

        public void update(float deltaTime)
        {
            Console.WriteLine(position.Y);
            float speed = 0.005F;
            
            Vector2f inputMovement = new Vector2f(0F, 0F);

            //inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Down) ? 10 : 0F;
            //inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Up) ? -speed : 0F;
            if (index == 2)
            {
            
                inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Left) ? -1 : 0F;
                inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Right) ? 1 : 0F;

                if ((sprite.Position.Y + sprite.Radius * 2) > Program.win.Size.Y*0.8F - 1)
                {
                    isJumping = false;
                }

                if (!isJumping && KeyboardInputManager.IsPressed(Keyboard.Key.Up))
                {
                    inputMovement.Y -= 200F;
                    isJumping = true;
                }
            }
            else
            {
                inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.A) ? -1 : 0F;
                inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.D) ? 1 : 0F;

                if ((sprite.Position.Y + sprite.Radius * 2) > Program.win.Size.Y*0.8F - 1)
                {
                    isJumping = false;
                }

                if (!isJumping && KeyboardInputManager.IsPressed(Keyboard.Key.W))
                {
                    inputMovement.Y -= 200F;
                    isJumping = true;
                }
            }
            if (inputMovement.Y != 0F || inputMovement.X != 0F)
            {
                movement += inputMovement * speed; 
                    // (float)Math.Sqrt(inputMovement.X * inputMovement.X + inputMovement.Y * inputMovement.Y);
            }

            movement *= (1F - deltaTime * 4F);    // friction
   
            movement += gravity * deltaTime;

            position += movement;



            

           if (position.Y < 0)
            {
                position = new Vector2f(position.X,0);
               // movement *= Vector2.Up;
            }            

             if (position.Y > Program.win.Size.Y*0.8F - sprite.Radius*2)
            {
                position = new Vector2f (position.X, Program.win.Size.Y*0.8F - sprite.Radius*2);
                //movement *= Vector2.Up;
            }

            if (index == 1)
            {
                if (position.X > (Program.win.Size.X -500F) - sprite.Radius * 2)
                {
                    position = new Vector2f((Program.win.Size.X-500F) - sprite.Radius *2, position.Y);
                    //movement *= Vector2.Left;
                }
                if (position.X < 0)
                {
                    position = new Vector2f(0, position.Y);
                    //movement *= Vector2.Left;
                }
            }
            if (index == 2)
            {
                if (position.X < (Program.win.Size.X -270F)  - sprite.Radius)
                {
                    position = new Vector2f((Program.win.Size.X -270F) - sprite.Radius, position.Y);
                    //movement *= Vector2.Left;
                }
                if (position.X > Program.win.Size.X - sprite.Radius*2)
                {
                    position = new Vector2f(Program.win.Size.X - sprite.Radius*2, position.Y);
                    //movement *= Vector2.Left;
                }
            }


        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}
