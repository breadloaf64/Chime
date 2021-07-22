using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler_FromRandom : BtnHandler {
    public override void HandlePush() {
        GetSessionController().SetLevel(LevelObject.RandomLevel());
    }
}
