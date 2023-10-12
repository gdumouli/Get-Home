﻿using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class PlayerHealth : MonoBehaviour{    public Animator animator;    public int maxHealth = 100;    public int currentHealth;    private bool dead = false;    public GameObject healthBar;    // Start is called before the first frame update    void Start()    {
        healthBar.GetComponent<Slider>().maxValue = maxHealth;        setFullHealth();    }    // Update is called once per frame    void Update()    {    }    public void TakeDamage(int damage)    {        currentHealth -= damage;        DisplayHealth();        if (currentHealth <= 0)        {            dead = true;
            animator.SetTrigger("Dead");            GetComponent<PlayerCombatMelee>().DisableCombo();        }    }    void DisplayHealth()    {        healthBar.GetComponent<Slider>().value = currentHealth;    }    public void setFullHealth()    {        currentHealth = maxHealth;        DisplayHealth();    }    private void OnTriggerEnter2D(Collider2D collider)    {        if (collider.transform.parent.gameObject.name == "Beer")        {            currentHealth = maxHealth;            DisplayHealth();            collider.transform.parent.gameObject.SetActive(false);        }    }}