
namespace game
{
    
    using System.IO;
    using System.Linq.Expressions;
    using UnityEngine;
   

    public class StorageMangerBase
    {
        public  static bool SaveToFile(string filename, string json)
        {
            try
            {
                var fileStream = new FileStream(filename, FileMode.Create);
                using (var wirter = new StreamWriter(fileStream))
                {
                    wirter.Write(json);
                }
                return true;
            }catch ( System.Exception e )
            {
                Debug.Log("Error saving file: "+ e.Message);
                return false;
            }            
        }
        public static string LoadFromFile(string filename)
        {
            try
            {
                if (File.Exists(filename) )
                {
                    var filestream = new FileStream(filename, FileMode.Open);
                    using (var reader = new StreamReader(filestream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else 
                {
                    Debug.Log("File not found: " + filename);
                    return null; 
                }
            }catch ( System.Exception e )
            {
                Debug.Log("Error saving file: " + e.Message);
                return null;
            }
        }
    }

  
}