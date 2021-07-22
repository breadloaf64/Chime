using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_S : Block {

    [SerializeField] protected Sprite[] sprites;
    bool state = false; //Red = false, blue = true

    protected override void HandleCollision(Collision2D collision) {
        foreach(Block_S b in FindObjectsOfType<Block_S>()) {
            b.Toggle();
        }
        foreach (Block_RB b in FindObjectsOfType<Block_RB>()) {
            b.Toggle();
        }
    }

    protected override void PlayHitSound() {
        int soundIndex;
        if (state) soundIndex = 1;
        else soundIndex = 0;
        AudioSource.PlayClipAtPoint(hitSounds[soundIndex], transform.position);
    }

    protected void UpdateSprite() {
        if (state) GetComponent<SpriteRenderer>().sprite = sprites[1];
        else GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public void Toggle() {
        if (state) state = false;
        else state = true;

        UpdateSprite();
    }
}
