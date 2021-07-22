using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaveLoad : MonoBehaviour
{

    public static readonly string userLevelsFolder = Application.dataPath + "/Levels/UserLevels/";
    private const string extension = ".txt";

    public static void Save(LevelObject level, string levelName) {
        CheckDirectory(userLevelsFolder);
        string levelJSON = JsonUtility.ToJson(level);
        File.WriteAllText(userLevelsFolder + levelName + extension, levelJSON);
        Debug.Log("LevelSaveLoad: Level written to file :D");
    }

    private static void CheckDirectory(string directory) { //if directory doesn't exist, create one
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }
    }

    public static LevelObject LoadUserLevel(string levelName) {
        if (File.Exists(userLevelsFolder + levelName + extension)) {
            Debug.Log("LevelSaveLoad: Loaded level with name \"" + levelName + "\"");
            string levelJSON = File.ReadAllText(userLevelsFolder + levelName + extension);
            LevelObject level = JsonUtility.FromJson<LevelObject>(levelJSON);
            return level;
        }
        else {
            Debug.LogError("LevelSaveLoad: No level found with name \"" + levelName + "\"");
            return LevelObject.DefaultLevel();
        }
    }

    static public string LevelNameFromPath(string path) {
        string[] bits = path.Split('/');
        string endbit = bits[bits.Length - 1];
        return endbit.Split('.')[0];
    }

    static public void DeleteUserLevel(string levelName) {
        if (File.Exists(userLevelsFolder + levelName + extension)) {
            Debug.Log("LevelSaveLoad: Deleted level with name \"" + levelName + "\"");
            File.Delete(userLevelsFolder + levelName + extension);
        }
        else {
            Debug.LogError("LevelSaveLoad: No level found with name \"" + levelName + "\"");
        }
    }
}
