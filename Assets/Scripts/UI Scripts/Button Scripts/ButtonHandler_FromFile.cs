﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler_FromFile : BtnHandler {
    public override void HandlePush() {
        string level = FindObjectOfType<UserLevelPanel>().SelectedLevel();
        GetSessionController().SetLevel(LevelSaveLoad.LoadUserLevel(level));
    }

    public void DeleteLevel() {
        UserLevelPanel ulp = FindObjectOfType<UserLevelPanel>();
        string level = ulp.SelectedLevel();
        LevelSaveLoad.DeleteUserLevel(level);
        ulp.Initialise();
    }
}