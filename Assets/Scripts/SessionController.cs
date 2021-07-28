using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionController : MonoBehaviour
{
    private LevelObject level;
    string sceneBefore; //name of previous scene (so that when you're in a level, you return to the scene by which you entered the level)

    private void Awake() {
        level = LevelObject.DefaultLevel();
        int SessionControllerCount = FindObjectsOfType<SessionController>().Length; //Implement singleton
        if (SessionControllerCount > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
        Debug.Log("sc setting default level");
    }

    public LevelObject GetLevel() {
        Debug.Log("get level: " + level.genString);
        return level.DeepCopy();
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
        //press r to reload the current scene, basically a restart button
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
