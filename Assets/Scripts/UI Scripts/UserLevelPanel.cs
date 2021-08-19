using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserLevelPanel : MonoBehaviour
{
    ArrayList levelList;
    int index;
    [SerializeField] TextMesh panelText;
    [SerializeField] AudioClip changeSound;

    private void Awake() {
        Initialise();
    }

    public void Initialise() {
        levelList = new ArrayList();
        DirectoryInfo info = new DirectoryInfo(LevelSaveLoad.userLevelsFolder);
        FileInfo[] fileInfo = info.GetFiles();
        if (fileInfo.Length > 0) {
            string levelName;
            foreach (FileInfo file in fileInfo) {
                if (file.Name.Split('.').Length == 2) {
                    levelName = file.Name.Split('.')[0];
                    levelList.Add(levelName);
                }
            }
            index = 0;
        }
        else {
            index = -1;
        }
    }

    private void Start() {
        UpdatePanel();
    }

    private void PlayChangeSound() {
        AudioSource.PlayClipAtPoint(changeSound, transform.position);
    }

    public void nextIndex() {
        if (index >= 0) {
            index++;
            constrainIndex();
        }
        UpdatePanel();
        PlayChangeSound();
    }

    public void previousIndex() {
        if (index >= 0) {
            index--;
            constrainIndex();
        }
        UpdatePanel();
        PlayChangeSound();
    }

    public void constrainIndex() {
        if (index >= levelList.Count) {
            index = 0;
        }
        else if (index < 0) {
            index = levelList.Count - 1;
        }
    }

    private void UpdatePanel() {
        if (index >= 0) {
            panelText.text = (string)levelList[index];
        }
        else {
            panelText.text = "No saved levels";
        }
    }

    public string SelectedLevel() {
        if (index >= 0) {
            return (string)levelList[index];
        }
        else {
            return "";
        }
    }

    public void RemoveSelectedLevel() {
        if (index >= 0) {
            levelList.Remove(SelectedLevel());
            constrainIndex();
            UpdatePanel();
        }
    }
}
