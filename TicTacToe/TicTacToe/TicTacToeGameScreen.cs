// All Code, Art, Music by Jimmy Ellis
// As a candidate for 8th Light Computer Science C# Division 
// this program has been written with deep care as proof
// and validation of apprentice/craftsman/resident level skill
// in the final science of life: code.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using LeapLibrary; 

// Notes by Jimmy Ellis
// What is called artificial intelligence is a rubix cube. That makes no sense, that is why artifical intelligence is not real
// What is real is the deep TRUTH behind PROBABILITY MATHEMATICS. Different probabilities ruled by different if statements 
// in equation to come to the final conclusion. 

// The pheonomina of the O (computer winning) all the time is achieved then by 
// 1. having the O go first and place in the middle each time.
// 2.using a collection probability equations to create possible ends as a reply to the X choice.
// This equation is the reason why A.I. in the end is a flawed concept. 
// As A.I. was achieved long ago. . . it is called code. 
// Notes by Jimmy Ellis
namespace TicTacToe
{

    public class TicTacToeGameScreen : GameScreen
    {
        #region Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //leap
        LeapTimeButton leapTimeButton;
        //artificial intelligence seperate class 
        TicTacToeAI tictactoeAI;


        Texture2D blankBackground;
        Rectangle blankBackgroundRect;

        Texture2D gameBoard;
        Rectangle gameBoardRect;

        Texture2D[] o;
        Rectangle[] oRect;
        bool[] isTimeSet = new bool[9];
        Texture2D[] x;
        Rectangle[] xRect;
        int width = 1024;
        int height = 720;
        int[] baseSeconds = {0,0,0 ,0,0,0 ,0,0,0};
        // the mouse 
        MouseState gameMouse;
          bool[] timedHit;
        //the mouse collision
        Rectangle mouseCollision;
        Rectangle[] boxCollisionRect;
        Texture2D[] boxCollision;

        // spritefont


        int mouseCount;
        // double click addition
        MouseState previousState;
        MouseState middleState;

        SpriteFont spriteFont;
        bool isFirstMove = false; 
      public  bool[,] isLocationUsed;
      public  bool[,] locationUsedByX;
       public bool[,] locationUsedByO;
        bool terminateLoop;
        // gameboard location enumeration for better huamn understanding
        enum GameBoardLocation
        {
            empty, row1x1, row1x2, row1x3, row2x1, row2x2, row2x3, row3x1, row3x2, row3x3
        }
        GameBoardLocation gameboardlocation;

        // enumeration for victory
        public enum WinningFormation
        {
            nowin, row1win, row2win, row3win, column1win, column2win, column3win, topleftdiagnolwin, toprightdiagnolwin, catgame
        }
        public static WinningFormation winningformation;

        // activate to activate winning condition
        // who wins

        bool hitEnterToReplay;
        public enum Winner
        {
            nowins, xwins, owins, catgame
        }
        public static Winner winner;

        // enumeration to see who's turn it is
       public enum TurnPiece
        {
            xturn, oturn
        }
        public TurnPiece turnpiece;

        Vector2 pieceSize;
        // for the algorithim that correctly checks which tic tac toe piee must be used
        int pieceNumberUsed = 0;

        int numberOfSpacesUsed = 0;
        /* comment only
Locations for each field in debug mode
textfile that maps locations 
 1 2 3 
 4 5 6 
 7 8 9 
1.) x: 150 y: 100
2.) x: 370 y: 90
3.) x: 570 y: 95
4.) x: 140 y: 290
5.) x: 365 y: 300
6.) x: 575 y: 290
7.) x: 155 y: 520
8.) x: 365 y: 510
9.) x: 570 y: 520
*/
        Vector2[,] boxLocations;

        #endregion

        #region Constructors


        public TicTacToeGameScreen()
        {
            tictactoeAI = new TicTacToeAI();
            
            // leap time button
            leapTimeButton = new LeapTimeButton();
            //
            turnpiece = TurnPiece.oturn;
            winningformation = WinningFormation.nowin;
            gameMouse = new MouseState();
            blankBackgroundRect = new Rectangle(0, 0, 1024, 720);
            mouseCount = 0;
            previousState = new MouseState();
            middleState = new MouseState();
            hitEnterToReplay = false;
            pieceSize.X = 128;
            pieceSize.Y = 128;
            // the peices rectangles set
            #region Pieces
            xRect = new Rectangle[9];
            oRect = new Rectangle[100];
            xRect[0] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[1] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[2] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[3] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[4] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[5] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[6] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[7] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[8] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);

