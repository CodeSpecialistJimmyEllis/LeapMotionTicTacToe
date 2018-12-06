


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    class ScreenManager
    {

        #region Variables
       
        ContentManager content;

        private static ScreenManager instance = new ScreenManager();
        GameScreen currentScreen;
        GameScreen newScreen;

        Stack<GameScreen> screenStack = new Stack<GameScreen>();

        // Screen width and height

        Vector2 dimensions = Vector2.Zero;
       
        //  screen states
        TicTacToeTitleScreen tictactoetitlescreen;
        TicTacToeGameScreen tictactoegamescreen;
        TicTacToeAI tictactoeAI;

        #endregion


        #region Properties
      
    
        public static ScreenManager Instance
        {
            get
            {
              //  if (instance == null)
              //      instance = new ScreenManager();
                return instance;
            }
        }
        // JESUS IS LORD <<< PHEONOMINA >>> SETS THE SIZE OF THE SCREEN AND GETS IT JESUS IS LORD! 
        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public float DimensionsX
        {
            get { return dimensions.X; }
            set { dimensions.X = value; }
        }
        public float DimensionsY
        {
            get { return dimensions.Y; }
            set { dimensions.Y = value; }
        }


        

        #endregion

        #region Main Methods

        // JESUS IS LORD INITIALIZES ARE GOOD BECAUSE THEY CAN BE RECALLED TO OVER AND OVER WHILE CONSTRUCTORS ONLY CALLED ONCE! THAT'S WHY CONSTRUCTORS AND INITAILZIE METHODS OARE DIFFERNET JES US IS LORD!

        // JESUS IS LORD LOAD OR UNLOAD SCREEN <<< PHEONOMINA >>>
        public void AddScreen(GameScreen screen)
        {
            //JESUS IS LORD CHANGE SCREENS IN HERE JESUS IS LORD!
            newScreen = screen;
            screenStack.Push(screen);

            currentScreen.UnloadContent();
         
            
            
            currentScreen = newScreen;
            currentScreen.LoadContent(content);
        }



        public void Initialize()
        {
            tictactoetitlescreen = new TicTacToeTitleScreen();
          //  instance = new ScreenManager();
            currentScreen = tictactoetitlescreen;
        }
        public void LoadContent(ContentManager Content)
        {

            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content);
        }
        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        #endregion
    }
}


