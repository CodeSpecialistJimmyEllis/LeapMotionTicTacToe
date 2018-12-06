// All Code, Art, Music by Jimmy Ellis
// As a candidate for 8th Light Computer Science C# Division 
// this program has been written with deep care as proof
// and validation of apprentice/craftsman/resident level skill
// in the final science of life: code.

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
    class TicTacToeTitleScreen : GameScreen
    {
        #region Fields
        // background texture
        Texture2D background;

        Song backgroundMusic;
        // buttons 
        Texture2D buttonContinue;
        bool buttonIsHit = false;
        Rectangle buttonContinueRect;
        Texture2D buttonContinueHit;
        float volumeLevel;

        // enumeration states that declare if the button is hit or not
        enum tictactoeButtonState
        {
            free, hit
        }
        tictactoeButtonState continuebutton;


        // the mouse is used to play this game as such mouse state creations
        MouseState tictactoeMouse;
        Rectangle mouseCollision;
        #endregion
        public TicTacToeTitleScreen()
        {

            

            volumeLevel = 1.0f;
            tictactoeMouse = new MouseState();
            mouseCollision = new Rectangle(0, 0, 24, 24);
            buttonContinueRect = new Rectangle(750, 500, 256, 128);

            continuebutton = tictactoeButtonState.free;
        }

        public override void LoadContent(ContentManager Content)
        {
         
            background = Content.Load<Texture2D>("Backgrounds/titlescreen");
            buttonContinue = Content.Load<Texture2D>("Buttons/continue");
            buttonContinueHit = Content.Load<Texture2D>("Buttons/continuehit");
            backgroundMusic = Content.Load<Song>("Sound/TicTacToe");
            MediaPlayer.Volume = volumeLevel;
            MediaPlayer.Play(backgroundMusic);
            base.LoadContent(Content);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            // mouse state is given to mouse
            tictactoeMouse = Mouse.GetState();

            // mouse collision algorithim
            mouseCollision.X = tictactoeMouse.X;
            mouseCollision.Y = tictactoeMouse.Y;

            // collision for mouse to change state
            if (buttonContinueRect.Intersects(mouseCollision))
            {
                buttonIsHit = true;
                continuebutton = tictactoeButtonState.hit;
                if (ButtonState.Pressed == tictactoeMouse.LeftButton)
                {
                    ScreenManager.Instance.AddScreen(new TicTacToeGameScreen());

                }
            }

            if ( (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) ))
            {
                ScreenManager.Instance.AddScreen(new TicTacToeGameScreen());
            }

            if (!buttonContinueRect.Intersects(mouseCollision))
            {
                buttonIsHit = true;
                continuebutton = tictactoeButtonState.free;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(background, new Rectangle(0, 0, 1024, 720), Color.White);

            if (tictactoeButtonState.free == continuebutton)
            {
                spriteBatch.Draw(buttonContinue, buttonContinueRect, Color.White);
            }

            else if (tictactoeButtonState.hit == continuebutton)
            {
                spriteBatch.Draw(buttonContinueHit, buttonContinueRect, Color.White);
            }

        }
    }
}
