using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class InGameState : IGameState
    {
        Player player;
        Player player2;
        Background background;
        
        public InGameState()
        {
            player = new Player(new Vector2f(50F, 10F),1);
            player2 = new Player(new Vector2f(680F, 10F),2); //neuer Spieler erstellt
            background = new Background();
        }

        public GameState Update(float deltaTime)
        {
            player.update(deltaTime);
            player2.update(deltaTime);
            return GameState.InGame;
            
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            player.draw(win, view);
            player2.draw(win, view);
            background.Draw(win, view);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }
    }
}
