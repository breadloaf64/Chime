using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler_FromCode : BtnHandler {

    [SerializeField] InputField input;
    
    public override void HandlePush() {
        string levelJSON = input.text;
        try {
            LevelObject level = JsonUtility.FromJson<LevelObject>(levelJSON);
            SessionController.Instance.SetLevel(level);
            FindObjectOfType<SceneLoader>().LoadScene("Level");
        }
        catch {
            //warning box
            Debug.Log("The code was not valid");
        }
    }
}
