using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler_FromFile : MonoBehaviour {

    [SerializeField] UserLevelPanel levelPanel;

    public void SetLevelAndLoadScene(string sceneToLoad) {
        if (levelPanel.LevelIsSelected()) {
            string levelName = levelPanel.SelectedLevel();
            SessionController.Instance.SetLevel(LevelSaveLoad.LoadUserLevel(levelName));
            FindObjectOfType<SceneLoader>().LoadScene(sceneToLoad);
        }
        else {
            ShowPopupText("You have no saved levels");
        }
    }

    public void DeleteLevel() {
        string level = levelPanel.SelectedLevel();
        LevelSaveLoad.DeleteUserLevel(level);
        levelPanel.nextIndex();
    }

    public void TriggerDeleteLevelWithConfirmation() {
        if (FindObjectOfType<UserLevelPanel>().LevelIsSelected()) {
            StartCoroutine(DeleteLevelWithConfirmation());
        }
        else {
            ShowPopupText("You have no saved levels");
        }
    }

    public IEnumerator DeleteLevelWithConfirmation() {
        string level = levelPanel.SelectedLevel();

        Dialog_Confirmation dialog = FindObjectOfType<Dialog_Confirmation>();
        dialog.Show("Are you sure you want to \n delete " + level + "?");

        while (dialog.result == Dialog_Confirmation.Result.None) {
            yield return null; // wait
        }

        if (dialog.result == Dialog_Confirmation.Result.Yes) {
            LevelSaveLoad.DeleteUserLevel(level);
            levelPanel.RemoveSelectedLevel();
            ShowPopupText(level + " was deleted");
        }
        else if (dialog.result == Dialog_Confirmation.Result.No) {
            // do nothing
        }
    }

    private void ShowPopupText(string message) {
        PopupText pt = FindObjectOfType<PopupText>();
        if (pt != null) {
            pt.Show(message);
        }
        else {
            Debug.LogWarning("No popup text object.");
        }
    }
}
