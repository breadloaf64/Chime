using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject {
    public string name;
    public Vector2 launcherPosition;
    public string genString;
    public string levelText;
    public int themeIndex;
    public int targetBalls;

    public static char[] validBlockCodes = { '1', '2', '3', 'U', '0', 'T', 't', 'G' }; //Mind this! THESE ARE HARDCODED, SO EDIT IT WHEN YOU CREATE A NEW BLOCK TYPE

    public static LevelObject DefaultLevel() {
        return new LevelObject {
            name = "defaultLevel",
            launcherPosition = new Vector2(8f, 0.5f),
            genString = EmptyGenString(),
            levelText = "",
            themeIndex = 0,
            targetBalls = 0
        };
    }

    private static string EmptyGenString() {
        int w = 16, h = 12;
        string emptyGenString = "";
        for (int i = 0; i < w * h; i++) {
            emptyGenString += "0";
        }
        return emptyGenString;
    }

    public static LevelObject RandomLevel() {
        Vector2 _launcherPosition = new Vector2(8f, 0.5f);
        string _genString = RandomGenString();

        return new LevelObject {
            name = "random",
            launcherPosition = _launcherPosition,
            genString = _genString,
            levelText = "",
            themeIndex = 0
        };
    }

    public LevelObject ShallowCopy() {
        return (LevelObject)this.MemberwiseClone();
    }

    public LevelObject DeepCopy() {
        return new LevelObject {
            name = string.Copy(this.name),
            launcherPosition = new Vector2(this.launcherPosition.x, this.launcherPosition.y),
            genString = string.Copy(this.genString),
            levelText = this.levelText,
            themeIndex = this.themeIndex,
            targetBalls = this.targetBalls
        };
    }

    public static string RandomGenString() {
        int w = 16;
        int h = 12;
        string genString = "";

        for (int i = 0; i < w * h; i++) {
            int randomIndex = Random.Range(0, validBlockCodes.Length);
            genString += validBlockCodes[randomIndex];
        }
        return genString;
    }
}
