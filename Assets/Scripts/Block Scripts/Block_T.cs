using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_T : Block {
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] bool state;

    private void Start() {
        UpdateBlock();
    }
    protected override void HandleCollision(Collision2D collision) {
        Toggle();
    }

     protected override void PlayHitSound() {
        int soundIndex;
        if (state) soundIndex = 1;
        else soundIndex = 0;
        AudioSource.PlayClipAtPoint(hitSounds[soundIndex], transform.position);
    }

    protected private void OnTriggerExit2D(Collider2D collision) {
        Toggle();
        PlayHitSound();
    }

    private void Toggle() {
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

    protected void UpdateSprite() {
        if (state) GetComponent<SpriteRenderer>().sprite = sprites[1];
        else GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
