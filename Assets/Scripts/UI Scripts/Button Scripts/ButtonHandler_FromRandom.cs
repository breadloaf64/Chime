using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler_FromRandom : BtnHandler {
    public override void HandlePush() {
        SessionController.Instance.SetLevel(LevelObject.RandomLevel());
    }
}
