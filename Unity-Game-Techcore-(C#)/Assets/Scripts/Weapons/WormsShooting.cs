using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormsShooting : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform Muzzle;
    public void FireProjectile(int power){
        Muzzle = transform.parent.Find("Aim").Find("Muzzle").transform;
        GameObject insProj = Instantiate(ProjectilePrefab, Muzzle.transform.position, Muzzle.transform.rotation);
        insProj.GetComponent<WormsProjectile>().Initialize(power);
    }
}
