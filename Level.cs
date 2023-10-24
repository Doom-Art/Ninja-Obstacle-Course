using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    internal class Level
    {
        private List<Portal> _portals;
        private List<Platform> _platforms;
        private List<int> _fadingPlatforms;
        public Level(List<Platform> platforms, List<int> fadingPlatforms, List<Portal> portals)
        {
            this._platforms = platforms;
            this._fadingPlatforms = fadingPlatforms;
            this._portals = portals;
        }
        public Level(List<Platform> platforms, List<Portal> portals)
        {
            this._platforms = platforms;
            this._portals = portals;
        }
        public Level(List<Platform> platforms, List<int> fadingPlatforms)
        {
            this._platforms = platforms;
            this._fadingPlatforms = fadingPlatforms;
        }
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Portal portal in _portals)
                portal.Draw(_spriteBatch);
            foreach (Platform p in _platforms)
            {
                p.Draw(_spriteBatch);
            }
        }
    }
}
