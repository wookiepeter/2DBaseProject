using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class EndScreenState : IGameState
    {
        Sprite background;

        public EndScreenState()
        {
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.EndScreen));
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
        }
    }
}
