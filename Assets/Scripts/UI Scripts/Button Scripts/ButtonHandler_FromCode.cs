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
            FindObjectOfType<PopupText>().Show("Invalid Level Code");
            Debug.Log("The code was not valid");
        }
    }
}
