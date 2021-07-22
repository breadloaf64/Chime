using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler_EditorNew : BtnHandler {
    public override void HandlePush() {
        GetSessionController().SetLevel(LevelObject.DefaultLevel()); ;
    }
}
