using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormsProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rbProjectile;
    public float AliveTime;
    public GameObject ExplosionPrefab;

    void Awake()
    {
        rbProjectile = GetComponent<Rigidbody2D>();
        Invoke("Explode",AliveTime);
        Invoke("EnableCollider", 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!collision.CompareTag("Player")){
            Destroy(collision);
            Explode();
        }
    }

    public void Initialize(int power){
        rbProjectile.AddForce(transform.right * (power), ForceMode2D.Impulse);
    }

    void EnableCollider(){
        GetComponent<Collider2D>().enabled = true;
    }

    void Explode(){
        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
