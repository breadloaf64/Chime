using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{

    //References
    [SerializeField] protected AudioClip[] hitSounds;
    protected GameStatus gs;

    //abstract methods
    protected abstract void HandleCollision(Collision2D collision);
    virtual protected void PlayHitSound() {
        int soundIndex = Random.Range(0, hitSounds.Length - 1);
        AudioSource.PlayClipAtPoint(hitSounds[soundIndex], transform.position);
    }

    private void Awake() {
        gs = FindObjectOfType<GameStatus>();
    }
    

    private void OnCollisionEnter2D(Collision2D collision) {
        HandleCollision(collision);
        Enlarge();
        PlayHitSound();
    }

    protected void Enlarge() {
        float scalar = 1.08f;

        GameObject bigSprite = Instantiate(this.gameObject, transform.position, transform.rotation, transform.parent);
        Destroy(bigSprite.GetComponent<Collider2D>());
        bigSprite.transform.localScale = transform.localScale * scalar;

        Destroy(bigSprite, 0.1f);
    }

    

    
}
