using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_RB : Block {
    // both red and blue blocks use this script. The red block

    [SerializeField] protected Sprite[] sprites;
    [SerializeField] bool state; // true means that the block is active - has the solid colour sprite and the ball will bounce off
    private Collider2D myCollider;

    private void Start() {
        myCollider = GetComponent<Collider2D>();
        Debug.Log("myCollider is null? " + (myCollider == null));
        UpdateBlock();
    }
    protected override void HandleCollision(Collision2D collision) {}

    public void Toggle() { //called when the switch block is hit by the ball
        if (state) state = false;
        else state = true;

        UpdateBlock();
        
    }

    private void UpdateBlock() {
        UpdateSprite();
        UpdateCollider();
        HandleDestroyBall();
    }

    protected void UpdateSprite() {
        if (state) GetComponent<SpriteRenderer>().sprite = sprites[1];
        else GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    private void UpdateCollider() {
        if (myCollider != null) {
            myCollider.isTrigger = !state;
        }
        
    }

    private void HandleDestroyBall() {
        // if the ball is inside the block's space when it toggles from off to on, the ball should be destroyed.
        if(state == true) {
            Ball[] balls = FindObjectsOfType<Ball>();
            foreach(Ball b in balls) {
                if (BallInsideBlock(b)) {
                    Destroy(b.gameObject);
                }
            }
        }
    }

    private bool BallInsideBlock(Ball b) {
        return myCollider.bounds.Contains(b.transform.position);
    }
}
