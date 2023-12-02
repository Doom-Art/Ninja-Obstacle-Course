using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class PenguinThrower
    {
        private Texture2D _penguinTex, _iceTex;
        private Vector2 _penguinLoc;
        private int _difficulty;
        private float _timer, _timeLimit;

        public PenguinThrower(Texture2D penguinTex, Texture2D iceTex, Vector2 penguinLoc)
        {
            this._penguinTex = penguinTex;
            this._iceTex = iceTex;
            this._penguinLoc = penguinLoc;
            
        }
        public void SetDifficulty(int difficulty)
        {
            _difficulty = difficulty;
            _timer = 0;
            _timeLimit = 130 - (_difficulty * 30);
            Hidden = false;
            
        }
        public void Update(List<Platform> platforms, Player player)
        {

        }
        public bool Hidden { get; set; }
    }
}
