using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRepeater : MonoBehaviour
{
    private float speed;
    public Vector3 startPos;
    public float repeatWidth;
    public float xMin;

    void Start()
    {
        speed = 5f;
        startPos = transform.position;
        //repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        repeatWidth = GetComponent<BoxCollider>().size.x;
        xMin = startPos.x - repeatWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < xMin || transform.position.x > startPos.x)
        {
            //transform.position = startPos;

            // reverse direction
            speed *= -1;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
