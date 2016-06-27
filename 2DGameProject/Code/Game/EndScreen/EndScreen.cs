using SFML.Graphics;
using SFML.Window;
using System;

namespace GameProject2D
{
    class EndScreenState : IGameState
    {
        Sprite background;
        Sprite WinnerFlowerSprite;

        Text winnerOneText = new Text("winner One Text", new Font("Fonts/ARJULIAN.ttf"));
        Text winnerTwoText = new Text("winner Two Text", new Font("Fonts/ARJULIAN.ttf"));

        float timeInEndScreen;

        public EndScreenState()
        {
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.EndScreen));
            WinnerFlowerSprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Blossom));
            WinnerFlowerSprite.Origin = 0.5F * (Vector2)WinnerFlowerSprite.Texture.Size;
            WinnerFlowerSprite.Scale = Vector2.One * 2F;

            WinnerFlowerSprite.Position = 0.3F * (Vector2)Program.win.Size;
            if (!InGameState.winnerOne)
                WinnerFlowerSprite.Position = (Vector2)WinnerFlowerSprite.Position + 0.4F * (Vector2)Program.win.Size * Vector2.Right;

            timeInEndScreen = 0;
        }

        public GameState Update(float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return GameState.InGame;
            }

            return GameState.EndScreen;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            //win.Draw(background);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            gui.Draw(background);

            timeInEndScreen += deltaTime;
            WinnerFlowerSprite.Rotation += (float)Math.Cos(timeInEndScreen * 4);
            if (InGameState.winnerOne)
            {
                winnerOneText.DisplayedString = "The Winner is Player One!";
                winnerOneText.Position = new Vector2(270.0f, 320.0f);
                winnerOneText.CharacterSize = 40;
                winnerOneText.Color = Color.Black;
                gui.Draw(winnerOneText);

                gui.Draw(WinnerFlowerSprite);
            }
            else if (!InGameState.winnerOne)
            {
                winnerTwoText.DisplayedString = "The Winner is Player Two!";
                winnerTwoText.Position = new Vector2(270.0f, 320.0f);
                winnerTwoText.CharacterSize = 40;
                winnerTwoText.Color = Color.Black;
                gui.Draw(winnerTwoText);

                gui.Draw(WinnerFlowerSprite);
            }

        }
    }
}
