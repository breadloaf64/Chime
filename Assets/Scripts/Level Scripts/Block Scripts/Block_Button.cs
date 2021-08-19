using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block_Button : Block {

    [SerializeField] UnityEvent ButtonFunctions;
    protected override void HandleCollision(Collision2D collision) {
        ButtonFunctions.Invoke();
        //Destroy(gameObject);
    }
}
