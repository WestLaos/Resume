using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public bool held = false;
    public float delay = 1.0f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private GameObject arm;
    private bool Cyborg = false;
    private bool Biker = false;
    private bool Punk = false;
    private bool doOnce = true;
    private bool canShoot = true;
    void Update()
    {
        if (held)
        {
            if (this.transform.parent.Find("Player_Cyborg") !=  null)
            {
                arm = this.transform.Find("Cyborg_Arm").gameObject;
                Cyborg = true;
            }
            else if (this.transform.parent.Find("Player_Punk") != null)
            {
                arm = this.transform.Find("Punk_Arm").gameObject;
                Punk = true;
            }
            else if (this.transform.parent.Find("Player_Biker") != null)
            {
                arm = this.transform.Find("Biker_Arm").gameObject;
                Biker = true;
            }
            arm.GetComponent<SpriteRenderer>().enabled = true;
            arm.GetComponent<Animator>().enabled = true;

            if (Input.GetKey(KeyCode.F))
            {
                arm.GetComponent<SpriteRenderer>().enabled = false;
                arm.GetComponent<Animator>().enabled = false;
                this.transform.parent = null;
                if(Cyborg){
                    if(GameObject.Find("Player_Cyborg").transform.parent.GetComponent<PlayerController>().FacingRight){
                        this.transform.position += new Vector3(-0.5f,0,0);
                    }
                    else{
                        this.transform.position += new Vector3(0.5f,0,0);
                    }
                }
                else if(Punk){
                    if(GameObject.Find("Player_Punk").transform.parent.GetComponent<PlayerController>().FacingRight){
                        this.transform.position += new Vector3(-0.5f,0,0);
                    }
                    else{
                        this.transform.position += new Vector3(0.5f,0,0);
                    }    
                }
                else if(Biker){
                    if(GameObject.Find("Player_Biker").transform.parent.GetComponent<PlayerController>().FacingRight){
                        this.transform.position += new Vector3(-0.5f,0,0);
                    }
                    else{
                        this.transform.position += new Vector3(0.5f,0,0);
                    }
                }
                held = false;
                Cyborg = false;
                Biker = false;
                Punk = false;
            }
        }
    }

    public void Shoot(){
        if(canShoot){
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0,0,0));
            canShoot = false;
        }
        if(!canShoot && doOnce){
            Invoke("resetTimer", delay);
            doOnce = false;
        }
    }

    private void resetTimer(){
        canShoot = true;
        doOnce = true;
    }
}
