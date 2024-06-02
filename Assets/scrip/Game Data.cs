using System;
using System.Collections.Generic;

namespace game
{
    //Lưu trữ thông tin game
    [Serializable]public class GameData
    {
         public int Id;
        // Start is called before the first frame update
        public int score = 0;
        public string timeplayed;      
    }

    [Serializable]public class GameDataPlayed
    {
        public List<GameData> plays;
        internal List<GameData> Plays;
    }
}