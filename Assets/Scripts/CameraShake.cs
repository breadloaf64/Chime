using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float shakeMagnitude;
    [SerializeField] float damping;

    Vector3 equilibrium;

    // Start is called before the first frame update
    void Start()
    {
        equilibrium = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
        shakeMagnitude *= (1 - damping);
    }

    void Shake() {
        float theta = Random.Range(0, 2 * Mathf.PI);
        float x = shakeMagnitude * Mathf.Cos(theta);
        float y = shakeMagnitude * Mathf.Sin(theta);
        Vector3 offset = new Vector3(x, y, 0);

        this.transform.position = equilibrium + offset;
    }

    public void AddShake(float magnitude) {
        shakeMagnitude += magnitude * 0.02f;
    }
}
