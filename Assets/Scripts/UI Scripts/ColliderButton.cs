using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderButton : MonoBehaviour


{
    private void OnCollisionEnter2D(Collision2D collision) {

        GetComponent<Button>().onClick.Invoke();
    }
}
