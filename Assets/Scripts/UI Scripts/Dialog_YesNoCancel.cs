using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_YesNoCancel : MonoBehaviour
{
    [SerializeField] Canvas dialogCanvas;
    [SerializeField] TextMesh messageText;

    public Result result = Result.None;

    public enum Result {
        None,
        Yes,
        No,
        Cancel
    }

    public void SetResult(int resultIndex) {
        switch(resultIndex) {
            case 1:
                result = Result.Yes;
                break;
            case 2:
                result = Result.No;
                break;
            case 3:
                result = Result.Cancel;
                break;
            default:
                return;
        }
        dialogCanvas.gameObject.SetActive(false);
    }

    public void Show(string message) {
        messageText.text = message;
        result = Result.None;
        dialogCanvas.gameObject.SetActive(true);
        Debug.Log("Start Dialog");
    }
}
