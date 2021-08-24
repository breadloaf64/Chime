using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Confirmation: MonoBehaviour
{
    [SerializeField] Canvas dialogCanvas;
    [SerializeField] TextMesh messageText;

    public Result result = Result.None;

    public enum Result {
        None,
        Yes,
        No
    }

    public void SetResult(bool yesSelected) {
        if (yesSelected) {
            result = Result.Yes;
        }
        else {
            result = Result.No;
        }
        dialogCanvas.gameObject.SetActive(false);
    }

    public void Show(string message) {
        messageText.text = message;
        result = Result.None;
        dialogCanvas.gameObject.SetActive(true);
    }
}
