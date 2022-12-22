using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;
    public GameObject Weapon4;
    public GameObject Weapon5;
    public GameObject Weapon6;
    public GameObject Weapon7;
    public GameObject Weapon8;
    public GameObject Weapon9;
    public GameObject Weapon10;
    private bool Spawned = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(!Spawned){
            Spawned = true;
            int Rand = Random.Range(0,11);
            switch(Rand){
                case 1:
                Instantiate(Weapon1, transform.position, transform.rotation);
                break;
                case 2:
                Instantiate(Weapon2, transform.position, transform.rotation);
                break;
                case 3:
                Instantiate(Weapon3, transform.position, transform.rotation);
                break;
                case 4:
                Instantiate(Weapon4, transform.position, transform.rotation);
                break;
                case 5:
                Instantiate(Weapon5, transform.position, transform.rotation);
                break;
                case 6:
                Instantiate(Weapon6, transform.position, transform.rotation);
                break;
                case 7:
                Instantiate(Weapon7, transform.position, transform.rotation);
                break;
                case 8:
                Instantiate(Weapon8, transform.position, transform.rotation);
                break;
                case 9:
                Instantiate(Weapon9, transform.position, transform.rotation);
                break;
                case 10:
                Instantiate(Weapon10, transform.position, transform.rotation);
                break; 
            }
            //Debug.Log("Ground");
            Destroy(gameObject);
            }
        }
    }
}
