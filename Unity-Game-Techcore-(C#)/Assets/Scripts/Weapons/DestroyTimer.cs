using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 0.25f);
    }

    void Explode(){
        Destroy(gameObject);
    }
}
