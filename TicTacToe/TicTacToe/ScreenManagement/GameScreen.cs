

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
    public class GameScreen
    {


     
        protected ContentManager content;
  

        public virtual void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
        }
        public virtual void UnloadContent()
        {
            content.Unload();
            

            GC.Collect();
            GC.Collect(0);
            GC.Collect(1);
            GC.Collect(2);
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
