using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace TicTacToe
{
    class TicTacToeAI : GameScreen
    {
        #region Fields

        Texture2D blankBackground;
        Rectangle blankBackgroundRect;

        Texture2D gameBoard;
        Rectangle gameBoardRect;

        Texture2D[] o;
        Rectangle[] oRect;

        Texture2D[] x;
        Rectangle[] xRect;
        int width = 1024;
        int height = 720;

        // the mouse 
        MouseState gameMouse;

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
        bool[,] isLocationUsed;
        bool[,] locationUsedByX;
        bool[,] locationUsedByO;
        bool terminateLoop;
        // gameboard location enumeration for better huamn understanding
        public enum GameBoardLocation
        {
            empty, row1x1, row1x2, row1x3, row2x1, row2x2, row2x3, row3x1, row3x2, row3x3
        }
        GameBoardLocation gameboardlocation;

        // enumeration for victory
        public enum WinningFormation
        {
            nowin, row1win, row2win, row3win, column1win, column2win, column3win, topleftdiagnolwin, toprightdiagnolwin, catgame
        }
         WinningFormation winningformation;

        // activate to activate winning condition
        // who wins

        bool hitEnterToReplay;
        public enum Winner
        {
            nowins, xwins, owins, catgame
        }
         Winner winner;

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

        #region Properties 

        // Properties to Send:
        // piece location properties
        // ---- no send ---Vector2[,] boxLocations;
        // enum WinningFormations, winningformations
        /*  ---checked propertied----  bool[,] isLocationUsed;
        =-- checked properited ==== bool[,] locationUsedByX;
        bool[,] --- locationUsedByO -----locationUsedByO;
         * turn properites 
         * --- sent through properites---- enum GameBoardLocation
         * --- sent through properties --- GameBoardLocation gameboardlocation
         *  --- sent through properties --- enum WinningFormation 
         * --- sent through properties ---  WinningFormation  winningformation
         *  enum Winner
         *   Winner winner;
         *    enum TurnPiece
         *      TurnPiece turnpiece
         *      boxCollisionRect
         */

        // piece location properties 

        public Rectangle[] BoxCollisionRect
        {
            get
            {
                return boxCollisionRect;
            }
            set
            {
                value = boxCollisionRect;
            }
        }
        #region Piece Statistics Properties 

        public TurnPiece P_Turnpiece
        {
            get
            {
                return turnpiece;
            }

            set
            {
                value = turnpiece; 
            }
        }
        public Winner P_Winner
        {
            get
            {
                return winner;
            }
            set
            {
                value = winner;
            }

        }

        public WinningFormation Winningformation
        {
            get
            {
                return winningformation;
            }

            set
            {
                value = winningformation;
            }
        }
      
        public GameBoardLocation Gameboardlocation
        {
            get
            {
                return gameboardlocation;
            }

            set
            {
                value = gameboardlocation;
            }
        }

        #endregion 
        #region Piece Location Properties
        public bool[,] IsLocationUsed
        {
            get
            {

                return isLocationUsed;
            }
            set
            {
                value = isLocationUsed; 
            }
        }

        public bool[,] LocationUsedByX
        {
            get
            {
                return locationUsedByX;
            }
            set
            {
                value = locationUsedByX;
            }
        }

        public bool[,] LocationUsedByO
        {
            get
            {
                return locationUsedByO;
            }
            set
            {
                value = locationUsedByO;
            }
        }
        #endregion 

        #endregion



        #region Constructors
        public TicTacToeAI()
        {

            
            
       
            
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

            for (int r = 0; r < oRect.Length; r++)
            {
                oRect[r] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            }
            terminateLoop = false;
            /*
            xRect[0] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[1] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[2] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[3] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[4] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[5] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[6] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[7] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);
            xRect[8] = new Rectangle(0, 0, (int)pieceSize.X, (int)pieceSize.Y);

            
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
             */
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
            isLocationUsed = new bool[3, 3];

            // nested double for loop that initializes all in the multi dimensional array as false for update method
            // deep logical algorithims
            for (int a = 0; a < 3; a++)
            {

                for (int b = 0; b < 3; b++)
                {
                    isLocationUsed[a, b] = false;

                }


            }
            // boolean multi dimensional array to see if x is using the spot 
            locationUsedByX = new bool[3, 3];
            for (int a = 0; a < 3; a++)
            {

                for (int b = 0; b < 3; b++)
                {
                    locationUsedByX[a, b] = false;

                }


            }
            // double nested for loop to set all values to false


            // boolean multi dimensional array to see if o is using the spot 
            locationUsedByO = new bool[3, 3];
            for (int a = 0; a < 3; a++)
            {

                for (int b = 0; b < 3; b++)
                {
                    locationUsedByO[a, b] = false;

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

        public override void LoadContent(ContentManager Content)
        {

            o = new Texture2D[100];
            for (int z = 0; z < 100; z++)
            {
                o[z] = Content.Load<Texture2D>("Pieces/o");
            }

            boxCollision = new Texture2D[9];

            boxCollision[0] = Content.Load<Texture2D>("Collisions/boxcollision01");
            boxCollision[1] = Content.Load<Texture2D>("Collisions/boxcollision02");
            boxCollision[2] = Content.Load<Texture2D>("Collisions/boxcollision03");

            boxCollision[3] = Content.Load<Texture2D>("Collisions/boxcollision04");
            boxCollision[4] = Content.Load<Texture2D>("Collisions/boxcollision05");
            boxCollision[5] = Content.Load<Texture2D>("Collisions/boxcollision06");

            boxCollision[6] = Content.Load<Texture2D>("Collisions/boxcollision07");
            boxCollision[7] = Content.Load<Texture2D>("Collisions/boxcollision08");
            boxCollision[8] = Content.Load<Texture2D>("Collisions/boxcollision09");





            base.LoadContent(Content);
            
            
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
           WinningConditions();



            #region o turn
            if ((TurnPiece.oturn == turnpiece) && (Winner.nowins == winner))
            {
               // mouseCount = 0;
               // terminateLoop = false;
                // if (gameMouse.LeftButton == ButtonState.Pressed)
                //{

                // ARTIFICIAL INTELLIGENCE HIERACHY!
                // 
                if (isFirstMove == false)
                {
                    // HIERACHY 1 FIRST MOVE BY JIMMY ELLIS
                    TicTacToeMoves();
                    isFirstMove = true;
                }


                // THE FINAL A.I TECHNIQUE - THE HUMAN DECISION.
                // ALL THE ARTIFICIAL INTELLIGENCE PROBABILITY MATHEMATICS ALGORITHIM HAVE BEEN WRITTEN
                // THE COMPUTER CALLS  O_ToWinPieceMoves only to make O win but sometimes X will win if that is called.
                // If StopX_ToWinPieceMoves is called first then O may not make the decision to win.
                // AS SUCH A FINAL ALGORITHIM MUST BE WRITTEN TO DECIDE WHICH ONE SHOULD BE DECIDED!
                // GENIUS GAME! - Jimmy Ellis
                // HIERACHY 2 STOP X FROM WINNING BY JIMMY ELLIS
                //  O_ToWinPieceMoves();

                OX_DecisionWinner();

                // HIERACHY 3 CAUSE O TO WIN BY JIMMY ELLIS!
                // StopX_ToWinPieceMoves();
                O_DecisionAlgorithim();
                StopX_ToWinPieceMoves();
                O_ToWinPieceMoves();
                // HIERACHY 4 CALCULATED RESPONSE MOVES TO PLAYER BY JIMMY ELLIS!
                O_PieceMoves();

                // HIERACHY 5 LAST OF MISCELLENIOUS MOVEMENTS


                // }
            }
            #endregion



            #region somebody won

            else if ((Winner.owins == winner) || (Winner.xwins == winner) || (Winner.nowins != winner))
            {
                hitEnterToReplay = true;
            }

            if ((hitEnterToReplay == true))
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter))
                {
                    ScreenManager.Instance.AddScreen(new TicTacToeTitleScreen());
                }
            }
            #endregion

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

           



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
            //////////////////////////////////////////////////////////////////

        }



        #region Victory Deciion Algorithim

        public void OX_DecisionWinner()
        {
            // algorithim in the a.i. hierachy. makes sure that when x and o both have two empty spaces and it is O's turn O win is chosen.
            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[0, 0] == true) && (isLocationUsed[0, 1] == true) && (isLocationUsed[1, 0] == true) && (isLocationUsed[1, 1] == true))
                {

                    if ((locationUsedByX[0, 0] == true) && (locationUsedByO[0, 1] == true) && (locationUsedByX[1, 0] == true) && (locationUsedByO[1, 1] == true))
                    {

                        if ((isLocationUsed[2, 1] == false) && (locationUsedByO[2, 1] == false) && (locationUsedByX[2, 1] == false))
                        {
                            oRect[76].X = Convert.ToInt32(boxLocations[2, 1].X);
                            oRect[76].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                            locationUsedByO[2, 1] = true;
                            isLocationUsed[2, 1] = true;
                            turnpiece = TurnPiece.xturn;
                        }

                    }
                }
            }
            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[0, 0] == true) && (isLocationUsed[0, 1] == true) && (isLocationUsed[1, 0] == true) && (isLocationUsed[1, 1] == true))
                {
                    if ((locationUsedByX[0, 0] == true) && (locationUsedByX[0, 1] == true) && (locationUsedByO[1, 0] == true) && (locationUsedByO[1, 1] == true))
                    {

                        if ((isLocationUsed[1, 2] == false) && (locationUsedByO[1, 2] == false) && (locationUsedByX[1, 2] == false))
                        {
                            oRect[77].X = Convert.ToInt32(boxLocations[1, 2].X);
                            oRect[77].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                            locationUsedByO[1, 2] = true;
                            isLocationUsed[1, 2] = true;
                            turnpiece = TurnPiece.xturn;
                        }
                    }
                }
            }
            ///////////////////
            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[0, 1] == true) && (isLocationUsed[0, 2] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[1, 2] == true))
                {
                    if ((locationUsedByO[0, 1] == true) && (locationUsedByX[0, 2] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByX[1, 2] == true))
                    {

                        if ((isLocationUsed[2, 1] == false) && (locationUsedByO[2, 1] == false) && (locationUsedByX[2, 1] == false))
                        {
                            oRect[78].X = Convert.ToInt32(boxLocations[2, 1].X);
                            oRect[78].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                            locationUsedByO[2, 1] = true;
                            isLocationUsed[2, 1] = true;
                            turnpiece = TurnPiece.xturn;
                        }
                    }
                }
            }
            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[0, 1] == true) && (isLocationUsed[0, 2] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[1, 2] == true))
                {

                    if ((locationUsedByX[0, 1] == true) && (locationUsedByX[0, 2] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByO[1, 2] == true))
                    {
                        if ((isLocationUsed[1, 0] == false) && (locationUsedByO[1, 0] == false) && (locationUsedByX[1, 0] == false))
                        {

                            oRect[79].X = Convert.ToInt32(boxLocations[1, 0].X);
                            oRect[79].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                            locationUsedByO[1, 0] = true;
                            isLocationUsed[1, 0] = true;
                            turnpiece = TurnPiece.xturn;
                        }
                    }
                }
            }
            //////////////////////////////////


            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[1, 0] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[2, 0] == true) && (isLocationUsed[2, 1] == true))
                {

                    if ((locationUsedByO[1, 0] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByX[2, 0] == true) && (locationUsedByX[2, 1] == true))
                    {
                        if ((isLocationUsed[1, 2] == false) && (locationUsedByO[1, 2] == false) && (locationUsedByX[1, 2] == false))
                        {
                            oRect[80].X = Convert.ToInt32(boxLocations[1, 2].X);
                            oRect[80].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                            locationUsedByO[1, 2] = true;
                            isLocationUsed[1, 2] = true;
                            turnpiece = TurnPiece.xturn;
                        }

                    }

                }
            }
            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[1, 0] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[2, 0] == true) && (isLocationUsed[2, 1] == true))
                {

                    if ((locationUsedByX[1, 0] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByX[2, 0] == true) && (locationUsedByO[2, 1] == true))
                    {

                        if ((isLocationUsed[0, 1] == false) && (locationUsedByO[0, 1] == false) && (locationUsedByX[1, 0] == false))
                        {
                            oRect[81].X = Convert.ToInt32(boxLocations[0, 1].X);
                            oRect[81].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                            locationUsedByO[0, 1] = true;
                            isLocationUsed[0, 1] = true;
                            turnpiece = TurnPiece.xturn;
                        }
                    }

                }
            }
            /////////////////////////////////

            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[1, 1] == true) && (isLocationUsed[1, 2] == true) && (isLocationUsed[2, 1] == true) && (isLocationUsed[2, 2] == true))
                {

                    if ((locationUsedByO[1, 1] == true) && (locationUsedByO[1, 2] == true) && (locationUsedByX[2, 1] == true) && (locationUsedByX[2, 2] == true))
                    {

                        if ((isLocationUsed[1, 0] == false) && (locationUsedByO[1, 0] == false) && (locationUsedByX[1, 0] == false))
                        {
                            oRect[82].X = Convert.ToInt32(boxLocations[1, 0].X);
                            oRect[82].Y = Convert.ToInt32(boxLocations[1, 0].Y);

                            locationUsedByO[1, 0] = true;
                            isLocationUsed[1, 0] = true;
                            turnpiece = TurnPiece.xturn;
                        }
                    }



                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((isLocationUsed[1, 1] == true) && (isLocationUsed[1, 2] == true) && (isLocationUsed[2, 1] == true) && (isLocationUsed[2, 2] == true))
                {

                    if ((locationUsedByO[1, 1] == true) && (locationUsedByX[1, 2] == true) && (locationUsedByO[2, 1] == true) && (locationUsedByX[2, 2] == true))
                    {

                        if ((isLocationUsed[0, 1] == false) && (locationUsedByO[0, 1] == false) && (locationUsedByX[1, 0] == false))
                        {
                            oRect[83].X = Convert.ToInt32(boxLocations[0, 1].X);
                            oRect[83].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                            locationUsedByO[0, 1] = true;
                            isLocationUsed[0, 1] = true;
                            turnpiece = TurnPiece.xturn;
                        }
                    }

                }
            }
        }


        #endregion


        // Methods to Go Into another class for probability mathematics 
        #region WinningConditions
        public void WinningConditions()
        {
            // to calculate the winning conditions for the player based on the winning conditions 
            // of a usual tic tac to game. algorithims all by Jimmy Ellis
            // tic tac toe three in a row row 1  left to right, row 2 left to right, row 3 left to right


            #region Algorithim: row1x1, row1x2, row1x3 win
            if ((isLocationUsed[0, 0] == true) && (isLocationUsed[0, 1] == true) && (isLocationUsed[0, 2] == true))
            {

                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[0, 1] == true) && (locationUsedByX[0, 2] == true))
                {
                    winningformation = WinningFormation.row1win;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[0, 1] == true) && (locationUsedByO[0, 2] == true))
                {

                    winningformation = WinningFormation.row1win;
                    winner = Winner.owins;
                }


            }
            #endregion

            #region Algorithim: row 2x1, 2x2, 2x3 win
            if ((isLocationUsed[1, 0] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[1, 2] == true))
            {
                if ((locationUsedByX[1, 0] == true) && (locationUsedByX[1, 1] == true) && (locationUsedByX[1, 2] == true))
                {
                    winningformation = WinningFormation.row2win;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[1, 0] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByO[1, 2] == true))
                {

                    winningformation = WinningFormation.row2win;
                    winner = Winner.owins;
                }
            }
            #endregion

            #region Algorithim: row 3x1, 3x2, 3x3
            if ((isLocationUsed[2, 0] == true) && (isLocationUsed[2, 1] == true) && (isLocationUsed[2, 2] == true))
            {
                if ((locationUsedByX[2, 0] == true) && (locationUsedByX[2, 1] == true) && (locationUsedByX[2, 2] == true))
                {
                    winningformation = WinningFormation.row3win;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[2, 0] == true) && (locationUsedByO[2, 1] == true) && (locationUsedByO[2, 2] == true))
                {

                    winningformation = WinningFormation.row3win;
                    winner = Winner.owins;
                }

            }
            #endregion
            // end tic tac toe column win

            // tic tac toe column win column 1, 2, and 3
            #region Algorithim: row 1x1, row2x1, row3x1
            if ((isLocationUsed[0, 0] == true) && (isLocationUsed[1, 0] == true) && (isLocationUsed[2, 0] == true))
            {
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[1, 0] == true) && (locationUsedByX[2, 0] == true))
                {
                    winningformation = WinningFormation.column1win;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[1, 0] == true) && (locationUsedByO[2, 0] == true))
                {

                    winningformation = WinningFormation.column1win;
                    winner = Winner.owins;
                }
            }
            #endregion
            #region Algorithim: row1x2, 2x2, 3x2
            if ((isLocationUsed[0, 1] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[2, 1] == true))
            {
                if ((locationUsedByX[0, 1] == true) && (locationUsedByX[1, 1] == true) && (locationUsedByX[2, 1] == true))
                {
                    winningformation = WinningFormation.column2win;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[0, 1] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByO[2, 1] == true))
                {

                    winningformation = WinningFormation.column2win;
                    winner = Winner.owins;
                }
            }
            #endregion

            #region Algorithim: 1x3, 2x3, 3x3
            if ((isLocationUsed[0, 2] == true) && (isLocationUsed[1, 2] == true) && (isLocationUsed[2, 2] == true))
            {
                if ((locationUsedByX[0, 2] == true) && (locationUsedByX[1, 2] == true) && (locationUsedByX[2, 2] == true))
                {
                    winningformation = WinningFormation.column3win;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[0, 2] == true) && (locationUsedByO[1, 2] == true) && (locationUsedByO[2, 2] == true))
                {

                    winningformation = WinningFormation.column3win;
                    winner = Winner.owins;
                }
            }
            #endregion
            // end tic tac toe column win

            // tic tac toe diagnol win
            #region Algorithim: row1x1, row2x2, row3x3
            if ((isLocationUsed[0, 0] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[2, 2] == true))
            {
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[1, 1] == true) && (locationUsedByX[2, 2] == true))
                {
                    winningformation = WinningFormation.topleftdiagnolwin;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByO[2, 2] == true))
                {

                    winningformation = WinningFormation.topleftdiagnolwin;
                    winner = Winner.owins;
                }

            }
            #endregion

            #region Algorithim: row1x3, row2x2, row3x1
            if ((isLocationUsed[0, 2] == true) && (isLocationUsed[1, 1] == true) && (isLocationUsed[2, 0] == true))
            {
                if ((locationUsedByX[0, 2] == true) && (locationUsedByX[1, 1] == true) && (locationUsedByX[2, 0] == true))
                {
                    winningformation = WinningFormation.toprightdiagnolwin;
                    winner = Winner.xwins;
                }

                else if ((locationUsedByO[0, 2] == true) && (locationUsedByO[1, 1] == true) && (locationUsedByO[2, 0] == true))
                {

                    winningformation = WinningFormation.toprightdiagnolwin;
                    winner = Winner.owins;
                }

            }
            int count = 0;
            for (int z = 0; z < 3; z++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (isLocationUsed[z, y] == true)
                    {
                        count++;
                    }


                }
            }

            if (count == 9)
            {
                winningformation = WinningFormation.catgame;
                winner = Winner.catgame;
            }

            #endregion
            // end tic tac toe diagnol win

        }
        #endregion



        #region Final Decision Algorithim

        public void O_DecisionAlgorithim()
        {
            #region Moves to Make In Relationship To Self and Win (Priority 1)

            if (TurnPiece.oturn == turnpiece)
            {
                // row 1x1, row1x2
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[0, 1] == true))
                {
                    StopX_ToWinPieceMoves();

                }

                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[0, 1] == true))
                {

                    O_ToWinPieceMoves();
                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                //row 1x2, row1x3
                if ((locationUsedByX[0, 1] == true) && (locationUsedByX[0, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }

                else if ((locationUsedByO[0, 1] == true) && (locationUsedByO[0, 2] == true))
                {
                    O_ToWinPieceMoves();
                }
                // row1x1, row1x3

            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[0, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[0, 2] == true))
                {
                    O_ToWinPieceMoves();
                }

            }
            /////////////////////////////////////////
            // row 2x1, row 2x2

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[1, 0] == true) && (locationUsedByX[1, 1] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[1, 0] == true) && (locationUsedByO[1, 1] == true))
                {
                    O_ToWinPieceMoves();
                }

            }
            if (TurnPiece.oturn == turnpiece)
            {
                // row 2x2, row 2x3
                if ((locationUsedByX[1, 1] == true) && (locationUsedByX[1, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[1, 1] == true) && (locationUsedByO[1, 2] == true))
                {
                    O_ToWinPieceMoves();
                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                // row 2x1, row 2x3
                if ((locationUsedByX[1, 0] == true) && (locationUsedByX[1, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[1, 0] == true) && (locationUsedByO[1, 2] == true))
                {
                    O_ToWinPieceMoves();
                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                ///////////////////////////////////////
                //row3x1, row3x2
                if ((locationUsedByX[2, 0] == true) && (locationUsedByX[2, 1] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[2, 0] == true) && (locationUsedByO[2, 1] == true))
                {
                    O_ToWinPieceMoves();
                }

            }

            if (TurnPiece.oturn == turnpiece)
            {

                //row3x2, row3x3
                if ((locationUsedByX[2, 1] == true) && (locationUsedByX[2, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[2, 1] == true) && (locationUsedByO[2, 2] == true))
                {
                    O_ToWinPieceMoves();
                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                //row3x1, row3x3
                if ((locationUsedByX[2, 0] == true) && (locationUsedByX[2, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[2, 0] == true) && (locationUsedByO[2, 2] == true))
                {
                    O_ToWinPieceMoves();
                }

            }
            /////////////////////////////////////////////////////////

            // COLUMNS // 

            ////////////////////////////////////////////////////////
            // left most column 
            // row1x1, row2x1

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[1, 0] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[1, 0] == true))
                {
                    O_ToWinPieceMoves();
                }
            }
            if (TurnPiece.oturn == turnpiece)
            {
                // row2x1, row3x1
                if ((locationUsedByX[1, 0] == true) && (locationUsedByX[2, 0] == true))
                {
                    StopX_ToWinPieceMoves();

                }
                else if ((locationUsedByO[1, 0] == true) && (locationUsedByO[2, 0] == true))
                {
                    O_ToWinPieceMoves();

                }
            }


            if (TurnPiece.oturn == turnpiece)
            {
                // row1x1, row3x1
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[2, 0] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[2, 0] == true))
                {
                    O_ToWinPieceMoves();
                }
            }
            ////////////////////
            // middle column //
            //////////////////
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 1] == true) && (locationUsedByX[1, 1] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 1] == true) && (locationUsedByO[1, 1] == true))
                {
                    O_ToWinPieceMoves();
                }
            }
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[1, 1] == true) && (locationUsedByX[2, 1] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[1, 1] == true) && (locationUsedByO[2, 1] == true))
                {
                    O_ToWinPieceMoves();
                }
            }
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 1] == true) && (locationUsedByX[2, 1] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 1] == true) && (locationUsedByO[2, 1] == true))
                {
                    O_ToWinPieceMoves();
                }
            }
            ///////////////////////
            // right most column
            ///////////////////////
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 2] == true) && (locationUsedByX[1, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 2] == true) && (locationUsedByO[1, 2] == true))
                {
                    O_ToWinPieceMoves();
                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[1, 2] == true) && (locationUsedByX[2, 2] == true))
                {

                    StopX_ToWinPieceMoves();

                }
                else if ((locationUsedByO[1, 2] == true) && (locationUsedByO[2, 2] == true))
                {

                    O_ToWinPieceMoves();

                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 2] == true) && (locationUsedByX[2, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 2] == true) && (locationUsedByO[2, 2] == true))
                {
                    O_ToWinPieceMoves();
                }

            }


            if (TurnPiece.oturn == turnpiece)
            {
                ///////////////////////////
                //diagnol from left to right 
                //////////////////////////////
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[1, 1] == true))
                {
                    StopX_ToWinPieceMoves();

                }

                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[1, 1] == true))
                {
                    O_ToWinPieceMoves();

                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[1, 1] == true) && (locationUsedByX[2, 2] == true))
                {

                    StopX_ToWinPieceMoves();

                }

                else if ((locationUsedByO[1, 1] == true) && (locationUsedByO[2, 2] == true))
                {

                    O_ToWinPieceMoves();

                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[2, 2] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 0] == true) && (locationUsedByO[2, 2] == true))
                {
                    O_ToWinPieceMoves();
                }
            }
            //////////////////////////////
            // diagnal from right to left
            //////////////////////////////
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 2] == true) && (locationUsedByX[1, 1] == true))
                {
                    StopX_ToWinPieceMoves();
                }
                else if ((locationUsedByO[0, 2] == true) && (locationUsedByO[1, 1] == true))
                {
                    O_ToWinPieceMoves();
                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[1, 1] == true) && (locationUsedByX[2, 0] == true))
                {
                    StopX_ToWinPieceMoves();

                }
                else if ((locationUsedByX[1, 1] == true) && (locationUsedByX[2, 0] == true))
                {
                    O_ToWinPieceMoves();

                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByX[0, 2] == true) && (locationUsedByX[2, 0] == true))
                {
                    StopX_ToWinPieceMoves();

                }
                else if ((locationUsedByX[0, 2] == true) && (locationUsedByX[2, 0] == true))
                {

                    O_ToWinPieceMoves();
                }

            }


            #endregion

        }


        #endregion 

  

        


        public void O_ToWinPieceMoves()
        {
            // the deepest greatest 22 algorithims of tic tac toe ever written by Jimmy Ellis 
            #region Moves to Make In Relationship To Self and Win (Priority 1)

            if (TurnPiece.oturn == turnpiece)
            {
                // row 1x1, row1x2
                if ((locationUsedByO[0, 0] == true) && (locationUsedByO[0, 1] == true))
                {
                    if ((locationUsedByO[0, 2] == false) && (locationUsedByX[0, 2] == false) && (isLocationUsed[0, 2] == false))
                    {
                        oRect[18].X = Convert.ToInt32(boxLocations[0, 2].X);
                        oRect[18].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        locationUsedByO[0, 2] = true;
                        isLocationUsed[0, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 2] && isLocationUsed[0, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }

                }
            }
            //row 1x2, row1x3
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 1] == true) && (locationUsedByO[0, 2] == true))
                {
                    if ((locationUsedByO[0, 0] == false) && (locationUsedByX[0, 0] == false) && (isLocationUsed[0, 0] == false))
                    {
                        oRect[19].X = Convert.ToInt32(boxLocations[0, 0].X);
                        oRect[19].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                        locationUsedByO[0, 0] = true;
                        isLocationUsed[0, 0] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 0] && isLocationUsed[0, 0])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            // row1x1, row1x3

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 0] == true) && (locationUsedByO[0, 2] == true))
                {
                    if ((locationUsedByO[0, 1] == false) && (locationUsedByX[0, 1] == false) && (isLocationUsed[0, 1] == false))
                    {
                        oRect[20].X = Convert.ToInt32(boxLocations[0, 1].X);
                        oRect[20].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        locationUsedByO[0, 1] = true;
                        isLocationUsed[0, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 1] && isLocationUsed[0, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            /////////////////////////////////////////
            // row 2x1, row 2x2

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 0] == true) && (locationUsedByO[1, 1] == true))
                {
                    if ((locationUsedByO[1, 2] == false) && (locationUsedByX[1, 2] == false) && (isLocationUsed[1, 2] == false))
                    {
                        oRect[21].X = Convert.ToInt32(boxLocations[1, 2].X);
                        oRect[21].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        locationUsedByO[1, 2] = true;
                        isLocationUsed[1, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 2] && isLocationUsed[1, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            // row 2x2, row 2x3

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 1] == true) && (locationUsedByO[1, 2] == true))
                {
                    if ((locationUsedByO[1, 0] == false) && (locationUsedByX[1, 0] == false) && (isLocationUsed[1, 0] == false))
                    {
                        oRect[22].X = Convert.ToInt32(boxLocations[1, 0].X);
                        oRect[22].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 0] && isLocationUsed[1, 0])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            // row 2x1, row 2x3

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 0] == true) && (locationUsedByO[1, 2] == true))
                {
                    if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                    {
                        oRect[23].X = Convert.ToInt32(boxLocations[1, 1].X);
                        oRect[23].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                        locationUsedByO[1, 1] = true;
                        isLocationUsed[1, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            ///////////////////////////////////////
            //row3x1, row3x2
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[2, 0] == true) && (locationUsedByO[2, 1] == true))
                {
                    if ((locationUsedByO[2, 2] == false) && (locationUsedByX[2, 2] == false) && (isLocationUsed[2, 2] == false))
                    {
                        oRect[24].X = Convert.ToInt32(boxLocations[2, 2].X);
                        oRect[24].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                        locationUsedByO[2, 2] = true;
                        isLocationUsed[2, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[2, 2] && isLocationUsed[2, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            //row3x2, row3x3

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[2, 1] == true) && (locationUsedByO[2, 2] == true))
                {
                    if ((locationUsedByO[2, 0] == false) && (locationUsedByX[2, 0] == false) && (isLocationUsed[2, 0] == false))
                    {
                        oRect[25].X = Convert.ToInt32(boxLocations[2, 0].X);
                        oRect[25].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                        locationUsedByO[2, 0] = true;
                        isLocationUsed[2, 0] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[2, 0] && isLocationUsed[2, 0])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            //row3x1, row3x3

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[2, 0] == true) && (locationUsedByO[2, 2] == true))
                {
                    if ((locationUsedByO[2, 1] == false) && (locationUsedByX[2, 1] == false) && (isLocationUsed[2, 1] == false))
                    {
                        oRect[26].X = Convert.ToInt32(boxLocations[2, 1].X);
                        oRect[26].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                        locationUsedByO[2, 1] = true;
                        isLocationUsed[2, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[2, 1] && isLocationUsed[2, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            /////////////////////////////////////////////////////////

            // COLUMNS // 

            ////////////////////////////////////////////////////////
            // left most column 
            // row1x1, row2x1

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 0] == true) && (locationUsedByO[1, 0] == true))
                {
                    if ((locationUsedByO[2, 0] == false) && (locationUsedByX[2, 0] == false) && (isLocationUsed[2, 0] == false))
                    {
                        oRect[27].X = Convert.ToInt32(boxLocations[2, 0].X);
                        oRect[27].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                        locationUsedByO[2, 0] = true;
                        isLocationUsed[2, 0] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[2, 0] && isLocationUsed[2, 0])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            // row2x1, row3x1

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 0] == true) && (locationUsedByO[2, 0] == true))
                {
                    if ((locationUsedByO[0, 0] == false) && (locationUsedByX[0, 0] == false) && (isLocationUsed[0, 0] == false))
                    {
                        oRect[28].X = Convert.ToInt32(boxLocations[0, 0].X);
                        oRect[28].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                        locationUsedByO[0, 0] = true;
                        isLocationUsed[0, 0] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 0] && isLocationUsed[0, 0])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            // row1x1, row3x1

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 0] == true) && (locationUsedByO[2, 0] == true))
                {
                    if ((locationUsedByO[1, 0] == false) && (locationUsedByX[1, 0] == false) && (isLocationUsed[1, 0] == false))
                    {
                        oRect[29].X = Convert.ToInt32(boxLocations[1, 0].X);
                        oRect[29].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 0] && isLocationUsed[1, 0])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }

            ////////////////////
            // middle column //
            //////////////////
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 1] == true) && (locationUsedByO[1, 1] == true))
                {
                    if ((locationUsedByO[2, 1] == false) && (locationUsedByX[2, 1] == false) && (isLocationUsed[2, 1] == false))
                    {
                        oRect[30].X = Convert.ToInt32(boxLocations[2, 1].X);
                        oRect[30].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                        locationUsedByO[2, 1] = true;
                        isLocationUsed[2, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[2, 1] && isLocationUsed[2, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 1] == true) && (locationUsedByO[2, 1] == true))
                {
                    if ((locationUsedByO[0, 1] == false) && (locationUsedByX[0, 1] == false) && (isLocationUsed[0, 1] == false))
                    {
                        oRect[31].X = Convert.ToInt32(boxLocations[0, 1].X);
                        oRect[31].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        locationUsedByO[0, 1] = true;
                        isLocationUsed[0, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 1] && isLocationUsed[0, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }

                    }
                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 1] == true) && (locationUsedByO[2, 1] == true))
                {
                    if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                    {
                        oRect[32].X = Convert.ToInt32(boxLocations[1, 1].X);
                        oRect[32].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                        locationUsedByO[1, 1] = true;
                        isLocationUsed[1, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            ///////////////////////
            // right most column
            ///////////////////////

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 2] == true) && (locationUsedByO[1, 2] == true))
                {
                    if ((locationUsedByO[2, 2] == false) && (locationUsedByX[2, 2] == false) && (isLocationUsed[2, 2] == false))
                    {
                        oRect[33].X = Convert.ToInt32(boxLocations[2, 2].X);
                        oRect[33].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                        locationUsedByO[2, 2] = true;
                        isLocationUsed[2, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[2, 2] && isLocationUsed[2, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 2] == true) && (locationUsedByO[2, 2] == true))
                {
                    if ((locationUsedByO[0, 2] == false) && (locationUsedByX[0, 2] == false) && (isLocationUsed[0, 2] == false))
                    {
                        oRect[34].X = Convert.ToInt32(boxLocations[0, 2].X);
                        oRect[34].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        locationUsedByO[0, 2] = true;
                        isLocationUsed[0, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 2] && isLocationUsed[0, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 2] == true) && (locationUsedByO[2, 2] == true))
                {
                    if ((locationUsedByO[1, 2] == false) && (locationUsedByX[1, 2] == false) && (isLocationUsed[1, 2] == false))
                    {
                        oRect[35].X = Convert.ToInt32(boxLocations[1, 2].X);
                        oRect[35].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        locationUsedByO[1, 2] = true;
                        isLocationUsed[1, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 2] && isLocationUsed[1, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }

            ///////////////////////////
            //diagnol from left to right 
            //////////////////////////////


            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 0] == true) && (locationUsedByO[1, 1] == true))
                {
                    if ((locationUsedByO[2, 2] == false) && (locationUsedByX[2, 2] == false) && (isLocationUsed[2, 2] == false))
                    {
                        oRect[36].X = Convert.ToInt32(boxLocations[2, 2].X);
                        oRect[36].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                        locationUsedByO[2, 2] = true;
                        isLocationUsed[2, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[2, 2] && isLocationUsed[2, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }

            }


            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 1] == true) && (locationUsedByO[2, 2] == true))
                {
                    if ((locationUsedByO[0, 0] == false) && (locationUsedByX[0, 0] == false) && (isLocationUsed[0, 0] == false))
                    {
                        oRect[37].X = Convert.ToInt32(boxLocations[0, 0].X);
                        oRect[37].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                        locationUsedByO[0, 0] = true;
                        isLocationUsed[0, 0] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 0] && isLocationUsed[0, 0])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }

                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 0] == true) && (locationUsedByO[2, 2] == true))
                {
                    if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                    {
                        oRect[38].X = Convert.ToInt32(boxLocations[1, 1].X);
                        oRect[38].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                        locationUsedByO[1, 1] = true;
                        isLocationUsed[1, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }
            }
            //////////////////////////////
            // diagnal from right to left
            //////////////////////////////
            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 2] == true) && (locationUsedByO[1, 1] == true))
                {
                    if ((locationUsedByO[2, 0] == false) && (locationUsedByX[2, 0] == false) && (isLocationUsed[2, 0] == false))
                    {
                        oRect[39].X = Convert.ToInt32(boxLocations[2, 0].X);
                        oRect[39].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                        locationUsedByO[2, 0] = true;
                        isLocationUsed[2, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }
                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[1, 1] == true) && (locationUsedByO[2, 0] == true))
                {
                    if ((locationUsedByO[0, 2] == false) && (locationUsedByX[0, 2] == false) && (isLocationUsed[0, 2] == false))
                    {
                        oRect[40].X = Convert.ToInt32(boxLocations[0, 2].X);
                        oRect[40].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        locationUsedByO[0, 2] = true;
                        isLocationUsed[0, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 2] && isLocationUsed[0, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                if ((locationUsedByO[0, 2] == true) && (locationUsedByO[2, 0] == true))
                {
                    if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                    {
                        oRect[41].X = Convert.ToInt32(boxLocations[1, 1].X);
                        oRect[41].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                        locationUsedByO[1, 1] = true;
                        isLocationUsed[1, 1] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }
                }

            }


            #endregion

        }




        // a copy of the above a.i. method that is redesigned to stop x from winning under such conditions 
        public void StopX_ToWinPieceMoves()
        {
            // the deepest greatest 22 algorithims of tic tac toe ever written by Jimmy Ellis 
            #region Moves to Make In Relationship To Self and Win (Priority 1)

            if (TurnPiece.oturn == turnpiece)
            {
                // row 1x1, row1x2
                if ((locationUsedByX[0, 0] == true) && (locationUsedByX[0, 1] == true))
                {
                    if ((locationUsedByO[0, 2] == false) && (locationUsedByX[0, 2] == false) && (isLocationUsed[0, 2] == false))
                    {
                        oRect[42].X = Convert.ToInt32(boxLocations[0, 2].X);
                        oRect[42].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        locationUsedByO[0, 2] = true;
                        isLocationUsed[0, 2] = true;
                        turnpiece = TurnPiece.xturn;

                        if (locationUsedByO[0, 2] && isLocationUsed[0, 2])
                        {
                            terminateLoop = true;
                        }

                        else
                        {
                            terminateLoop = false;
                        }
                    }

                }

                if (TurnPiece.oturn == turnpiece)
                {
                    //row 1x2, row1x3
                    if ((locationUsedByX[0, 1] == true) && (locationUsedByX[0, 2] == true))
                    {
                        if ((locationUsedByO[0, 0] == false) && (locationUsedByX[0, 0] == false) && (isLocationUsed[0, 0] == false))
                        {
                            oRect[43].X = Convert.ToInt32(boxLocations[0, 0].X);
                            oRect[43].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                            locationUsedByO[0, 0] = true;
                            isLocationUsed[0, 0] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[0, 0] && isLocationUsed[0, 0])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                // row1x1, row1x3
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 0] == true) && (locationUsedByX[0, 2] == true))
                    {
                        if ((locationUsedByO[0, 1] == false) && (locationUsedByX[0, 1] == false) && (isLocationUsed[0, 1] == false))
                        {
                            oRect[44].X = Convert.ToInt32(boxLocations[0, 1].X);
                            oRect[44].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                            locationUsedByO[0, 1] = true;
                            isLocationUsed[0, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[0, 1] && isLocationUsed[0, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                /////////////////////////////////////////
                // row 2x1, row 2x2
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[1, 0] == true) && (locationUsedByX[1, 1] == true))
                    {
                        if ((locationUsedByO[1, 2] == false) && (locationUsedByX[1, 2] == false) && (isLocationUsed[1, 2] == false))
                        {
                            oRect[45].X = Convert.ToInt32(boxLocations[1, 2].X);
                            oRect[45].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                            locationUsedByO[1, 2] = true;
                            isLocationUsed[1, 2] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 2] && isLocationUsed[1, 2])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                // row 2x2, row 2x3
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[1, 1] == true) && (locationUsedByX[1, 2] == true))
                    {
                        if ((locationUsedByO[1, 0] == false) && (locationUsedByX[1, 0] == false) && (isLocationUsed[1, 0] == false))
                        {
                            oRect[46].X = Convert.ToInt32(boxLocations[1, 0].X);
                            oRect[46].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                            locationUsedByO[1, 0] = true;
                            isLocationUsed[1, 0] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 0] && isLocationUsed[1, 0])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                // row 2x1, row 2x3

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[1, 0] == true) && (locationUsedByX[1, 2] == true))
                    {
                        if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                        {
                            oRect[47].X = Convert.ToInt32(boxLocations[1, 1].X);
                            oRect[47].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                            locationUsedByO[1, 1] = true;
                            isLocationUsed[1, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                ///////////////////////////////////////
                //row3x1, row3x2

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[2, 0] == true) && (locationUsedByX[2, 1] == true))
                    {
                        if ((locationUsedByO[2, 2] == false) && (locationUsedByX[2, 2] == false) && (isLocationUsed[2, 2] == false))
                        {
                            oRect[48].X = Convert.ToInt32(boxLocations[2, 2].X);
                            oRect[48].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                            locationUsedByO[2, 2] = true;
                            isLocationUsed[2, 2] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[2, 2] && isLocationUsed[2, 2])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                //row3x2, row3x3
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[2, 1] == true) && (locationUsedByX[2, 2] == true))
                    {
                        if ((locationUsedByO[2, 0] == false) && (locationUsedByX[2, 0] == false) && (isLocationUsed[2, 0] == false))
                        {
                            oRect[49].X = Convert.ToInt32(boxLocations[2, 0].X);
                            oRect[49].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                            locationUsedByO[2, 0] = true;
                            isLocationUsed[2, 0] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[2, 0] && isLocationUsed[2, 0])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                //row3x1, row3x3

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[2, 0] == true) && (locationUsedByX[2, 2] == true))
                    {
                        if ((locationUsedByO[2, 1] == false) && (locationUsedByX[2, 1] == false) && (isLocationUsed[2, 1] == false))
                        {
                            oRect[50].X = Convert.ToInt32(boxLocations[2, 1].X);
                            oRect[50].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                            locationUsedByO[2, 1] = true;
                            isLocationUsed[2, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[2, 1] && isLocationUsed[2, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                /////////////////////////////////////////////////////////

                // COLUMNS // 

                ////////////////////////////////////////////////////////
                // left most column 
                // row1x1, row2x1

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 0] == true) && (locationUsedByX[1, 0] == true))
                    {
                        if ((locationUsedByO[2, 0] == false) && (locationUsedByX[2, 0] == false) && (isLocationUsed[2, 0] == false))
                        {
                            oRect[51].X = Convert.ToInt32(boxLocations[2, 0].X);
                            oRect[51].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                            locationUsedByO[2, 0] = true;
                            isLocationUsed[2, 0] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[2, 0] && isLocationUsed[2, 0])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                // row2x1, row3x1

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[1, 0] == true) && (locationUsedByX[2, 0] == true))
                    {
                        if ((locationUsedByO[0, 0] == false) && (locationUsedByX[0, 0] == false) && (isLocationUsed[0, 0] == false))
                        {
                            oRect[52].X = Convert.ToInt32(boxLocations[0, 0].X);
                            oRect[52].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                            locationUsedByO[0, 0] = true;
                            isLocationUsed[0, 0] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[0, 0] && isLocationUsed[0, 0])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                // row1x1, row3x1

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 0] == true) && (locationUsedByX[2, 0] == true))
                    {
                        if ((locationUsedByO[1, 0] == false) && (locationUsedByX[1, 0] == false) && (isLocationUsed[1, 0] == false))
                        {
                            oRect[53].X = Convert.ToInt32(boxLocations[1, 0].X);
                            oRect[53].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                            locationUsedByO[1, 0] = true;
                            isLocationUsed[1, 0] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 0] && isLocationUsed[1, 0])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                ////////////////////
                // middle column //
                //////////////////
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 1] == true) && (locationUsedByX[1, 1] == true))
                    {
                        if ((locationUsedByO[2, 1] == false) && (locationUsedByX[2, 1] == false) && (isLocationUsed[2, 1] == false))
                        {
                            oRect[54].X = Convert.ToInt32(boxLocations[2, 1].X);
                            oRect[54].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                            locationUsedByO[2, 1] = true;
                            isLocationUsed[2, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[2, 1] && isLocationUsed[2, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[1, 1] == true) && (locationUsedByX[2, 1] == true))
                    {
                        if ((locationUsedByO[0, 1] == false) && (locationUsedByX[0, 1] == false) && (isLocationUsed[0, 1] == false))
                        {
                            oRect[55].X = Convert.ToInt32(boxLocations[0, 1].X);
                            oRect[55].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                            locationUsedByO[0, 1] = true;
                            isLocationUsed[0, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[0, 1] && isLocationUsed[0, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }

                        }
                    }
                }

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 1] == true) && (locationUsedByX[2, 1] == true))
                    {
                        if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                        {
                            oRect[56].X = Convert.ToInt32(boxLocations[1, 1].X);
                            oRect[56].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                            locationUsedByO[1, 1] = true;
                            isLocationUsed[1, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                ///////////////////////
                // right most column
                ///////////////////////

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 2] == true) && (locationUsedByX[1, 2] == true))
                    {
                        if ((locationUsedByO[2, 2] == false) && (locationUsedByX[2, 2] == false) && (isLocationUsed[2, 2] == false))
                        {
                            oRect[57].X = Convert.ToInt32(boxLocations[2, 2].X);
                            oRect[57].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                            locationUsedByO[2, 2] = true;
                            isLocationUsed[2, 2] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[2, 2] && isLocationUsed[2, 2])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[1, 2] == true) && (locationUsedByX[2, 2] == true))
                    {
                        if ((locationUsedByO[0, 2] == false) && (locationUsedByX[0, 2] == false) && (isLocationUsed[0, 2] == false))
                        {
                            oRect[58].X = Convert.ToInt32(boxLocations[0, 2].X);
                            oRect[58].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                            locationUsedByO[0, 2] = true;
                            isLocationUsed[0, 2] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[0, 2] && isLocationUsed[0, 2])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 2] == true) && (locationUsedByX[2, 2] == true))
                    {
                        if ((locationUsedByO[1, 2] == false) && (locationUsedByX[1, 2] == false) && (isLocationUsed[1, 2] == false))
                        {
                            oRect[59].X = Convert.ToInt32(boxLocations[1, 2].X);
                            oRect[59].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                            locationUsedByO[1, 2] = true;
                            isLocationUsed[1, 2] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 2] && isLocationUsed[1, 2])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }

                ///////////////////////////
                //diagnol from left to right 
                //////////////////////////////
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 0] == true) && (locationUsedByX[1, 1] == true))
                    {
                        if ((locationUsedByO[2, 2] == false) && (locationUsedByX[2, 2] == false) && (isLocationUsed[2, 2] == false))
                        {
                            oRect[60].X = Convert.ToInt32(boxLocations[2, 2].X);
                            oRect[60].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                            locationUsedByO[2, 2] = true;
                            isLocationUsed[2, 2] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[2, 2] && isLocationUsed[2, 2])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[1, 1] == true) && (locationUsedByX[2, 2] == true))
                    {
                        if ((locationUsedByO[0, 0] == false) && (locationUsedByX[0, 0] == false) && (isLocationUsed[0, 0] == false))
                        {
                            oRect[61].X = Convert.ToInt32(boxLocations[0, 0].X);
                            oRect[61].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                            locationUsedByO[0, 0] = true;
                            isLocationUsed[0, 0] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[0, 0] && isLocationUsed[0, 0])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }

                    }
                }

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 0] == true) && (locationUsedByX[2, 2] == true))
                    {
                        if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                        {
                            oRect[62].X = Convert.ToInt32(boxLocations[1, 1].X);
                            oRect[62].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                            locationUsedByO[1, 1] = true;
                            isLocationUsed[1, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }
                //////////////////////////////
                // diagnal from right to left
                //////////////////////////////
                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 2] == true) && (locationUsedByX[1, 1] == true))
                    {
                        if ((locationUsedByO[2, 0] == false) && (locationUsedByX[2, 0] == false) && (isLocationUsed[2, 0] == false))
                        {
                            oRect[63].X = Convert.ToInt32(boxLocations[2, 0].X);
                            oRect[63].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                            locationUsedByO[2, 0] = true;
                            isLocationUsed[2, 0] = true;
                            turnpiece = TurnPiece.xturn;
                        }
                    }
                    else if ((locationUsedByX[1, 1] == true) && (locationUsedByX[2, 0] == true))
                    {
                        if ((locationUsedByO[0, 2] == false) && (locationUsedByX[0, 2] == false) && (isLocationUsed[0, 2] == false))
                        {
                            oRect[64].X = Convert.ToInt32(boxLocations[0, 2].X);
                            oRect[64].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                            locationUsedByO[0, 2] = true;
                            isLocationUsed[0, 2] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[0, 2] && isLocationUsed[0, 2])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }
                }

                if (TurnPiece.oturn == turnpiece)
                {
                    if ((locationUsedByX[0, 2] == true) && (locationUsedByX[2, 0] == true))
                    {
                        if ((locationUsedByO[1, 1] == false) && (locationUsedByX[1, 1] == false) && (isLocationUsed[1, 1] == false))
                        {
                            oRect[65].X = Convert.ToInt32(boxLocations[1, 1].X);
                            oRect[65].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                            locationUsedByO[1, 1] = true;
                            isLocationUsed[1, 1] = true;
                            turnpiece = TurnPiece.xturn;

                            if (locationUsedByO[1, 1] && isLocationUsed[1, 1])
                            {
                                terminateLoop = true;
                            }

                            else
                            {
                                terminateLoop = false;
                            }
                        }
                    }

                }
            }

            #endregion

        }

        public void SetToOturn()
        {
            turnpiece = TurnPiece.oturn;
        }

        public void O_PieceMoves()
        {
            //the deep principle of probability mathematics comes alive. the great one equation. checking either side of the X choice
            // placing a piece based on each possible outcome as such 9+ algorithims. an alogorithm for each piece placement plus a few more 
            // for good measure

            #region Moves to Make In Relationship To Opponent (Priority 2)
            if (TurnPiece.oturn == turnpiece)
            {
                if (locationUsedByX[0, 0] == true)
                {
                    if (isLocationUsed[0, 1] == false)
                    {
                        oRect[1].X = Convert.ToInt32(boxLocations[0, 1].X);
                        oRect[1].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        locationUsedByO[0, 1] = true;
                        isLocationUsed[0, 1] = true;
                        turnpiece = TurnPiece.xturn;

                    }

                    else if (isLocationUsed[1, 0] == false)
                    {
                        oRect[2].X = Convert.ToInt32(boxLocations[1, 0].X);
                        oRect[2].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;

                    }
                    else if (isLocationUsed[1, 1] == false)
                    {
                        oRect[67].X = Convert.ToInt32(boxLocations[1, 1].X);
                        oRect[67].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                        locationUsedByO[1, 1] = true;
                        isLocationUsed[1, 1] = true;
                        turnpiece = TurnPiece.xturn;

                    }



                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if (locationUsedByX[0, 1] == true)
                {
                    if (isLocationUsed[0, 0] == false)
                    {
                        oRect[4].X = Convert.ToInt32(boxLocations[0, 0].X);
                        oRect[4].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                        locationUsedByO[0, 0] = true;
                        isLocationUsed[0, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[0, 2] == false)
                    {
                        oRect[5].X = Convert.ToInt32(boxLocations[0, 2].X);
                        oRect[5].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        locationUsedByO[0, 2] = true;
                        isLocationUsed[0, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 0] == false)
                    {
                        oRect[68].X = Convert.ToInt32(boxLocations[1, 0].X);
                        oRect[68].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 2] == false)
                    {
                        oRect[69].X = Convert.ToInt32(boxLocations[1, 2].X);
                        oRect[69].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        locationUsedByO[1, 2] = true;
                        isLocationUsed[1, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                }

            }
            if (TurnPiece.oturn == turnpiece)
            {
                if (locationUsedByX[0, 2] == true)
                {
                    if (isLocationUsed[0, 1] == false)
                    {
                        oRect[6].X = Convert.ToInt32(boxLocations[0, 1].X);
                        oRect[6].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        locationUsedByO[0, 1] = true;
                        isLocationUsed[0, 1] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 2] == false)
                    {
                        oRect[7].X = Convert.ToInt32(boxLocations[1, 2].X);
                        oRect[7].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        locationUsedByO[1, 2] = true;
                        isLocationUsed[1, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }


                }

            }

            if (TurnPiece.oturn == turnpiece)
            {
                if (locationUsedByX[1, 0] == true)
                {
                    if (isLocationUsed[0, 0] == false)
                    {
                        oRect[8].X = Convert.ToInt32(boxLocations[0, 0].X);
                        oRect[8].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                        locationUsedByO[0, 0] = true;
                        isLocationUsed[0, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[2, 0] == false)
                    {
                        oRect[9].X = Convert.ToInt32(boxLocations[2, 0].X);
                        oRect[9].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                        locationUsedByO[2, 0] = true;
                        isLocationUsed[2, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[0, 1] == false)
                    {
                        oRect[70].X = Convert.ToInt32(boxLocations[0, 1].X);
                        oRect[70].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        locationUsedByO[0, 1] = true;
                        isLocationUsed[0, 1] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[2, 1] == false)
                    {
                        oRect[71].X = Convert.ToInt32(boxLocations[2, 1].X);
                        oRect[71].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                        locationUsedByO[2, 1] = true;
                        isLocationUsed[2, 1] = true;
                        turnpiece = TurnPiece.xturn;
                    }




                }
            }


            if (TurnPiece.oturn == turnpiece)
            {
                if (locationUsedByX[1, 2] == true)
                {
                    if (isLocationUsed[0, 2] == false)
                    {
                        oRect[10].X = Convert.ToInt32(boxLocations[0, 2].X);
                        oRect[10].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        locationUsedByO[0, 2] = true;
                        isLocationUsed[0, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[2, 2] == false)
                    {
                        oRect[11].X = Convert.ToInt32(boxLocations[2, 2].X);
                        oRect[11].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                        locationUsedByO[2, 2] = true;
                        isLocationUsed[2, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[0, 1] == false)
                    {
                        oRect[72].X = Convert.ToInt32(boxLocations[0, 1].X);
                        oRect[72].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        locationUsedByO[0, 1] = true;
                        isLocationUsed[0, 1] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[2, 1] == false)
                    {
                        oRect[73].X = Convert.ToInt32(boxLocations[2, 1].X);
                        oRect[73].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                        locationUsedByO[2, 1] = true;
                        isLocationUsed[2, 1] = true;
                        turnpiece = TurnPiece.xturn;
                    }



                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if (locationUsedByX[2, 0] == true)
                {
                    if (isLocationUsed[1, 0] == false)
                    {
                        oRect[12].X = Convert.ToInt32(boxLocations[1, 0].X);
                        oRect[12].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;

                    }

                    else if (isLocationUsed[2, 1] == false)
                    {
                        oRect[13].X = Convert.ToInt32(boxLocations[2, 1].X);
                        oRect[13].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                        locationUsedByO[2, 1] = true;
                        isLocationUsed[2, 1] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                }

                else if (locationUsedByX[2, 1] == true)
                {
                    if (isLocationUsed[2, 0] == false)
                    {
                        oRect[14].X = Convert.ToInt32(boxLocations[2, 0].X);
                        oRect[14].Y = Convert.ToInt32(boxLocations[2, 0].Y);
                        locationUsedByO[2, 0] = true;
                        isLocationUsed[2, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[2, 2] == false)
                    {
                        oRect[15].X = Convert.ToInt32(boxLocations[2, 2].X);
                        oRect[15].Y = Convert.ToInt32(boxLocations[2, 2].Y);
                        locationUsedByO[2, 2] = true;
                        isLocationUsed[2, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 0] == false)
                    {
                        oRect[74].X = Convert.ToInt32(boxLocations[1, 0].X);
                        oRect[74].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 2] == false)
                    {
                        oRect[75].X = Convert.ToInt32(boxLocations[1, 2].X);
                        oRect[75].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        locationUsedByO[1, 2] = true;
                        isLocationUsed[1, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }
                }
            }

            if (TurnPiece.oturn == turnpiece)
            {
                if (locationUsedByX[2, 2] == true)
                {
                    if (isLocationUsed[2, 1] == false)
                    {
                        oRect[16].X = Convert.ToInt32(boxLocations[2, 1].X);
                        oRect[16].Y = Convert.ToInt32(boxLocations[2, 1].Y);
                        locationUsedByO[2, 1] = true;
                        isLocationUsed[2, 1] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 2] == false)
                    {
                        oRect[17].X = Convert.ToInt32(boxLocations[1, 2].X);
                        oRect[17].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        locationUsedByO[1, 2] = true;
                        isLocationUsed[1, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }


                }
            }
            #endregion


        }





        public void TicTacToeMoves()
        {
            // o piece placement algorithim
            if (TurnPiece.oturn == turnpiece)
            {

                // gameboardlocation = GameBoardLocation.row1x1;

                oRect[0].X = Convert.ToInt32(boxLocations[1, 1].X);
                oRect[0].Y = Convert.ToInt32(boxLocations[1, 1].Y);



                if ((oRect[0].X == Convert.ToInt32(boxLocations[1, 1].X) && (oRect[0].Y == Convert.ToInt32(boxLocations[1, 1].Y))))
                {
                    // for probability math and piece placement detection and calculations
                    // represents row 1x1
                    isLocationUsed[1, 1] = true;
                    // represents row1x1 
                    locationUsedByO[1, 1] = true;


                    // switch turn back into x
                    turnpiece = TurnPiece.xturn;
                    isFirstMove = true;
                }
            }
        }




    }
}
