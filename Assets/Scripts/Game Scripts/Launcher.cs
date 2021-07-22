using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Ball ball;

    GameStatus gs;

    //Launcher properties
    Vector2 dir;
    bool snap;
    float angle;
    [SerializeField] int snapAmount = 5;
    [SerializeField] bool active = true;



    private void Awake() {
        gs = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if(active) {
            if (Input.GetKeyDown(KeyCode.S)) { ToggleSnap(); }

            UpdateDirVector();
            transform.eulerAngles = new Vector3(0, 0, angle); //rotate sprite

            if (Input.GetMouseButtonDown(0)) {
                if (SceneManager.GetActiveScene().name.Equals("Level")) { //launch ball at any time if in level
                    LaunchBall();
                }
                else if  (!EventSystem.current.IsPointerOverGameObject()) { // Don't launch ball in menus if mouse is over a button/textbox etc
                    LaunchBall();
                }
            }
        }

    }

    void ToggleSnap() {
        snap = !snap;
    }

    void UpdateDirVector() {
        dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (snap) {
            angle = Mathf.Round(angle / snapAmount) * snapAmount;
            dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }
    }

    void LaunchBall() {
        if(gs != null) {
            gs.addToBallCount(1);
        }
        
        Ball newBall = Instantiate(ball, transform.position, Quaternion.identity);
        newBall.Launch(dir);
    }
}
