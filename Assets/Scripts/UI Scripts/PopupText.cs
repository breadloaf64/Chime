using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    [SerializeField] Text txt;
    [SerializeField] Animator FadeAnimator;

    public void Show(string message) {
        txt.text = message;
        FadeAnimator.SetTrigger("Popup");
    }
}
