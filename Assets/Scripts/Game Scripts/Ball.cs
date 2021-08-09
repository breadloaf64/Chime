using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] float speed = 10f;
    Rigidbody2D rb;
    AudioSource audiosource;
    bool gravityOn;
    [SerializeField] float gravityScale = 1;
    [SerializeField] float drag = 1f;
    CameraShake cs;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cs = FindObjectOfType<CameraShake>();
        if(cs == null) {
            Debug.Log("no camera shake found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gravityOn) ForceSpeed();
    }

    public void Launch(Vector2 launchDir) {

        rb.velocity = launchDir.normalized * speed; //launchDir should be normalised
    }

    void ForceSpeed() {
        Vector2 v = rb.velocity;
        rb.velocity = v.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.name.Equals("BottomCollider")) {
            int soundIndex = Random.Range(0, sounds.Length);
            audiosource.PlayOneShot(sounds[soundIndex]);
            cs.AddShake(1);
        }
    }

    public void ToggleGravity() {
        if (gravityOn) {
            gravityOn = false;
            rb.gravityScale = 0;
            rb.drag = 0;
        }
        else {
            gravityOn = true;
            rb.gravityScale = gravityScale;
            rb.drag = drag;
        }
        
    }
}
