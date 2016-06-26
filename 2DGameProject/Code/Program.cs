using SFML.Graphics;
using SFML.Window;
using System;
using SFML.Audio;

namespace GameProject2D
{
    class Program
    {
        public static GameTime GameTime;

        static bool running = true;

        static GameState currentGameState = GameState.MainMenu;
        static GameState prevGameState = GameState.MainMenu;
        static IGameState state;

        public static RenderWindow win;
        static readonly Vector2 windowSize = new Vector2(1024, 768);
        static View view;
        static GUI gui;

        static Music menuMusic = new Music("Sounds/Music/MainMenu.wav");
        static Music inGameMusic = new Music("Sounds/Music/InGame.wav");
        public static Sound splashSound = new Sound(new SoundBuffer("Sounds/splash.wav"));
        public static Sound jumpsSound = new Sound(new SoundBuffer("Sounds/jump.wav"));

        static void Main(string[] args)
        {
            // initialize window and view
            win = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y), "Schweißrim");
            view = new View();
            ResetView();
            gui = new GUI(win, view);
            
            // prevent window resizing
            win.Resized += (sender, e) => { (sender as Window).Size = windowSize; };

            // exit Program, when window is being closed
            //win.Closed += new EventHandler(closeWindow);
            win.Closed += (sender, e) => { (sender as Window).Close(); };

            // initialize GameState
            HandleNewGameState();

            // initialize GameTime
            GameTime = new GameTime();
            GameTime.Start();
         
            // debug Text
            Text debugText = new Text("debug Text", new Font("Fonts/calibri.ttf"));
            Text winnerOneText = new Text("winner One Text", new Font("Fonts/MagicCardsNormal.ttf"));
            Text winnerTwoText = new Text("winner Two Text", new Font("Fonts/MagicCardsNormal.ttf"));

            while (running && win.IsOpen())
            {
                KeyboardInputManager.Update();
                GamePadInputManager.Update();

                // update GameTime
                GameTime.Update();
                float deltaTime = (float)GameTime.EllapsedTime.TotalSeconds;

                currentGameState = state.Update(deltaTime);

                if (currentGameState != prevGameState)
                {
                    HandleNewGameState();
                }

                // gather drawStuff from State
                win.Clear(new Color(155, 124, 247));    //cornflowerblue ftw!!! 1337
                state.Draw(win, view, deltaTime);
                state.DrawGUI(gui, deltaTime);

                // some DebugText
              // debugText.DisplayedString = "fps: " + (1.0F / deltaTime);
                //win.Draw(debugText);

                if (InGameState.winnerOne && currentGameState == GameState.EndScreen)
                {
                    winnerOneText.DisplayedString = "The Winner is Player One!";
                    winnerOneText.Position = new Vector2(320.0f, 320.0f);
                    winnerOneText.CharacterSize = 20;
                    winnerOneText.Color = Color.Black;
                    Program.win.Draw(winnerOneText);
                }
                else if (!InGameState.winnerOne && currentGameState == GameState.EndScreen)
                {
                    winnerTwoText.DisplayedString = "The Winner is Player Two!";
                    winnerTwoText.Position = new Vector2(320.0f, 320.0f);
                    winnerTwoText.CharacterSize = 20;
                    winnerTwoText.Color = Color.Black;
                    Program.win.Draw(winnerTwoText);
                }

                //System.Threading.Thread.Sleep((1000 / 60) - (int)(deltaTime / 1000F));

                // do the actual drawing
                win.SetView(view);
                win.Display();

                // check for window-events. e.g. window closed        
                win.DispatchEvents();
            }
        }

        static void HandleNewGameState()
        {
            switch (currentGameState)
            {
                case GameState.None:
                    running = false;
                    break;

                case GameState.MainMenu:
                    state = new MainMenuState();
                    inGameMusic.Stop();
                    menuMusic.Loop = true;
                    menuMusic.Play();
                    break;

                case GameState.InGame:
                    state = new InGameState();
                    menuMusic.Stop();
                    inGameMusic.Loop = true;
                    inGameMusic.Play();
                    break;

                case GameState.Reset:
                    currentGameState = prevGameState;
                    prevGameState = GameState.Reset;
                    HandleNewGameState();
                    break;

                case GameState.EndScreen:
                    state = new EndScreenState();
                    inGameMusic.Stop();
                    menuMusic.Loop = true;
                    menuMusic.Play();
                    break;
            }

            prevGameState = currentGameState;

            ResetView();
        }

        static void ResetView()
        {
            view.Center = new Vector2(win.Size.X / 2F, win.Size.Y / 2F);
            view.Size = new Vector2(win.Size.X, win.Size.Y);
        }
    }
}