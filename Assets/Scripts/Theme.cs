using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme
{
    public const int themeCount = 2;

    public Color backCol1;
    public Color backCol2;

    public static Theme getTheme(int index) {
        if (index == 1) {
            return new Theme {
                backCol1 = new Color32(255, 217, 161, 255),
                backCol2 = new Color32(0, 0, 0, 255)
            };
        }
        else { //index 0 - default
            return new Theme {
                backCol1 = new Color32(191, 232, 255, 255),
                backCol2 = new Color32(0, 0, 0, 255)
            };
        }
    }
}
