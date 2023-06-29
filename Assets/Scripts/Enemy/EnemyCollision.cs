using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Deactivate the players collider so the player passes through enemies
        GetComponent<CircleCollider2D>().enabled = false;
        // End immunity
        StartCoroutine(EndImmunity());
    }

    // Handles ending immunity
    IEnumerator EndImmunity()
    {
        // Wait the immuneTime before ending immunity
        yield return new WaitForSeconds(immuneTime);
        isImmune = false;
        // Enable the players collider
        GetComponent<CircleCollider2D>().enabled = true;
        // Stop the animation
        animator.SetBool("Immune", false);
    }
}
