using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointText : MonoBehaviour
{

    [SerializeField] int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 force = new Vector2(0, 200);
        GetComponent<Rigidbody2D>().AddForce(force);
        GetComponent<TextMesh>().text = "+" + points.ToString();
    }

    public void setPoints(int _points) {
        points = _points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
