using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionController : MonoBehaviour
{
    LevelObject level = LevelObject.DefaultLevel();
    string levelName = "default";
    string sceneBefore; //name of previous scene (so that when you're in a level, you return to the scene by which you entered the level)


    private void Awake() {
        int SessionControllerCount = FindObjectsOfType<SessionController>().Length; //Implement singleton
        if (SessionControllerCount > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    public LevelObject GetLevel() {return level;}

    public void SetLevel(LevelObject level) {
        this.level = level.DeepCopy();
    }

    public string GetLevelName() { return string.Copy(levelName); }

    public void SetLevelName(string name) {
        this.levelName = string.Copy(name);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(sceneBefore);
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
        //Debug.Log("Setting sceneBefore to " + scene);
        sceneBefore = string.Copy(scene);
        //Debug.Log("Scenebefore is now " + sceneBefore);
    }

    public string GetSceneBefore() {
        return string.Copy(sceneBefore);
    }
}
