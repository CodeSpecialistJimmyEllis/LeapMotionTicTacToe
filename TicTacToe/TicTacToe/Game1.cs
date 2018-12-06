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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // TODO: 
        // send tic tac toe gamescreen variables to game1.cs
        // send tic tac toe ai variables tso game1.cs
        // transfer gamescreen variables to a.i.
        // transfer a.i. variables to gamescreen.
        // debug accordingly 


        // leap motion 

        public static LeapComponet leap;
        public static Rectangle leapRectangle;
        Texture2D finger;
        //

       public static Rectangle leapcollision;
        public Game1()
        {
            
            ScreenManager.Instance.Dimensions = new Vector2(1024, 720);
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            //ScreenManager.Instance.DimensionsX = 1024;

            //ScreenManager.Instance.DimensionsY = 720;
     
            Content.RootDirectory = "Content";
            leapcollision = new Rectangle(0, 0, 128, 128);
            // leap
            leap = new LeapComponet(this);
            this.Components.Add(leap);
        }
        protected override void Initialize()
        {
            
            ScreenManager.Instance.Initialize();
            leapRectangle = new Rectangle(800, 0, 128, 128);
          
           // graphics.ApplyChanges();
            IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            base.Initialize();
            
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // leap
            finger = Content.Load<Texture2D>("Pieces/x");
            //
            ScreenManager.Instance.LoadContent(Content);
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            ScreenManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
        
            ScreenManager.Instance.Draw(spriteBatch);
            foreach (Vector2 fingerLoc in Game1.leap.FingerPoints)
            {
              
                spriteBatch.Draw(finger, fingerLoc, Color.White);
            }

            spriteBatch.End();

            foreach (Vector2 fingerLocA in Game1.leap.FingerPoints)
            {
                leapcollision.X = (int)fingerLocA.X;
                leapcollision.Y = (int)fingerLocA.Y;
            }
            base.Draw(gameTime);
        }
    }
}
