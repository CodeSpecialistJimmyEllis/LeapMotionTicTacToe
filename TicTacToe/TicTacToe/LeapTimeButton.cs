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

namespace TicTacToe
{
    class LeapTimeButton
    {

        enum LeapBoxHit
        {
            row1x1,row1x2, row1x3, row2x1, row2x2, row2x3, row3x1, row3x2, row3x3, none
        }

        LeapBoxHit leapBoxHit;
        // leap
        LeapComponet leap;
        public Rectangle leapRectangle;

        public Rectangle LeapRectangle { get { return leapRectangle; } }
        Texture2D finger;

        Vector2[,] boxLocations;
        int baseSeconds = 0;
        bool[] universalButtonHit;
        public bool[] UniversalButtonHit { get { return universalButtonHit; } set { universalButtonHit = value; } }
        bool isTimeSet = false;
        Rectangle[] boxCollisionRect;
        GameTime changingTime;
        Texture2D universalLeapButton;
        Rectangle universalLeapButtonRect;
        SpriteFont spriteFont;
        Color universalLeapButtonColor;


        public LeapTimeButton()
        {
            universalLeapButtonRect = new Rectangle(0, 0, 256, 256);
            universalLeapButtonColor = Color.White;
            leapRectangle = new Rectangle(800, 0, 64, 64);
            changingTime = new GameTime();

            boxLocations = new Vector2[3, 3];
            leapBoxHit = LeapBoxHit.none;
            boxLocations[0, 0] = new Vector2(150, 100);
            boxLocations[0, 1] = new Vector2(370, 90);
            boxLocations[0, 2] = new Vector2(570, 95);

            boxLocations[1, 0] = new Vector2(140, 290);
            boxLocations[1, 1] = new Vector2(365, 300);
            boxLocations[1, 2] = new Vector2(575, 290);

            boxLocations[2, 0] = new Vector2(155, 520);
            boxLocations[2, 1] = new Vector2(365, 510);
            boxLocations[2, 2] = new Vector2(570, 520);

            universalButtonHit = new bool[9];
          
            universalButtonHit[0] = false;
            universalButtonHit[1] = false;
            universalButtonHit[2] = false;

            universalButtonHit[3] = false;
            universalButtonHit[4] = false;
            universalButtonHit[5] = false;

            universalButtonHit[6] = false;
            universalButtonHit[7] = false;
            universalButtonHit[8] = false;
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
        }

        public void LoadContent(ContentManager Content)
        {
            universalLeapButton = Content.Load<Texture2D>("Pieces/X");
            //spriteFont = Content.Load<SpriteFont>("SpriteFont1");

            finger = Content.Load<Texture2D>("Pieces/x");

        }

        public void Update(GameTime gameTime)
        {


            #region change button color logic row1x1
            if (leapRectangle.Intersects(boxCollisionRect[0]))
            {
                leapBoxHit = LeapBoxHit.row1x1;
                if (isTimeSet == false)
                {
                    isTimeSet = true;
                    baseSeconds = gameTime.TotalGameTime.Seconds;
                }

                if (universalButtonHit[0] == false)
                {
                    if ((gameTime.TotalGameTime.Seconds) == (baseSeconds + 1))
                    {
                        universalButtonHit[0] = true;
                        
                    }
                   
                   
                }
            }
            else
            {
               
                
                isTimeSet = false;
               
              //  leapBoxHit = LeapBoxHit.none;

            }
            #endregion



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // debug mode options
             
          //  spriteBatch.DrawString(spriteFont, "total game time: " + gameTime.TotalGameTime.Seconds.ToString(), new Vector2(300, 300), Color.Black);
          //  spriteBatch.DrawString(spriteFont, "added game time: " + changingTime.TotalGameTime.Seconds.ToString(), new Vector2(300, 400), Color.Black);
        //    spriteBatch.DrawString(spriteFont, "base secods " +  baseSeconds.ToString(), new Vector2(350, 440), Color.Black);
         //   spriteBatch.DrawString(spriteFont, "universal button hit " + universalButtonHit.ToString(), new Vector2(400, 450), Color.Black);
         //   spriteBatch.DrawString(spriteFont, "is time set: " + isTimeSet.ToString(), new Vector2(300, 200), Color.Black);
             
           // spriteBatch.DrawString(spriteFont, "leap rectangle: " + Game1.leapRectangle.ToString(), new Vector2(300, 200), Color.Black);
            /*
            if (LeapBoxHit.row1x1 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[0], universalLeapButtonColor);
            }
            else
            {

            }
            if (LeapBoxHit.row1x2 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[1], universalLeapButtonColor);
            }
            else{ }

            if (LeapBoxHit.row1x3 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[2], universalLeapButtonColor);
            }
            else {}
            if (LeapBoxHit.row2x1 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[3], universalLeapButtonColor);
            }
            else {}
            if (LeapBoxHit.row2x2 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[4], universalLeapButtonColor);
            }
            else {}

            if (LeapBoxHit.row2x3 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[5], universalLeapButtonColor);
            }
            else {}
            if (LeapBoxHit.row3x1 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[6], universalLeapButtonColor);
            }
            else {}
            if (LeapBoxHit.row3x2 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[7], universalLeapButtonColor);
            }
            else {}
            if (LeapBoxHit.row3x3 == leapBoxHit)
            {
                spriteBatch.Draw(universalLeapButton, boxCollisionRect[8], universalLeapButtonColor);
            }
            else { }
             */
         

        }
    }
}
