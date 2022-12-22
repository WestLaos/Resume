using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float _LaserBeamLength;
    private LineRenderer _LineRenderer;

    private void Start(){
        _LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update(){  
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = Camera.main.nearClipPlane + 1;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 endPosition = transform.position + (transform.right * _LaserBeamLength);

        _LineRenderer.SetPositions(new Vector3[] {transform.position, endPosition});
    }
}
