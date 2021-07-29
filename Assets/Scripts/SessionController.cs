using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionController : MonoBehaviour
{
    private LevelObject level = LevelObject.DefaultLevel();
    string sceneBefore; //name of previous scene (so that when you're in a level, you return to the scene by which you entered the level)

    // These are here to facilitate singleton. _instance privately holds the reference to the singleton instance
    private static SessionController _instance;
    // Instance publically returns the reference to the single instance
    public static SessionController Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public LevelObject GetLevel() {
        return this.level.DeepCopy();
    }

    public void SetLevel(LevelObject level) {
        this.level = level.DeepCopy();
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
