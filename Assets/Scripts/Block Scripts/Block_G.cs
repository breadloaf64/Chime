using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_G : Block {
    protected override void HandleCollision(Collision2D collision) {
        if(collision.gameObject.CompareTag("Ball")) {
            collision.gameObject.GetComponent<Ball>().ToggleGravity();
        }
    }
}
