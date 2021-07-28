using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionController : MonoBehaviour
{
    LevelObject level = LevelObject.DefaultLevel();
    string sceneBefore; //name of previous scene (so that when you're in a level, you return to the scene by which you entered the level)


    private void Awake() {
        int SessionControllerCount = FindObjectsOfType<SessionController>().Length; //Implement singleton
        if (SessionControllerCount > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    public LevelObject GetLevel() {
        Debug.Log("get level: " + level.genString);
        return level;
    }

    public void SetLevel(LevelObject level) {
        this.level = level.DeepCopy();
        Debug.Log("set level: " + level.genString);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            LoadPreviousScene();
        }
        else if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name != "LevelEditor") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void LoadPreviousScene() {
        SceneLoader sl = FindObjectOfType<SceneLoader>();
        sl.LoadPreviousScene();
    }

    public void SetSceneBefore(string scene) {
        sceneBefore = string.Copy(scene);
    }

    public string GetSceneBefore() {
        return string.Copy(sceneBefore);
    }
}