            terminateLoop = false;
            // rectangle pieces 
            oRect[0] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[1] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[2] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[3] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[4] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[5] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[6] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[7] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[8] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[9] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[10] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[11] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[12] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[13] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[14] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[15] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[16] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[17] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[18] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[19] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[20] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[21] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[22] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[23] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[24] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[25] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[26] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[27] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[28] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[29] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[30] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[31] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[32] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[33] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[34] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[35] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[36] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[37] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[38] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[39] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[40] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[41] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);


            oRect[42] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[43] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[44] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[45] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[46] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[47] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[48] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[49] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[50] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[51] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[52] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[53] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[54] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[55] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[56] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[57] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[58] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[59] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[60] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[61] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[62] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[63] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[64] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[65] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[66] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);


            oRect[60] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[61] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[62] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[63] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[64] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[65] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[66] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[67] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[68] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[69] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[70] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[71] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[72] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[73] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[74] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[75] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[76] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[77] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[78] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[79] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[80] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[81] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[82] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[83] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[84] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[85] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[86] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[87] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[88] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[89] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[90] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[91] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[92] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[93] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[94] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);

            oRect[95] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[96] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[97] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[98] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            oRect[99] = new Rectangle(130, 0, (int)pieceSize.X, (int)pieceSize.Y);
            #endregion

            gameBoardRect = new Rectangle(100, 50, 640, 640);
            mouseCollision = new Rectangle(0, 0, 24, 24);
            // intitalizing the texture array

            // gameboard location is set to empty
            gameboardlocation = GameBoardLocation.empty;

            // locations of each and every piece put into a vector2 array to explicit or implicit conversion to int for rectangular collision
            boxLocations = new Vector2[3, 3];

            boxLocations[0, 0] = new Vector2(150, 100);
            boxLocations[0, 1] = new Vector2(370, 90);
            boxLocations[0, 2] = new Vector2(570, 95);

            boxLocations[1, 0] = new Vector2(140, 290);
            boxLocations[1, 1] = new Vector2(365, 300);
            boxLocations[1, 2] = new Vector2(575, 290);

            boxLocations[2, 0] = new Vector2(155, 520);
            boxLocations[2, 1] = new Vector2(365, 510);
            boxLocations[2, 2] = new Vector2(570, 520);

            // confirm or deny if the location is used or not based on boolean logic algorithim
            tictactoeAI.IsLocationUsed = new bool[3, 3];

            for (int i = 0; i < 9; i++)
            {
                isTimeSet[i] = false;
            }
            // nested double for loop that initializes all in the multi dimensional array as false for update method
            // deep logical algorithims
            for (int a = 0; a < 3; a++)
            {

                for (int b = 0; b < 3; b++)
                {
                    tictactoeAI.IsLocationUsed[a, b] = false;

                }


            }
            // boolean multi dimensional array to see if x is using the spot 
            tictactoeAI.LocationUsedByX = new bool[3, 3];
            for (int a = 0; a < 3; a++)
            {

                for (int b = 0; b < 3; b++)
                {
                    tictactoeAI.LocationUsedByX[a, b] = false;

                }


            }
            // double nested for loop to set all values to false
            timedHit = new bool[9];
            for (int s = 0; s < 9; s++)
            {
                  timedHit[s] = false;
            }
            // boolean multi dimensional array to see if o is using the spot 
            tictactoeAI.LocationUsedByO = new bool[3, 3];
            for (int a = 0; a < 3; a++)
            {

                for (int b = 0; b < 3; b++)
                {
                    tictactoeAI.LocationUsedByO[a, b] = false;

                }


            }
            /* comment only
Locations for each field in debug mode
textfile that maps locations 
 1 2 3 
 4 5 6 
 7 8 9 
1.) x: 150 y: 100
2.) x: 370 y: 90
3.) x: 570 y: 95
4.) x: 140 y: 290
5.) x: 365 y: 300
6.) x: 575 y: 290
7.) x: 155 y: 520
8.) x: 365 y: 510
9.) x: 570 y: 520
*/



            boxCollisionRect = new Rectangle[9];

            boxCollisionRect[0] = new Rectangle((int)boxLocations[0, 0].X, (int)boxLocations[0, 0].Y, 130, 130);
            boxCollisionRect[1] = new Rectangle((int)boxLocations[0, 1].X, (int)boxLocations[0, 1].Y, 130, 130);
            boxCollisionRect[2] = new Rectangle((int)boxLocations[0, 2].X, (int)boxLocations[0, 2].Y, 130, 130);

            boxCollisionRect[3] = new Rectangle((int)boxLocations[1, 0].X, (int)boxLocations[1, 0].Y, 130, 130);
            boxCollisionRect[4] = new Rectangle((int)boxLocations[1, 1].X, (int)boxLocations[1, 1].Y, 130, 130);
            boxCollisionRect[5] = new Rectangle((int)boxLocations[1, 2].X, (int)boxLocations[1, 2].Y, 130, 130);

            boxCollisionRect[6] = new Rectangle((int)boxLocations[2, 0].X, (int)boxLocations[2, 0].Y, 130, 130);
            boxCollisionRect[7] = new Rectangle((int)boxLocations[2, 1].X, (int)boxLocations[2, 1].Y, 130, 130);
            boxCollisionRect[8] = new Rectangle((int)boxLocations[2, 2].X, (int)boxLocations[2, 2].Y, 130, 130);

            /* comment only
            Locations for each field in debug mode
            textfile that maps locations 
             1 2 3 
             4 5 6 
             7 8 9 
            1.) x: 150 y: 100
            2.) x: 370 y: 90
            3.) x: 570 y: 95
            4.) x: 140 y: 290
            5.) x: 365 y: 300
            6.) x: 575 y: 290
            7.) x: 155 y: 520
            8.) x: 365 y: 510
            9.) x: 570 y: 520
            */


           
        }



        #endregion
      
        #region LoadContent
        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            leapTimeButton.LoadContent(Content);
            tictactoeAI.LoadContent(Content);
            // background picture
            blankBackground = Content.Load<Texture2D>("Backgrounds/blankbackground");

            // x on the screen
            x = new Texture2D[9];
            for (int i = 0; i < 9; i++)
            {
                x[i] = Content.Load<Texture2D>("Pieces/x");
            }
            // o on the screen
            o = new Texture2D[100];
            for (int z = 0; z < 100; z++)
            {
                o[z] = Content.Load<Texture2D>("Pieces/o");
            }
            // tic tac toe game board 
            gameBoard = Content.Load<Texture2D>("Pieces/board");

            // items on the screen
            spriteFont = Content.Load<SpriteFont>("Content/SpriteFont1");


            boxCollision = new Texture2D[9];
            /*
            for (int i = 0; i < boxCollision.Length; i++)
            {
                boxCollision = new Texture2D[i];
            }*/
            // box collision empty boxes to decide collision
            boxCollision[0] = Content.Load<Texture2D>("Collisions/boxcollision01");
            boxCollision[1] = Content.Load<Texture2D>("Collisions/boxcollision02");
            boxCollision[2] = Content.Load<Texture2D>("Collisions/boxcollision03");

            boxCollision[3] = Content.Load<Texture2D>("Collisions/boxcollision04");
            boxCollision[4] = Content.Load<Texture2D>("Collisions/boxcollision05");
            boxCollision[5] = Content.Load<Texture2D>("Collisions/boxcollision06");

            boxCollision[6] = Content.Load<Texture2D>("Collisions/boxcollision07");
            boxCollision[7] = Content.Load<Texture2D>("Collisions/boxcollision08");
            boxCollision[8] = Content.Load<Texture2D>("Collisions/boxcollision09");


        }
        #endregion
        #region UnloadContent
        public override void UnloadContent()
        {
            

        }
        #endregion
        #region Update
      
        public override void Update(GameTime gameTime)
        {

            // HIERACHY 6 CHECK TO SEE WHO WINS 
            leapTimeButton.Update(gameTime);
            if (TicTacToeAI.Winner.nowins != tictactoeAI.P_Winner)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.R))
                {
                    tictactoeAI.P_Winner = TicTacToeAI.Winner.nowins;
                    TicTacToeGameScreen gameScreen = new TicTacToeGameScreen();
                    ScreenManager.Instance.AddScreen(gameScreen);
                }
            }
          
            // double click addition
           
            middleState = previousState;
            
            mouseCollision.X = gameMouse.X;
            mouseCollision.Y = gameMouse.Y;
            // calling algorithim that calculates the computers tic tac toe moves based on probability mathematics


            pieceNumberUsed = numberOfSpacesUsed;
            
            
            #region x turn

            // proper testing then permenant location change  // TERMINATE COMMENTS
            /*
            tictactoeAI.IsLocationUsed = tictactoeAI.IsLocationUsed;
            tictactoeAI.tictactoeAI.LocationUsedByO = tictactoeAI.LocationUsedByO;
            tictactoeAI.tictactoeAI.LocationUsedByX = tictactoeAI.LocationUsedByX;
             * 
             */
            /*
            
          
            tictactoeAI.P_Turnpiece = turnpiece;
            */

           
                tictactoeAI.Update(gameTime);



                if ((TicTacToeAI.TurnPiece.xturn == tictactoeAI.P_Turnpiece) && (TicTacToeAI.Winner.nowins == tictactoeAI.P_Winner))
                {


                    #region leap collisions and timed hit boxes
                    if (tictactoeAI.BoxCollisionRect[0].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[0] == false)
                        {
                            isTimeSet[0] = true;
                            baseSeconds[0] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[0] + 2))
                        {
                            timedHit[0] = true;
                        }

                    }

                    else
                    {
                        isTimeSet[0] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[1].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[1] == false)
                        {
                            isTimeSet[1] = true;
                            baseSeconds[1] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[1] + 2))
                        {
                            timedHit[1] = true;
                        }

                    }

                    else
                    {
                        isTimeSet[1] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[2].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[2] == false)
                        {
                            isTimeSet[2] = true;
                            baseSeconds[2] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[2] + 2))
                        {
                            timedHit[2] = true;
                        }

                    }

                    else
                    {
                        isTimeSet[2] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[3].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[3] == false)
                        {
                            isTimeSet[3] = true;
                            baseSeconds[3] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[3] + 2))
                        {
                            timedHit[3] = true;
                        }

                    }

                    else
                    {
                        isTimeSet[3] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[4].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[4] == false)
                        {
                            isTimeSet[4] = true;
                            baseSeconds[4] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[4] + 2))
                        {
                            timedHit[4] = true;
                        }

                    }

                    else
                    {
                        isTimeSet[4] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[5].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[5] == false)
                        {
                            isTimeSet[5] = true;
                            baseSeconds[5] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[5] + 2))
                        {
                            timedHit[5] = true;
                        }

                    }

                    else
                    {
                        isTimeSet[5] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[6].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[6] == false)
                        {
                            isTimeSet[6] = true;
                            baseSeconds[6] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[6] + 2))
                        {
                            timedHit[6] = true;
                        }


                    }

                    else
                    {
                        isTimeSet[6] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[7].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[7] == false)
                        {
                            isTimeSet[7] = true;
                            baseSeconds[7] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[7] + 2))
                        {
                            timedHit[7] = true;
                        }

                    }

                    else
                    {
                        isTimeSet[7] = false;
                    }
                    if (tictactoeAI.BoxCollisionRect[8].Intersects(Game1.leapcollision))
                    {
                        if (isTimeSet[8] == false)
                        {
                            isTimeSet[8] = true;
                            baseSeconds[8] = gameTime.TotalGameTime.Seconds;
                        }
                        if (gameTime.TotalGameTime.Seconds >= (baseSeconds[8] + 3))
                        {
                            timedHit[8] = true;
                        }
                    }

                    else
                    {
                        isTimeSet[8] = false;
                    }
                    #endregion 

                    #region MasterPlacementAlgorithim For X
                    // position 1x1

                    // double click :)

                    // boxCollisionRect = tictactoeAI.BoxCollisionRect;

                    // if (Mouse.GetState().LeftButton == ButtonState.Presed && gameMouse.

                    // if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    // {
                    //  gameMouse = Mouse.GetState();

                    //   the first row 
                    // if statement algorithim activates to set x piece as long as the location is not used by o as such collision
                    // detection complete for x pieces. now to finish o placement algorithims. - Jimmy Ellis
                    if (timedHit[0] == true && (tictactoeAI.LocationUsedByO[0, 0] == false) && (tictactoeAI.LocationUsedByX[0, 0] == false) && (tictactoeAI.IsLocationUsed[0, 0] == false))
                    {

                        

                        
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row1x1;
                        xRect[0].X = Convert.ToInt32(boxLocations[0, 0].X);
                        xRect[0].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                        tictactoeAI.IsLocationUsed[0, 0] = true;
                        tictactoeAI.LocationUsedByX[0, 0] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[0, 0] == true) && (tictactoeAI.LocationUsedByX[0, 0] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim

                            tictactoeAI.SetToOturn();
                        }
                    }


                    if (timedHit[1] && (tictactoeAI.LocationUsedByO[0, 1] == false) && (tictactoeAI.LocationUsedByX[0, 1] == false) && (tictactoeAI.IsLocationUsed[0, 1] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row1x2;
                        xRect[1].X = Convert.ToInt32(boxLocations[0, 1].X);
                        xRect[1].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        tictactoeAI.IsLocationUsed[0, 1] = true;
                        tictactoeAI.LocationUsedByX[0, 1] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[0, 1] == true) && (tictactoeAI.LocationUsedByX[0, 1] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();
                        }
                    }

                   /* if (tictactoeAI.BoxCollisionRect[2].Intersects(Game1.leapcollision) */ if( timedHit[2] == true && (tictactoeAI.LocationUsedByO[0, 2] == false) && (tictactoeAI.LocationUsedByX[0, 2] == false) && (tictactoeAI.IsLocationUsed[0, 2] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row1x3;
                        xRect[2].X = Convert.ToInt32(boxLocations[0, 2].X);
                        xRect[2].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        tictactoeAI.IsLocationUsed[0, 2] = true;
                        tictactoeAI.LocationUsedByX[0, 2] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[0, 2] == true) && (tictactoeAI.LocationUsedByX[0, 2] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();
                        }
                    }

                    // end first row

                    // the second row

                    if (timedHit[3] && (tictactoeAI.LocationUsedByO[1, 0] == false) && (tictactoeAI.LocationUsedByX[1, 0] == false) && (tictactoeAI.IsLocationUsed[1, 0] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row2x1;
                        xRect[3].X = Convert.ToInt32(boxLocations[1, 0].X);
                        xRect[3].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        tictactoeAI.IsLocationUsed[1, 0] = true;
                        tictactoeAI.LocationUsedByX[1, 0] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[1, 0] == true) && (tictactoeAI.LocationUsedByX[1, 0] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();
                        }
                    }

                    if (timedHit[4] == true && (tictactoeAI.LocationUsedByO[1, 1] == false) && (tictactoeAI.LocationUsedByX[1, 1] == false) && (tictactoeAI.IsLocationUsed[1, 1] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row2x2;
                        xRect[4].X = Convert.ToInt32(boxLocations[1, 1].X);
                        xRect[4].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                        tictactoeAI.IsLocationUsed[1, 1] = true;
                        tictactoeAI.LocationUsedByX[1, 1] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[1, 1] == true) && (tictactoeAI.LocationUsedByX[1, 1] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();

                        }
                    }

                    if (timedHit[5] == true && (tictactoeAI.LocationUsedByO[1, 2] == false) && (tictactoeAI.LocationUsedByX[1, 2] == false) && (tictactoeAI.IsLocationUsed[1, 2] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row2x3;
                        xRect[5].X = Convert.ToInt32(boxLocations[1, 2].X);
                        xRect[5].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        tictactoeAI.IsLocationUsed[1, 2] = true;
                        tictactoeAI.LocationUsedByX[1, 2] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[1, 2] == true) && (tictactoeAI.LocationUsedByX[1, 2] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();
                        }
                    }
                    // end second row

                    // the third row
                    if ( timedHit[6] == true && (tictactoeAI.LocationUsedByO[2, 0] == false) && (tictactoeAI.LocationUsedByX[2, 0] == false) && (tictactoeAI.IsLocationUsed[2, 0] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row3x1;
                        xRect[6].X = Convert.ToInt32(boxLocations[2, 0].X);
                        xRect[6].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                        tictactoeAI.IsLocationUsed[2, 0] = true;
                        tictactoeAI.LocationUsedByX[2, 0] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[2, 0] == true) && (tictactoeAI.LocationUsedByX[2, 0] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();
                        }
                    }
                    if (timedHit[7] == true && (tictactoeAI.LocationUsedByO[2, 1] == false) && (tictactoeAI.LocationUsedByX[2, 1] == false) && (tictactoeAI.IsLocationUsed[2, 1] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row3x2;
                        xRect[7].X = Convert.ToInt32(boxLocations[2, 1].X);
                        xRect[7].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                        tictactoeAI.IsLocationUsed[2, 1] = true;
                        tictactoeAI.LocationUsedByX[2, 1] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[2, 1] == true) && (tictactoeAI.LocationUsedByX[2, 1] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();
                        }
                    }
                    if (timedHit[8] == true && (tictactoeAI.LocationUsedByO[2, 2] == false) && (tictactoeAI.LocationUsedByX[2, 2] == false) && (tictactoeAI.IsLocationUsed[2, 2] == false))
                    {
                        tictactoeAI.Gameboardlocation = TicTacToeAI.GameBoardLocation.row3x3;
                        xRect[8].X = Convert.ToInt32(boxLocations[2, 2].X);
                        xRect[8].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                        tictactoeAI.IsLocationUsed[2, 2] = true;
                        tictactoeAI.LocationUsedByX[2, 2] = true;

                        // validate piece is layed before turn is switched for no error play
                        if ((tictactoeAI.IsLocationUsed[2, 2] == true) && (tictactoeAI.LocationUsedByX[2, 2] == true))
                        {

                            // switch the turn to the oturn computer opponent algorithim
                            tictactoeAI.SetToOturn();
                        }
                    }
                }
                // end third row
                #endregion

            
            #endregion




            base.Update(gameTime);
        }
        #endregion



      
        // final piece of the puzzle. the tic tac toe algorithim solved. The final piece. the unbeatable a.i.
        // artificial intelligence hierachy a combination of 3 methods. 
        // the method that places the O in the middle for winnign
        // the method that responds to O moves
        // the method that checks for O winning conditions
        // middle o method called FIRST, o in relationship to x moves is called LAST, and O in relationship to WINNING CONDITIONS happens SECOND
        // only one of the methods will create a move and no double moves will occur



        //
        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {


            //drawing of peices in game
          
            ////////////////////////////////////
            spriteBatch.Draw(blankBackground, blankBackgroundRect, Color.White);

            spriteBatch.Draw(gameBoard, gameBoardRect, Color.White);
            

            ///////////////////////////////

            // The Deepest Genius Known To Man. The Empty Collision. Mathematics. . . in secret never seen only experience 
            // as the how of how to get the tic tac toe on the screen. 
            /* spriteBatch.Begin();
             spriteBatch.Draw();
             spriteBatch.End(); */



            #region Invisible Box Collision Zone
            // drawing the empty collision boxes for collision reassessment
           ////////////////////////////////////////////////
            spriteBatch.Draw(boxCollision[0], boxCollisionRect[0], Color.Red);
            spriteBatch.Draw(boxCollision[0], boxCollisionRect[1], Color.Red);
            spriteBatch.Draw(boxCollision[0], boxCollisionRect[2], Color.Red);

            spriteBatch.Draw(boxCollision[0], boxCollisionRect[3], Color.Red);
            spriteBatch.Draw(boxCollision[0], boxCollisionRect[4], Color.Red);
            spriteBatch.Draw(boxCollision[0], boxCollisionRect[5], Color.Red);

            spriteBatch.Draw(boxCollision[0], boxCollisionRect[6], Color.Red);
            spriteBatch.Draw(boxCollision[0], boxCollisionRect[7], Color.Red);
            spriteBatch.Draw(boxCollision[0], boxCollisionRect[8], Color.Red);
            //////////////////////////////////////////////
            #endregion

            


            #region Drawing all Xs and Os and placement Algorithims

            /////////////////////////////////////////////////////////////


            // x draw on screen
            spriteBatch.Draw(x[0], xRect[0], Color.White);


            spriteBatch.Draw(x[1], xRect[1], Color.White);
            spriteBatch.Draw(x[2], xRect[2], Color.White);

            spriteBatch.Draw(x[3], xRect[3], Color.White);
            spriteBatch.Draw(x[4], xRect[4], Color.White);
            spriteBatch.Draw(x[5], xRect[5], Color.White);

            spriteBatch.Draw(x[6], xRect[6], Color.White);
            spriteBatch.Draw(x[7], xRect[7], Color.White);
            spriteBatch.Draw(x[8], xRect[8], Color.White);
            // end x 

            spriteBatch.Draw(o[0], oRect[0], Color.White);
            spriteBatch.Draw(o[1], oRect[1], Color.White);
            spriteBatch.Draw(o[2], oRect[2], Color.White);

            spriteBatch.Draw(o[3], oRect[3], Color.White);
            spriteBatch.Draw(o[4], oRect[4], Color.White);
            spriteBatch.Draw(o[5], oRect[5], Color.White);

            spriteBatch.Draw(o[6], oRect[6], Color.White);
            spriteBatch.Draw(o[7], oRect[7], Color.White);
            spriteBatch.Draw(o[8], oRect[8], Color.White);

            // drawing more os for a.i. algorithim

            spriteBatch.Draw(o[9], oRect[9], Color.White);
            spriteBatch.Draw(o[10], oRect[10], Color.White);
            spriteBatch.Draw(o[11], oRect[11], Color.White);

            spriteBatch.Draw(o[12], oRect[12], Color.White);
            spriteBatch.Draw(o[13], oRect[13], Color.White);
            spriteBatch.Draw(o[14], oRect[14], Color.White);

            spriteBatch.Draw(o[15], oRect[15], Color.White);
            spriteBatch.Draw(o[16], oRect[16], Color.White);
            spriteBatch.Draw(o[17], oRect[17], Color.White);

            spriteBatch.Draw(o[18], oRect[18], Color.White);
            spriteBatch.Draw(o[19], oRect[19], Color.White);
            spriteBatch.Draw(o[20], oRect[20], Color.White);

            spriteBatch.Draw(o[21], oRect[21], Color.White);
            spriteBatch.Draw(o[22], oRect[22], Color.White);
            spriteBatch.Draw(o[23], oRect[23], Color.White);

            spriteBatch.Draw(o[24], oRect[24], Color.White);
            spriteBatch.Draw(o[25], oRect[25], Color.White);
            spriteBatch.Draw(o[26], oRect[26], Color.White);

            spriteBatch.Draw(o[27], oRect[27], Color.White);
            spriteBatch.Draw(o[28], oRect[28], Color.White);
            spriteBatch.Draw(o[29], oRect[29], Color.White);

            spriteBatch.Draw(o[30], oRect[30], Color.White);
            spriteBatch.Draw(o[31], oRect[31], Color.White);
            spriteBatch.Draw(o[32], oRect[32], Color.White);

            spriteBatch.Draw(o[33], oRect[33], Color.White);
            spriteBatch.Draw(o[34], oRect[34], Color.White);
            spriteBatch.Draw(o[35], oRect[35], Color.White);

            spriteBatch.Draw(o[36], oRect[36], Color.White);
            spriteBatch.Draw(o[37], oRect[37], Color.White);
            spriteBatch.Draw(o[38], oRect[38], Color.White);

            spriteBatch.Draw(o[39], oRect[39], Color.White);
            spriteBatch.Draw(o[40], oRect[40], Color.White);
            spriteBatch.Draw(o[41], oRect[41], Color.White);

            spriteBatch.Draw(o[42], oRect[42], Color.White);
            spriteBatch.Draw(o[43], oRect[43], Color.White);
            spriteBatch.Draw(o[44], oRect[44], Color.White);

            spriteBatch.Draw(o[45], oRect[45], Color.White);
            spriteBatch.Draw(o[46], oRect[46], Color.White);
            spriteBatch.Draw(o[47], oRect[47], Color.White);

            spriteBatch.Draw(o[48], oRect[48], Color.White);
            spriteBatch.Draw(o[49], oRect[49], Color.White);
            spriteBatch.Draw(o[50], oRect[50], Color.White);

            spriteBatch.Draw(o[51], oRect[51], Color.White);
            spriteBatch.Draw(o[52], oRect[52], Color.White);
            spriteBatch.Draw(o[53], oRect[53], Color.White);

            spriteBatch.Draw(o[54], oRect[54], Color.White);
            spriteBatch.Draw(o[55], oRect[55], Color.White);
            spriteBatch.Draw(o[56], oRect[56], Color.White);

            spriteBatch.Draw(o[57], oRect[57], Color.White);
            spriteBatch.Draw(o[58], oRect[58], Color.White);
            spriteBatch.Draw(o[59], oRect[59], Color.White);

            spriteBatch.Draw(o[60], oRect[60], Color.White);
            spriteBatch.Draw(o[61], oRect[61], Color.White);
            spriteBatch.Draw(o[62], oRect[62], Color.White);

            spriteBatch.Draw(o[63], oRect[63], Color.White);
            spriteBatch.Draw(o[64], oRect[64], Color.White);
            spriteBatch.Draw(o[65], oRect[65], Color.White);
            spriteBatch.Draw(o[66], oRect[66], Color.White);

            spriteBatch.Draw(o[67], oRect[67], Color.White);
            spriteBatch.Draw(o[68], oRect[68], Color.White);
            spriteBatch.Draw(o[69], oRect[69], Color.White);
            spriteBatch.Draw(o[70], oRect[70], Color.White);

            spriteBatch.Draw(o[71], oRect[71], Color.White);
            spriteBatch.Draw(o[72], oRect[72], Color.White);
            spriteBatch.Draw(o[73], oRect[73], Color.White);
            spriteBatch.Draw(o[74], oRect[74], Color.White);

            spriteBatch.Draw(o[75], oRect[75], Color.White);
            spriteBatch.Draw(o[76], oRect[76], Color.White);
            spriteBatch.Draw(o[77], oRect[77], Color.White);
            spriteBatch.Draw(o[78], oRect[78], Color.White);

            spriteBatch.Draw(o[79], oRect[79], Color.White);
            spriteBatch.Draw(o[80], oRect[80], Color.White);
            spriteBatch.Draw(o[81], oRect[81], Color.White);
            spriteBatch.Draw(o[82], oRect[82], Color.White);

            spriteBatch.Draw(o[83], oRect[83], Color.White);
            spriteBatch.Draw(o[84], oRect[84], Color.White);
            spriteBatch.Draw(o[85], oRect[85], Color.White);
            spriteBatch.Draw(o[86], oRect[86], Color.White);

            spriteBatch.Draw(o[87], oRect[87], Color.White);
            spriteBatch.Draw(o[88], oRect[88], Color.White);
            spriteBatch.Draw(o[89], oRect[89], Color.White);
            spriteBatch.Draw(o[90], oRect[90], Color.White);

            spriteBatch.Draw(o[91], oRect[91], Color.White);
            spriteBatch.Draw(o[92], oRect[92], Color.White);
            spriteBatch.Draw(o[93], oRect[93], Color.White);
            spriteBatch.Draw(o[94], oRect[94], Color.White);

            spriteBatch.Draw(o[95], oRect[95], Color.White);
            spriteBatch.Draw(o[96], oRect[96], Color.White);
            spriteBatch.Draw(o[97], oRect[97], Color.White);
            spriteBatch.Draw(o[98], oRect[98], Color.White);
            spriteBatch.Draw(o[99], oRect[99], Color.White);




            tictactoeAI.Draw(spriteBatch);
            //////////////////////////////////////////////////////////////////

            // debug draw strings for probability math and piece placement to be commented out at completion of project
            // proper algorithim work in xna is achieved by debug modes that show data algorithims on screen for 
            // proper coding.
            /////////////////////////////////////////////////////////////////////

            // to see if location is used in general
            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[0, 0].ToString(), new Vector2(300, 330), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[0, 1].ToString(), new Vector2(300, 360), Color.White);

            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[0, 2].ToString(), new Vector2(300, 390), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[1, 0].ToString(), new Vector2(300, 420), Color.White);

            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[1, 1].ToString(), new Vector2(300, 450), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[1, 2].ToString(), new Vector2(300, 480), Color.White);

            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[2, 0].ToString(), new Vector2(300, 510), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[2, 1].ToString(), new Vector2(300, 540), Color.White);

            spriteBatch.DrawString(spriteFont, tictactoeAI.IsLocationUsed[2, 2].ToString(), new Vector2(300, 570), Color.White);
            // collision count
           // spriteBatch.DrawString(spriteFont, "What is " + tictactoeAI.P_Turnpiece.ToString(), new Vector2(300, 590), Color.White);


            // to see if location is used by X
            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[0, 0].ToString(), new Vector2(400, 0), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[0, 1].ToString(), new Vector2(400, 20), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[0, 2].ToString(), new Vector2(400, 40), Color.White);

            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[1, 0].ToString(), new Vector2(400, 60), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[1, 1].ToString(), new Vector2(400, 80), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[1, 2].ToString(), new Vector2(400, 100), Color.White);

            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[2, 0].ToString(), new Vector2(400, 120), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[2, 1].ToString(), new Vector2(400, 140), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.LocationUsedByX[2, 2].ToString(), new Vector2(400, 160), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.P_Winner.ToString(), new Vector2(400, 180), Color.White);
            spriteBatch.DrawString(spriteFont, tictactoeAI.Winningformation.ToString(), new Vector2(400, 200), Color.White);
            spriteBatch.DrawString(spriteFont, mouseCount.ToString(), new Vector2(400, 220), Color.White);

            if (hitEnterToReplay == true)
            {
                spriteBatch.DrawString(spriteFont, "HIT ESCAPE TO CLOSE", new Vector2(512, 700), Color.Blue);
            }

            spriteBatch.DrawString(spriteFont, "HIT R FOR REPLAY AFTER GAME", new Vector2(100, 700), Color.Blue);
            //drawing of debug mode mouse location
            // used to detect the x and y for the algorithims
            spriteBatch.DrawString(spriteFont, "X: " + mouseCollision.X.ToString(), new Vector2(900, 300), Color.White);
            spriteBatch.DrawString(spriteFont, "Y: " + mouseCollision.Y.ToString(), new Vector2(900, 320), Color.White);

            //turn piece algorithim display
            spriteBatch.DrawString(spriteFont, "turnpiece" +  tictactoeAI.P_Turnpiece.ToString(), new Vector2(500, 0), Color.White);

            ////////////////////////////////////////////////////////////////////

            #endregion


            leapTimeButton.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, "isTimeSet: " + isTimeSet[6].ToString(), new Vector2(100,100),Color.White);
          //  spriteBatch.DrawString(spriteFont, "baseSeconds: " + baseSeconds.ToString(), new Vector2(100,200),  Color.White);
            spriteBatch.DrawString(spriteFont, "isTimedHit: " + timedHit[6].ToString(), new Vector2(100, 300), Color.White);
        }
        #endregion
    }
}
/*
comment only
Locations for each field in debug mode
textfile that maps locations 

 1 2 3 
 4 5 6 
 7 8 9 


1.) x: 150 y: 100
2.) x: 370 y: 90
3.) x: 570 y: 95
4.) x: 140 y: 290
5.) x: 365 y: 300
6.) x: 575 y: 290
7.) x: 155 y: 520
8.) x: 365 y: 510   
9.) x: 570 y: 520
*/

/*

 * Probability mathematics begins with all possible options, all possible ends and is called artificial intelligence when used 
 * in computer scientists.
 * The last part is the middle of probability mathe matics which is the proper middle.
 * The middle should only be one equation used over and over again to come up with the same conclusion.
 * If more than one equation used then it is a set of equations that should be used each and every time.
 * As such probability mathematics in this situation is 
 * all possible options for x and o (update method & tic tac toe moves)
 * all winning conditions for x and o (update method & tic tac toe moves)
 * the equation that dictates the o option perfectly each time (yet to be created as of May 06 2014)
 * Today I will make history by delivering a crushing defeat to all doubt. Code is Gold.   - Jimmy Ellis 05/06/2014
 * 
 * Random Equation: The equation randomly picks a square that is either above or below left or right of the x chosen
 * as x is will and always will be something that either has one or two squares next to it as it is only defeated by
 * O with the opening move being O.
 * That in mathematics is yet to be seen. - Jimmy 05/06/2014
*/
