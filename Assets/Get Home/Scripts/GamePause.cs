using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseScreen;
    public GameObject deathScreen;

    public Vector3 checkpoint = new Vector3(0, 0, 0);

    private bool gamePaused = false;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("DEAD status: " + dead);
        // activate pause screen
        if (Input.GetKeyDown(KeyCode.P) && !dead)
        {
            gamePaused = !gamePaused;
            player.GetComponent<PlayerMovement>().enabled = !gamePaused;
            pauseScreen.SetActive(gamePaused);
        }

        // if player died, wait for input to restart
        if (dead)
        {
            if (Input.anyKeyDown)
            {
                player.transform.position = checkpoint;
                player.GetComponent<PlayerHealth>().setFullHealth();
                setDeath(false);
            }
        }

        // check if player is dead
        if (player.GetComponent<PlayerHealth>().currentHealth <= 0 && !dead)
        {
            setDeath(true);
        }

    }

    void setDeath(bool isDead)
    {
        // show player screen & update script
        dead = isDead;
        deathScreen.SetActive(dead);

        player.GetComponent<PlayerMovement>().enabled = !dead;
        player.GetComponent<Animator>().enabled = !dead;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // toggle if enemies are "alive" or not
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Animator>().enabled = !dead;
            var scripts = enemy.GetComponents<MonoBehaviour>();
            // toggle enemy's scripts
            foreach (var script in scripts)
            {
                script.enabled = !dead;
                Debug.Log("Script: " + script);
            }
        }
    }

    public void updateCheckpoint(Vector3 newPos)
    {
        Debug.Log("Checkpoint called.");
        checkpoint = newPos;
    }
}
