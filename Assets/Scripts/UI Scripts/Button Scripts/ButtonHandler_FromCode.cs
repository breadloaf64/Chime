using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler_FromCode : MonoBehaviour {

    [SerializeField] InputField input;
    
    public void SetLevelAndLoadScene(string sceneToLoad) {
        string levelJSON = input.text;
        try {
            LevelObject level = JsonUtility.FromJson<LevelObject>(levelJSON);
            SessionController.Instance.SetLevel(level);
            SceneLoader sl = FindObjectOfType<SceneLoader>();
            sl.LoadScene(sceneToLoad); 
        }
        catch {
            ShowPopupText("Invalid Level Code");
            Debug.Log("Invalid Level Code");
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
