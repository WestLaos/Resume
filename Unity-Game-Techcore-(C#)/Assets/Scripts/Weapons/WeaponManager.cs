using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject GUN;
    void Start()
    {
        for(int i = 0; i != 10; ++i) {
            if(Random.Range(0,2) == 1){
            Instantiate(GUN,new Vector2(Random.Range(-10f,10f),0),transform.rotation);
            }
        }
    }
}
