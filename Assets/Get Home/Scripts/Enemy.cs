using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 40;
    protected int currentHealth;
    public Animator animator;

    protected bool keepActive = false; // used for child classes

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play Hurt Animation
        animator.SetTrigger("Hurt");
        // Die
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;

        if (!keepActive)
        {
            this.enabled = false;
        }
    }
}
