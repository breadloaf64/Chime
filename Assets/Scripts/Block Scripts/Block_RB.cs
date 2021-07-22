using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_RB : Block {

    [SerializeField] protected Sprite[] sprites;
    [SerializeField] bool state; 

    private void Start() {
        UpdateBlock();
    }
    protected override void HandleCollision(Collision2D collision) {}

    protected void UpdateSprite() {
        if (state) GetComponent<SpriteRenderer>().sprite = sprites[1];
        else GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public void Toggle() {
        if (state) state = false;
        else state = true;

        UpdateBlock();
        
    }

    private void UpdateBlock() {
        UpdateSprite();
        UpdateCollider();
    }

    private void UpdateCollider() {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = !state;
    }
}
