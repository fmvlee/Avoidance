using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the player colliding with the enemy
public class EnemyCollision : MonoBehaviour
{
    // Immunity time granted following collision
    [SerializeField]
    private float immuneTime = 3f;
    // Is immunity active
    [SerializeField]
    public bool isImmune = false;
    // Animator for when immunity is active
    private Animator animator;

    private void Start()
    {
        // Get the animator
        animator = GetComponent<Animator>();
    }

    // Handles Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks player has collided with an enemy and isnt immune whilst the game is in the playing state
        if(collision.gameObject.tag == "Enemy" && !isImmune && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Deduct a life
            GameManager.Instance.lifeManager.RemoveLife();
            // If the player has lives left the state will be playing
            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                // Immunity starts
                StartImmunity();
            }
        }
    }

    // Handles the activation of immunity
    private void StartImmunity()
    {
        isImmune = true;
        // Start the immunity animation
        animator.SetBool("Immune", true);
        // Move the player to the immune layer so they can pass through enemies
        gameObject.layer = 8;
        // End immunity
        StartCoroutine(EndImmunity());
    }

    // Handles ending immunity
    IEnumerator EndImmunity()
    {
        // Wait the immuneTime before ending immunity
        yield return new WaitForSeconds(immuneTime);
        isImmune = false;
        // Move the player back to the default layer
        gameObject.layer = 0;
        // Enable the players collider
        GetComponent<CircleCollider2D>().enabled = true;
        // Stop the animation
        animator.SetBool("Immune", false);
    }
}
