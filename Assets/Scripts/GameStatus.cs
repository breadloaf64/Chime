using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    
    [SerializeField] Text scoreText;
    [SerializeField] Text ballsText;

    int points = 0;
    int ballCount = 0;

    // Start is called before the first frame update

    private void Awake() {

        //implement singleton
        /*
        int existingCount = FindObjectsOfType<GameStatus>().Length;
        if (existingCount > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void addToPoints(int toAdd) {
        points += toAdd;
        scoreText.text = "Score: " + points.ToString();
    }

    public void addToBallCount(int toAdd) {
        ballCount += toAdd;
        ballsText.text = "Balls: " + ballCount.ToString();
    }

    public void reset() {
        Destroy(gameObject);
    }

    public int getPoints() { return points; }
}
