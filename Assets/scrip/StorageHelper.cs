
namespace game
{
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    public class StorageHelper
    {
        // Start is called before the first frame update
        public readonly string filename = "game_data.txt";
        public GameDataPlayed played; 
        public void LoadData()
        {
            played = new GameDataPlayed()
            {
                Plays = new List<GameData>()
            };
           string datAsJson = StorageMangerBase.LoadFromFile(filename);
           if(datAsJson != null)
            {
                played = JsonUtility.FromJson<GameDataPlayed>(datAsJson);
            }
        }
        public void SaveData()
        {
            string datAsJson = JsonUtility.ToJson(played);
            StorageMangerBase.SaveToFile(filename, datAsJson);
        }
}
}
