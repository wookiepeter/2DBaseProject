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
        public CircleShape sprite;
        Vector2f position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2f movement { get; set; }
        //Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }

        Vector2f gravity = new Vector2f(0F, 0.8F);
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
            //Console.WriteLine(position.Y);
            float speed = 500F;
            
            Vector2f inputMovement = new Vector2f(0F, 0F);

            bool startJumping = false;
             
            //inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Down) ? 10 : 0F;
            //inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Up) ? -speed : 0F;
            if (index == 2)
            {
                if (GamePadInputManager.IsConnected(1))
                {
                    inputMovement.X = GamePadInputManager.GetLeftStick(1).X;
                    if ((sprite.Position.Y + sprite.Radius * 2) > Program.win.Size.Y * 0.7F - 1)
                    {
                        isJumping = false;
                    }
                    startJumping = !isJumping && GamePadInputManager.IsPressed(GamePadButton.A, 0);
                }

                else
                {
                    inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Left) ? -1 : 0F;
                    inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Right) ? 1 : 0F;

                    if ((sprite.Position.Y + sprite.Radius * 2) > Program.win.Size.Y * 0.7F - 1)
                    {
                        isJumping = false;
                    }
                    startJumping = !isJumping && KeyboardInputManager.IsPressed(Keyboard.Key.Up);
                }
            }
            else
            {

                if (GamePadInputManager.IsConnected(0))
                {
                    inputMovement.X = GamePadInputManager.GetLeftStick(0).X;
                    if ((sprite.Position.Y + sprite.Radius * 2) > Program.win.Size.Y * 0.7F - 1)
                    {
                        isJumping = false;
                    }
                    startJumping = !isJumping && GamePadInputManager.IsPressed(GamePadButton.A, 0);
                }

                else
                {
                    inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.A) ? -1 : 0F;
                    inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.D) ? 1 : 0F;

                    if ((sprite.Position.Y + sprite.Radius * 2) > Program.win.Size.Y * 0.7F - 1)
                    {
                        isJumping = false;
                    }
                    startJumping = !isJumping && KeyboardInputManager.IsPressed(Keyboard.Key.W);
                }
            }

            if (startJumping)
            {
                movement = new Vector2(inputMovement.X * speed, -400F);
                isJumping = true;
            }
            else
                movement = new Vector2(inputMovement.X * speed, movement.Y + gravity.Y * speed * deltaTime);
            
            position += movement * deltaTime;



            

           if (position.Y < 0)
            {
                position = new Vector2f(position.X,0);
               // movement *= Vector2.Up;
            }            

             if (position.Y > Program.win.Size.Y*0.7F - sprite.Radius*2)
            {
                position = new Vector2f (position.X, Program.win.Size.Y*0.7F - sprite.Radius*2);
                //movement *= Vector2.Up;
            }

            if (index == 1)
            {
                if (position.X > (Program.win.Size.X *0.43F) - sprite.Radius*1.7F)
                {
                    position = new Vector2f((Program.win.Size.X *0.43F) - sprite.Radius*1.7F, position.Y);
                    //movement *= Vector2.Left;
                }
                if (position.X < Program.win.Size.X * 0.03F - sprite.Radius/3)
                {
                    position = new Vector2f(Program.win.Size.X * 0.03F - sprite.Radius/3, position.Y);
                    //movement *= Vector2.Left;
                }
            }
            if (index == 2)
            {
                if (position.X < ((1F - 0.43F) * Program.win.Size.X)  - sprite.Radius/2.5F)
                {
                    position = new Vector2f(((1F - 0.43F) * Program.win.Size.X) - sprite.Radius/2.5F, position.Y);
                    //movement *= Vector2.Left;
                }
                if (position.X > (1F-0.03F)*Program.win.Size.X - sprite.Radius*1.7F)
                {
                    position = new Vector2f((1F - 0.03F)*Program.win.Size.X - sprite.Radius*1.7F, position.Y);
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
