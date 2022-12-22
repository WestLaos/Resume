using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public HealthBar hpBar;
    public bool isAlive = true;
    private Animator animator;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        if (this.transform.name.Equals("Player A"))
        {
            this.setHPBar(GameObject.Find("HealthBar A").GetComponent<HealthBar>());
        }
        else
        {
            this.setHPBar(GameObject.Find("HealthBar B").GetComponent<HealthBar>());
        }
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && isAlive)
        {
            isAlive = false;
            Death();
        }
    }

    public void setHPBar(HealthBar healthBar)
    {
        this.hpBar = healthBar;
        hpBar.MaxHealth(playerHealth);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0) playerHealth = 0;
        hpBar.SetHealth(playerHealth);
    }

    public void Death()
    {
        soundManager.playDeathSound();
        animator.SetTrigger("Death");
    }
}
