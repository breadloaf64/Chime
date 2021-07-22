using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BtnHandler : MonoBehaviour
{

    abstract public void HandlePush(); 

    protected SessionController GetSessionController() {
        return FindObjectOfType<SessionController>();
    }

    protected SessionController GetSessionControllerComponent() {
        return FindObjectOfType<SessionController>();
    }
}
