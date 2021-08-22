using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] GameObject selectionBox;
    [SerializeField] Camera myCamera;
    [SerializeField] InputField infieldLevelName;
    [SerializeField] int maxLevelNameSize = 11;
    [SerializeField] InputField infieldLevelText;

    //External references
    SessionController sc;
    LevelHandler lh;
    SceneLoader sl;
    PullDown pd;
    PopupText pt;

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
        sc = SessionController.Instance;
        lh = FindObjectOfType<LevelHandler>();
        sl = FindObjectOfType<SceneLoader>();
        pd = FindObjectOfType<PullDown>();
        pt = FindObjectOfType<PopupText>();
    }

    void Start()
    {
        if (sc != null) {
            level = sc.GetLevel();
            if (level.name != "defaultLevel") {
                infieldLevelName.text = level.name;
            }
            infieldLevelText.text = level.levelText;
        }
        else {
            level = LevelObject.DefaultLevel();
            infieldLevelName.text = "";
            Debug.Log("LevelEditor: No SessionController found. loading default level");
        }
        StandardiseGenString(level);
        history = new LevelHistory();
        history.Add(level);
        
    }

    void StandardiseGenString(LevelObject l) {
        string genString = l.genString;
        string standardised = "";
        for (int i = 0; i < genString.Length; i++) {
            if (lh.IsValidType(genString[i])) {
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
        // if pulldown is down, then  don't allow user to add blocks to level
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
                    TrySaveLevel();
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
            if (keyName.Length == 1 && lh.IsValidType(keyName[0])) {
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
                sc.SetLevel(level);
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

    public void TrySaveLevel() {

        if (ValidateLevel()) {

            if (LevelSaveLoad.LevelExists(infieldLevelName.text)) {
                StartCoroutine(OverwriteLevelConfirmation());
            }
            else {
                SaveLevel();
            }

        }
    }

    private bool ValidateLevel() {
        return (level.name.Length > 0);
    }

    private IEnumerator OverwriteLevelConfirmation() {
        string name = infieldLevelName.text;

        Dialog_Confirmation dialog = FindObjectOfType<Dialog_Confirmation>();
        dialog.Show("Are you sure you want to \n overwrite " + name + "?");
        Debug.Log("Showing Dialog");

        while (dialog.result == Dialog_Confirmation.Result.None) {
            yield return null; // wait
        }

        if (dialog.result == Dialog_Confirmation.Result.Yes) {
            SaveLevel();
        }
        else if (dialog.result == Dialog_Confirmation.Result.No) {
            // do nothing
        }
    }

    private void SaveLevel() {
        string name = infieldLevelName.text;

        if (name.Equals("")) {
            pt.Show("Please name your level");
            Debug.Log("LevelEditor | couldn't save level - has no name"); 
        }
        else {
            LevelSaveLoad.Save(level, name);
            pt.Show(name + " saved");
            Debug.Log("Level Saved!");
        }
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
            pt.Show("Level cleared");
        }
    }

    public void CopyLevelToClipboard() {
        string levelJSON = JsonUtility.ToJson(level);
        GUIUtility.systemCopyBuffer = levelJSON;
        pt.Show("Copied " + level.name + " to clipboard");
        Debug.Log("Copied level JSON text to clipboard");
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
            if (lh.IsValidType(brushType)) {
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

    public void UpdateLevelName() {
        string name = infieldLevelName.text;

        // Truncate level name if needed [to fit in text box]
        if (name.Length > maxLevelNameSize) {
            name = name.Substring(0, maxLevelNameSize);
        }

        // set values
        infieldLevelName.text = name;
        level.name = name;
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
