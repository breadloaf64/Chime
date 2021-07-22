using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_123 : Block {
    [SerializeField] protected int hitsLeft = 3;
    [SerializeField] protected int breakScore = 10;
    [SerializeField] protected int hitScore = 5;
    [SerializeField] protected AudioClip breakSound;
    [SerializeField] protected GameObject breakEffect;
    [SerializeField] protected Sprite[] hitSprites;
    [SerializeField] protected GameObject PointTextEffect;

    protected override void HandleCollision(Collision2D collision) {
        Damage();
        UpdateSprite();
    }

    private void Damage() {
        hitsLeft--;
        if (hitsLeft == 0) {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
            TriggerBreakEffect();
            scorePoints(breakScore);
            Destroy(gameObject);
        }
        else {
            scorePoints(hitScore);
        }
    }

    void TriggerBreakEffect() {
        GameObject sparkles = Instantiate(breakEffect, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }

    private void scorePoints(int points) {
        if (gs != null) { // no gamestatus in menu screens
            gs.addToPoints(points);
            float randomAmount = 1f;
            Vector2 tweak = new Vector2(Random.Range(-randomAmount, randomAmount), Random.Range(-randomAmount, randomAmount));
            GameObject pointText = Instantiate(PointTextEffect, (Vector2)transform.position + tweak, transform.rotation);
            pointText.GetComponent<PointText>().setPoints(points);
            Destroy(pointText, 1);
        }

    }

    protected void UpdateSprite() {
        int spriteIndex = Mathf.Clamp(hitsLeft - 1, 0, 2);
        if (hitSprites[spriteIndex] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else {
            Debug.Log("HitSprite missing from " + gameObject.name);
        }
    }
}
