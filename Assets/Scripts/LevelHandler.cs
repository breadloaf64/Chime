using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour {
    [SerializeField] GameObject BlockParent;
    [SerializeField] Launcher launcher;

    //Inspector input system for block object references;
    [System.Serializable]
    public struct KeyBlock {
        public char key;
        public GameObject BlockObject;
    }
    [SerializeField] KeyBlock[] KeyBlockMap;
    Dictionary<char, GameObject> BlockLookup;

    
    static int w = 16, h = 12;

    private void Awake() {
        createBlockLookup();
    }
    
    void createBlockLookup() {
        BlockLookup = new Dictionary<char, GameObject>();
        foreach (KeyBlock keyblock in KeyBlockMap) {
            BlockLookup.Add(keyblock.key, keyblock.BlockObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        SessionController sc = FindObjectOfType<SessionController>();
        if (sc != null) {
            LoadLevel(sc.GetLevel());
        }
        else {
            LoadLevel(LevelObject.DefaultLevel());
        }
    }

    public void LoadLevel(LevelObject level) {
        clearAllBlocks();
        launcher.transform.position = level.launcherPosition; //set launcher position
        int themeIndex = level.themeIndex;
        Theme theme = Theme.getTheme(themeIndex);
        GameObject.Find("Back_1").GetComponent<SpriteRenderer>().color = theme.backCol1;
        GameObject.Find("Back_2").GetComponent<SpriteRenderer>().color = theme.backCol2;
        makeBlocksFromGenString(removeInvalidChars(level.genString));

        //level text
        GameObject levelText = GameObject.Find("txtLevelText");
        if (levelText != null) {
            levelText.GetComponent<Text>().text = level.levelText;
        }
    }

    void clearAllBlocks() {
        foreach (Transform block in BlockParent.transform) {
            Destroy(block.gameObject);
        }
    }

    string removeInvalidChars(string original) {
        string removed = "";
        for (int i = 0; i < original.Length; i++) {
            if (isValidType(original[i])) {
                removed += original[i];
            }
            else {
                Debug.LogWarning("LevelHandler: Invalid char '" + original[i] + "' has been ommitted from GenString! Oh no!");
            }
        }
        return removed;
    }

    public bool isValidType(char c) {
        return c == '0' || BlockLookup.ContainsKey(c);
    }

    void makeBlocksFromGenString(string genString) { //This assumes that the Genstring is valid
        genString = removeInvalidChars(genString);
        char blockType;
        int x, y;
        Vector2 pos;

        if (genString.Length > w * h) {
            Debug.LogWarning("the GenString is too long. The level will be truncated.");
        }

        for (int i = 0; i < genString.Length; i++) {
            x = i % w;
            y = Mathf.FloorToInt(i / w);
            blockType = genString[i];
            pos = new Vector2(x, y);
            makeBlock(blockType, GenToWorld(pos));
        }

    }

    public static Vector2 WorldToGen(Vector2 worldV) {
        float x = worldV.x - 0.5f;
        float y = (12 - worldV.y - 0.5f);
        return new Vector2(x, y);
    }

    public static Vector2 GenToWorld(Vector2 genV) {
        float x = genV.x + 0.5f;
        float y = 12 - genV.y - 0.5f; //actual x and y coordinates in relation to parent
        return new Vector2(x, y);
    }

    public void makeBlock(char type, Vector2 pos) {

        if (type == '0') {
            //Do nothing
        }
        else {
            Instantiate(BlockLookup[type], pos, BlockParent.transform.rotation, BlockParent.transform);
        }
    }
}
