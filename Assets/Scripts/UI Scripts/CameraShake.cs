using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    [SerializeField] float period;
    [SerializeField] float damping;
    [SerializeField] float strength;
    [SerializeField] float maxMagnitude;

    float magnitude;
    int counter;

    Vector3 posPrev;
    Vector3 posNext;

    Vector3 equilibrium;

    // Start is called before the first frame update
    void Start()
    {
        equilibrium = transform.position;
        posPrev = equilibrium;
        posNext = equilibrium;
    }

    // Update is called once per frame
    void Update()
    {
        if (magnitude > 0.001) {
            HandleShake();
        }
    }

    void HandleShake() {
        counter++;

        if (counter >= period) {
            counter = 0;
            posPrev = posNext;
            posNext = NewShakePoint();
            magnitude *= (1 - damping);
        }

        transform.position = Vector3.Lerp(posPrev, posNext, (float)counter / period);
    }

    Vector3 NewShakePoint() {
        float theta = Random.Range(0, 2 * Mathf.PI);
        float x = magnitude * Mathf.Cos(theta);
        float y = magnitude * Mathf.Sin(theta);
        Vector3 offset = new Vector3(x, y, 0);
        return offset + equilibrium;
    }

    public void AddShake(float addMagnitude) {
        magnitude += addMagnitude * strength;
        magnitude = Mathf.Clamp(magnitude, 0, maxMagnitude);
    }
}
