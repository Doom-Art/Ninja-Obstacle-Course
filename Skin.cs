using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Skin
    {
        Texture2D _skinTex, _iconTex;
        bool _locked;
        Rectangle _iconLocation;
        int _unlockLevel, _price;

        public Skin(Texture2D skinTex)
        {
            _skinTex = skinTex;
            _locked = false;
        }
        public Skin(Texture2D skinTex, bool locked)
        {
            _skinTex = skinTex;
            _locked = locked;
        }
        public Skin(Texture2D skinTex, bool locked, int price)
        {
            _skinTex = skinTex;
            _locked = locked;
            _price = price;
        }
        public int Price { get { return _price; } }
        public Skin(Texture2D skinTex, Texture2D iconTex, Rectangle iconLocation, int unlockLevel)
        {
            _skinTex = skinTex;
            _locked = true;
            _iconTex = iconTex;
            _iconLocation = iconLocation;
            _unlockLevel = unlockLevel;
        }
        public void DrawIcon(SpriteBatch sprite)
        {
            sprite.Draw(_iconTex, _iconLocation, Color.White);
        }
        public int UnlockLevel
        {
            get { return _unlockLevel; }
        }
        public void UnlockSkin()
        {
            _locked = false;
        }
        public void LockSkin()
        {
            _locked = true;
        }
        public Rectangle IconLocation { get { return _iconLocation; } }
        public Texture2D SkinTex { get { return _skinTex; } }
        public bool Locked
        {
            get { return _locked; }
        }
    }
}
