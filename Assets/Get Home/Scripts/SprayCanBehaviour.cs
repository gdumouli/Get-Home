﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCanBehaviour : MonoBehaviour
{
    public LayerMask enemyLayers;
    public float dist = 3f;
    public float height = 2f;
    private float nextAttackTime = 0f;
    public GameObject spray;
    public GameObject attackPoint;
    public ParticleSystem ps;
    public GameObject area;
    private List<Collider2D> enemies = new List<Collider2D>();
    ContactFilter2D filter = new ContactFilter2D();

    //SFX
    public AudioSource spraySound;

    // Start is called before the first frame update
    void Start()
    {
        filter.SetLayerMask(enemyLayers);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerMovement>().lookingRight && area.transform.localScale.x < 0)
        {
            var shape = ps.shape;
            shape.rotation = new Vector3(0, 90, 0);
            area.transform.localScale = new Vector2(-area.transform.localScale.x, area.transform.localScale.y);
        }
        else if (!GetComponent<PlayerMovement>().lookingRight && area.transform.localScale.x > 0)
        {
            var shape = ps.shape;
            shape.rotation = new Vector3(0, -90, 0);
            area.transform.localScale = new Vector2(-area.transform.localScale.x, area.transform.localScale.y);
        }

        area.transform.position = attackPoint.transform.position;
        if (Input.GetKey(KeyCode.E) && Time.time >= nextAttackTime)
        {
            spraySound.Play(); //SFX
            filter.useTriggers = true;
            Physics2D.OverlapCollider(area.GetComponent<Collider2D>(), filter, enemies);
            foreach (Collider2D enemy in enemies)
            {
                Debug.Log(enemy.gameObject.name);
                enemy.GetComponent<EnemyHealth>().TakeDamage(spray.GetComponent<WeaponStats>().lightAttackDamage);
            }
            nextAttackTime = Time.time + 1f / spray.GetComponent<WeaponStats>().lightAttackRate;
            ps.Play();
        }
        else if (!Input.GetKey(KeyCode.E))
        {
            spraySound.Stop(); //SFX
            ps.Stop();
        }
        if (Input.GetKey(KeyCode.E))
        {
            GetComponent<WeaponManagement>().AdjustDurability();
        }
    }
}
