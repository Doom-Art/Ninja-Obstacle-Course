using System;

namespace Ninja_Obstacle_Course
{
    public class Score
    {
        private readonly int _coins, _difficulty;
        private readonly float _seconds;
        private string _name;
        public Score(string name, float seconds, int coins, int difficulty)
        {
            _name = name;
            _seconds = seconds;
            _coins = coins;
            _difficulty = difficulty;
        }
        public string Name { set { _name =  value; } get { return _name; } }
        public static Score LoadSave(string savedScore)
        {
            string name = "", temp = "";
            int coins = 0, counter = 0, difficulty = 0;
            float seconds =0;
            for (int i = 0; i < savedScore.Length; i++)
            {
                if (savedScore[i] != ',')
                {
                    if (counter == 0)
                        name += savedScore[i];
                    else
                        temp += savedScore[i];
                }
                else
                {
                    if (counter == 1)
                    {
                        _ = float.TryParse(temp, out seconds);
                    }
                    else if (counter == 2)
                    {
                        _ = Int32.TryParse(temp, out coins);
                    }
                    else if (counter == 3)
                    {
                        _ = Int32.TryParse(temp, out difficulty);
                    }
                    counter++;
                    temp = "";
                }
            }
            return new Score(name, seconds, coins, difficulty);
        }
        public static Score Default(int difficulty)
        {
            return new Score("NA", 0, 0, difficulty);
        }
        public bool HighScore (float seconds, int coins)
        {
            if (_seconds == 0)
                return true;
            else if (Math.Round(_seconds, 2) == Math.Round(seconds, 2))
                return _coins < coins;
            else
            {
                return seconds < _seconds;
            }
        }
        public string Save()
        {
            return _name + "," + _seconds + "," + _coins + "," + _difficulty + ",";
        }
        public override string ToString()
        {
            string difficulty;
            if (_difficulty == 1)
                difficulty = "Easy";
            else if (_difficulty == 2)
                difficulty = "Normal";
            else
                difficulty = "Hard";
            return $"{_name}:_{_seconds:0.00}_seconds,_{_coins}_coins,_on_{difficulty}";
        }

    }
}
