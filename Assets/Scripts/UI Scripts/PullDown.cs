using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDown : MonoBehaviour
{
    [SerializeField] bool state; // false = up, true = down
    [SerializeField] Animator animator;
    [SerializeField] TextMesh toggleText;

    public void Toggle() {
        if (state) {
            state = false;
            toggleText.text = "\\/";
            animator.SetBool("Down", false);
        }
        else {
            state = true;
            toggleText.text = "/\\";
            animator.SetBool("Down", true);
        }
    }

    public bool GetState() { return state; }
}
