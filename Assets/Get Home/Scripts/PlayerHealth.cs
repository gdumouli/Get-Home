﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    private bool dead = false;
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<Slider>().maxValue = maxHealth;
        setFullHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        DisplayHealth();
        if (currentHealth <= 0)
        {
            dead = true;
            Death();
        }
    }

    public void setFullHealth()
    {
        currentHealth = maxHealth;
        DisplayHealth();
    }

    void DisplayHealth()
    {
        healthBar.GetComponent<Slider>().value = currentHealth;
    }

    void Death()
    {
        animator.SetTrigger("Dead");
        GetComponent<PlayerMovement>().enabled = false;
    }
}
