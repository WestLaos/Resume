using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormsAimingSystem : MonoBehaviour
{
    public float minPower;
    public float maxPower;
    public float currPower;
    public float currAngle;
    public SpriteRenderer AimSprite;
    public WormsShooting Shoot;
    // public PlayerController player;
    public PickUpWeapon isHeld;
    void Start(){
        // AimSprite.enabled = false;
        // player = this.transform.parent.GetComponent<PlayerController>();
        isHeld = this.GetComponent<PickUpWeapon>();
    }
    void Update()
    {
        if (isHeld.held && this.transform.parent.GetComponent<PlayerController>().canAttack) {
            //Debug.Log(currAngle);
            //Debug.Log(currPower);
            AimSprite = transform.parent.transform.Find("Aim").transform.GetComponent<SpriteRenderer>();
            
            if(Input.GetMouseButton(0)){
                AimSprite.enabled = true;
                CalculateAngle();
                CalculatePower();
            }
            else if(Input.GetMouseButtonUp(0)){
                Shoot.FireProjectile((int)currPower);
                AimSprite.enabled = false;
                AimSprite.transform.localScale = new Vector2(0.22f, 0.22f);
                AimSprite.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
            }
        }
    }

    void CalculateAngle(){
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        Vector2 dir = worldPosition - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        UpdateAngle((int)angle);
    }
    void UpdateAngle(int angle){
        currAngle = angle;
        if(currAngle < 0){
            currAngle += 360;
        }
        AimSprite.transform.rotation = Quaternion.AngleAxis(currAngle, Vector3.forward);
    }
    void CalculatePower(){
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        float distance = Vector2.Distance(worldPosition, transform.position);
        UpdatePower((float)distance*2);
   }

    void UpdatePower(float amount){
        currPower = Mathf.Clamp(amount,minPower,maxPower);
        AimSprite.transform.localScale = new Vector2(currPower / 10, currPower / 10);
    }
}
