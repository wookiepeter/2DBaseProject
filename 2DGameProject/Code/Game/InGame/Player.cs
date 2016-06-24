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
        public bool isJumping;

        RectangleShape sprite;
        Vector2f position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2f movement { get; set; }
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }
        Vector2f gravity = new Vector2f(0F, 2F);


        public Player(Vector2f position)
        {
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.FillColor = Color.Red;

            this.position = position;
            this.movement = new Vector2f(0F, 0F);
            
            this.size = new Vector2f(45F, 45F);
        }

        public void update(float deltaTime)
        {
            Console.WriteLine(deltaTime);
            float speed = 0.005F;
            
            Vector2f inputMovement = new Vector2f(0F, 0F);

            inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Down) ? speed : 0F;
           // inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Up) ? -speed : 0F;

            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Left) ? -speed : 0F;
            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Right) ? speed : 0F;

            if (KeyboardInputManager.IsPressed(Keyboard.Key.Up))
            {
                inputMovement.Y -= 100F; //wie hoh der fliegt
                isJumping = true;
            }
            if(inputMovement.Y != 0F || inputMovement.X != 0F)
            {
                movement += inputMovement * speed / (float)Math.Sqrt(inputMovement.X * inputMovement.X + inputMovement.Y * inputMovement.Y);
            }

            movement *= (1F - deltaTime * 4F);    // friction

            position += movement;

           if(position.X < 0)
            {
                position -= movement;
                movement *= Vector2.Left;
            }

            if (position.Y < 0)
            {
                position -= movement;
                movement *= Vector2.Up;
            }

            if (position.Y > Program.win.Size.Y - sprite.Size.Y) // Windowsize - Playersize
            {
                position -= movement;
                movement *= Vector2.Right;
            }

            if (position.X > Program.win.Size.X - sprite.Size.X)
            {
                position -= movement;
                movement *= Vector2.Left;
            }
        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}
