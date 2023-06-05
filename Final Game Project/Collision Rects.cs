using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Game_Project
{
    internal class CollisionRect
    {
        private Texture2D _texture;
        private Rectangle _location;
        public static Vector2 _speed;
        private int width;
        private int height;

        public CollisionRect(Texture2D texture, int x, int y, int width, int height)
        {
            _texture = texture;
            _location = new Rectangle(x, y, width, height);
            _speed = new Vector2();
        }
        public float Width
        {
            get { return _location.Width; }
            set { _location.Width = (int)value; }
        }
        public float Height
        {
            get { return _location.Height; }
            set { _location.Height = (int)value; }
        }
        public float X
        {
            get { return _location.X; }
            set { _location.X = (int)value; }
        }
        public float Y
        {
            get { return _location.Y; }
            set { _location.Y = (int)value; }
        }
        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float VSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }
        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }

        public void Move()
        {
            _location.Offset(CollisionRect._speed);
        }

        public void UndoMove()
        {
            _location.X -= (int)CollisionRect._speed.X;
            _location.Y -= (int)CollisionRect._speed.Y;
        }
        //public void Update()
        //{
        //    Move();
        //}
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}

