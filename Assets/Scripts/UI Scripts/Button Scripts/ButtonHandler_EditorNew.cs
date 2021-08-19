using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler_EditorNew : MonoBehaviour {
    public void LoadEditorWithNewLevel() {
        SessionController.Instance.SetLevel(LevelObject.DefaultLevel()); ;
    }
}
