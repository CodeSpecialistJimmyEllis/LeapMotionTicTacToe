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
    class TicTacToeWinnerDeclared : GameScreen
    {

        Texture2D background;
        SpriteFont LargeFont;

        public TicTacToeWinnerDeclared()
        {
            
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            background = Content.Load<Texture2D>("Backgrounds/blankbackground");
            LargeFont = Content.Load<SpriteFont>("Content/LargeFont");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, new Rectangle(0, 0, 1024, 720), Color.White);

            spriteBatch.DrawString(LargeFont, "COMPUTER WINS", new Vector2(0, 360), Color.White);
            spriteBatch.DrawString(LargeFont, "PLAYER WINS", new Vector2(0, 360), Color.White);
            spriteBatch.DrawString(LargeFont, "CAT GAME", new Vector2(0, 360), Color.White);
        }
    }
}
