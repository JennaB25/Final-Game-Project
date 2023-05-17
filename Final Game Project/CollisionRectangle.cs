using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game_Project
{
    internal class CollisionRectangle
    {
        private Texture2D _texture;
        private Rectangle _location;         
        int width;
        int height;

        public CollisionRectangle(Texture2D texture, int x, int y, int width, int height)
        {
            _texture = texture;
            _location = new Rectangle(x, y, width, height);
            this.width = width;
            this.height = height;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}
