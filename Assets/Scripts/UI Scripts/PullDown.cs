using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDown : MonoBehaviour {
    [SerializeField] bool state; // false = up, true = down
    [SerializeField] Animator animator;
    [SerializeField] TextMesh toggleText;
    [SerializeField] GameObject[] tabs;

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

    public void ChangeTab(int tabIndex) {
        if (tabIndex < 0 || tabIndex >= tabs.Length) {
            Debug.LogWarning("There is no tab with index " + tabIndex);
            
        }
        else {
            foreach(GameObject tab in tabs) {
                tab.SetActive(false);
            }
            tabs[tabIndex].SetActive(true);
        }
    }
}
