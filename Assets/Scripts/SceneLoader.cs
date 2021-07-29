using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string previousSceneName;
    SessionController sc;
    [SerializeField] Animator transition;
    float transitionTime = 0.5f;

    private void Awake() {
        sc = SessionController.Instance;
    }

    public string GetCurrentScene() {
        return SceneManager.GetActiveScene().name;
    }

    public void LoadNextScene() {
        sc.SetSceneBefore(SceneManager.GetActiveScene().name);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        //StartCoroutine(TransitionLoadScene(currentSceneIndex + 1));
    }

    public void LoadFirstScene() {
        sc.SetSceneBefore(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(0);
        //StartCoroutine(TransitionLoadScene(0));
    }



    public void LoadScene(string sceneName) {
        sc.SetSceneBefore(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
        //StartCoroutine(TransitionLoadScene(sceneName));
    }

    public void LoadPreviousScene() {
        string sceneBefore = SceneManager.GetActiveScene().name;

        if (SceneManager.GetActiveScene().name == "Level") { //accesses session controller to see what scene the user was in before entering level
            Debug.Log("Loading scene: " + sc.GetSceneBefore());
            SceneManager.LoadScene(sc.GetSceneBefore());
            //StartCoroutine(TransitionLoadScene(sc.GetSceneBefore()));
        }
        else if (previousSceneName.Equals("Quit")) {
            Application.Quit();
            Debug.Log("The application would quit here");
        }
        else if (checkSceneName(previousSceneName)) {
            SceneManager.LoadScene(previousSceneName);
            //StartCoroutine(TransitionLoadScene(previousSceneName));
        }
        sc.SetSceneBefore(sceneBefore);
    }

    private bool checkSceneName(string sceneName) {
        bool found = false;
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (sceneName.Equals(sceneNameFromPath(scenePath))) {
                found = true;
            }
        }
        if (!found) Debug.LogError("Sceneloader: No scene found with the name '" + sceneName + "'." );
        return found;
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator TransitionLoadScene(string scene) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }

    IEnumerator TransitionLoadScene(int scene) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }

    private string sceneNameFromPath(string path) {
        string[] bits = path.Split('/');
        string endbit = bits[bits.Length - 1];
        return endbit.Split('.')[0];
    }
}
