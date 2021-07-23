﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] GameObject selectionBox;
    [SerializeField] Camera myCamera;
    [SerializeField] InputField infieldLevelName;
    [SerializeField] InputField infieldLevelText;

    //External references
    SessionController sc;
    LevelHandler lh;
    SceneLoader sl;
    PullDown pd;

    // Input variables
    bool shift = false;
    bool ctrl = false;

    bool disableEdits = false;

    //Storing current state
    LevelObject level;
    LevelHistory history;
    char brushType = '1';
    Vector2 selectionPosition;

    private void Awake() {
        sc = FindObjectOfType<SessionController>();
        lh = FindObjectOfType<LevelHandler>();
        sl = FindObjectOfType<SceneLoader>();
        pd = FindObjectOfType<PullDown>();
    }

    void Start()
    {
        if (sc != null) {
            level = sc.GetLevel();
            infieldLevelName.text = sc.GetLevelName();
        }
        else {
            level = LevelObject.DefaultLevel();
            infieldLevelName.text = "DefaultLevel";
        }
        StandardiseGenString(level);
        history = new LevelHistory();
        history.Add(level);
        
    }

    void StandardiseGenString(LevelObject l) {
        string genString = l.genString;
        string standardised = "";
        for (int i = 0; i < genString.Length; i++) {
            if (lh.isValidType(genString[i])) {
                standardised += genString[i];
            }
        }
        while (standardised.Length < 12 * 16) {
            standardised += "0";
        }
        l.genString = standardised;
    }

    void Update() {
        SetDisableEdits();
        HandleInput();
        MoveSelection();
    }

    void SetDisableEdits() {
        disableEdits = pd.GetState();
        //check for warning box
    }

    void MoveSelection() {
        if (!pd.GetState()) {
            selectionPosition = SnapToGridCentre(MousePositionConstrained());
            selectionBox.transform.position = selectionPosition;
        }
    }

    void SetCtrl() {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) ctrl = true;
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) ctrl = false;
    }

    void SetShift() {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) shift = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) shift = false;
    }

    void HandleInput() {
        SetCtrl();
        SetShift();

        //detectKey
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKey(vKey)) {
                HandleKeyPress(vKey);
            }
        }
    }

    void HandleKeyPress(KeyCode key) {
        string keyName = key.ToString();

        if (Input.anyKeyDown && key == KeyCode.PageUp || Input.anyKeyDown && key == KeyCode.PageDown) pd.Toggle();

        if (ctrl && !shift) { //ctrl shortcuts
            if (Input.anyKeyDown) {
                if (key == KeyCode.S) {
                    Save();
                }
                else if (key == KeyCode.Z) {
                    Undo();
                }
                else if (key == KeyCode.Y) {
                    Redo();
                }
                else if (key == KeyCode.C) {
                    ClearAll();
                }
            }
        }
        else if (shift && !ctrl && !disableEdits) { //shift letters
            if (keyName.Equals("T")) {
                MakeBlock('t');
            }
        }
        else if (!shift && !ctrl && !disableEdits) {
            if (keyName.Length == 1 && lh.isValidType(keyName[0])) {
                MakeBlock(keyName[0]);
            }
            else if (keyName.Equals("Alpha1")) {
                MakeBlock('1');
            }
            else if (keyName.Equals("Alpha2")) {
                MakeBlock('2');
            }
            else if (keyName.Equals("Alpha3")) {
                MakeBlock('3');
            }
            else if (keyName.Equals("Return")) {
                Debug.Log("set level has gentring: " + level.genString);
                sc.SetLevel(level);
                sc.SetLevelName(infieldLevelName.text);
                sl.LoadScene("Level");
            }
            else if (keyName.Equals("L")) {
                MoveLauncher();
            }
            else if (keyName.Equals("Mouse0")) {
                HandleMousePress();
            }
            else if (keyName.Equals("Mouse1")) {
                MakeBlock('0');
            }
        }
    }

    public void Save() {
        string name = infieldLevelName.text;
        LevelSaveLoad.Save(level, name);


        //Level saved popup
        Debug.Log("Level Saved!");
    }

    public void Undo() {
        level = history.Back();
        lh.LoadLevel(level);
    }

    public void Redo() {
        level = history.Forward();
        lh.LoadLevel(level);
    }

    public void ClearAll() {
        string newGenString = LevelObject.DefaultLevel().genString;
        if (!level.genString.Equals(newGenString)) {
            level.genString = newGenString;
            lh.LoadLevel(level);
            history.Add(level);
        }
    }

    void MoveLauncher() {
        Vector2 newPosition = SnapToGridVertical(MousePositionConstrained());
        if (!level.launcherPosition.Equals(newPosition)) {
            level.launcherPosition = newPosition;
            FindObjectOfType<Launcher>().transform.position = newPosition;
            history.Add(level);
        }
    }

    void HandleMousePress() {
        if (!EventSystem.current.IsPointerOverGameObject()) { // don't make blocks if pressing pullDown button
            if (lh.isValidType(brushType)) {
                MakeBlock(brushType);
            }
            else if (brushType == 'L') {
                MoveLauncher();
            }
        }
    }

    public void ChangeBrush(string brushType) {
        this.brushType = brushType[0];
    }

    public void NextTheme() {
        level.themeIndex = (level.themeIndex + 1) % Theme.themeCount;
        lh.LoadLevel(level);
    }

    public void UpdateLevelText() {
        string levelText = infieldLevelText.text;
        level.levelText = levelText;
    }

    void MakeBlock(char type) {
        string replaced = ReplaceAt(level.genString, TargetIndex(), type);
        if (level.genString != replaced) {
            level.genString = replaced;
            lh.LoadLevel(level);
            history.Add(level);
        }
    }

    public static string ReplaceAt(string input, int index, char newChar) {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

    int TargetIndex() {
        Vector2 pos = LevelHandler.WorldToGen(selectionPosition);
        return (int)(16 * pos.y + pos.x);
    }

    private Vector2 SnapToGridCentre(Vector2 v) {
        float x = Mathf.RoundToInt(v.x + 0.5f) - 0.5f;
        float y = Mathf.RoundToInt(v.y + 0.5f) - 0.5f;
        return new Vector2(x, y);
    }

    private Vector2 SnapToGridVertical(Vector2 v) {
        float x = Mathf.RoundToInt(v.x);
        float y = Mathf.RoundToInt(v.y + 0.5f) - 0.5f;
        return new Vector2(x, y);
    }

    private Vector2 MousePositionConstrained() {
        float x = myCamera.ScreenToWorldPoint(Input.mousePosition).x;
        float y = myCamera.ScreenToWorldPoint(Input.mousePosition).y;

        x = Mathf.Clamp(x, 0.1f, 15.9f);
        y = Mathf.Clamp(y, 0.1f, 11.9f);

        return new Vector2(x, y);
    }
}
