using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    SessionController sc;

    private void Awake() {
        sc = FindObjectOfType<SessionController>();
    }
}
