using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler_FromFile : BtnHandler {
    public override void HandlePush() {
        string levelName = FindObjectOfType<UserLevelPanel>().SelectedLevel();
        SessionController.Instance.SetLevel(LevelSaveLoad.LoadUserLevel(levelName));
    }

    public void DeleteLevel() {
        UserLevelPanel ulp = FindObjectOfType<UserLevelPanel>();
        string level = ulp.SelectedLevel();
        LevelSaveLoad.DeleteUserLevel(level);
        ulp.nextIndex();
    }

    public void TriggerDeleteLevelWithConfirmation() {
        StartCoroutine(DeleteLevelWithConfirmation());

    }

    public IEnumerator DeleteLevelWithConfirmation() {
        UserLevelPanel ulp = FindObjectOfType<UserLevelPanel>();
        string level = ulp.SelectedLevel();

        Dialog_Confirmation dialog = FindObjectOfType<Dialog_Confirmation>();
        dialog.Show("Are you sure you want to delete " + level + "?");

        while (dialog.result == Dialog_Confirmation.Result.None) {
            yield return null; // wait
        }

        if (dialog.result == Dialog_Confirmation.Result.Yes) {
            LevelSaveLoad.DeleteUserLevel(level);
            ulp.nextIndex();
            Debug.Log("result was yes");
        }
        else if (dialog.result == Dialog_Confirmation.Result.No) {
            // do nothing
            Debug.Log("result was No");
        }
    }
}
