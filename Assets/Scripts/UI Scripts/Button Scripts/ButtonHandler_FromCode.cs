using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler_FromCode : BtnHandler {

    [SerializeField] InputField input;
    [SerializeField] string sceneToLoad = "level";
    
    public override void HandlePush() {
        string levelJSON = input.text;
        try {
            LevelObject level = JsonUtility.FromJson<LevelObject>(levelJSON);
            SessionController.Instance.SetLevel(level);
            SceneLoader sl = FindObjectOfType<SceneLoader>();
            sl.LoadScene(sceneToLoad); 
        }
        catch {
            //warning box
            Debug.Log("The code was not valid");
        }
    }
}
