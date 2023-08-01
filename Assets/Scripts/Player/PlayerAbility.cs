using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    // Game contoller
    private GameControls gameControls;

    // Sprite to show on blur effect
    [SerializeField]
    private GameObject spawnSprite;

    // Variable for cooldown
    [SerializeField]
    private float cooldownTime = 5f;
    [SerializeField]
    private float abilityUseTime = 2.5f;
    private bool isAbilityAvailable;
    private bool hasCooldownStarted;

    void Awake()
    {
        // Initialise required variables
        isAbilityAvailable = true;
        hasCooldownStarted = false;
        gameControls = new GameControls();
    }
    
    private void Update()
    {
        // Only do this when playing
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Only perform if the ability available and the players not immune
            if (gameControls.PlayerActionMap.Ability.IsPressed() && !GameManager.Instance.player.GetComponent<EnemyCollision>().isImmune && isAbilityAvailable)
            {
                // Instantiate sprite to create blur effect
                Instantiate(spawnSprite, gameObject.transform.position, gameObject.transform.rotation);
                // Move player to the immune layer
                gameObject.layer = 8;
                // Check if cooldown started
                if (!hasCooldownStarted)
                {
                    // Start Cooldown
                    hasCooldownStarted = true;
                    StartCoroutine(AbilityCooldown());
                }
            }
        }
    }

    // Handles the cooldown
    IEnumerator AbilityCooldown()
    {        
        // Allow use of ability for abilityUseTime
        yield return new WaitForSeconds(abilityUseTime);
        // Turn off ability
        isAbilityAvailable = false;
        // Move to the default layer
        gameObject.layer = 0;
        // Wait for cooldown time to complete
        yield return new WaitForSeconds(cooldownTime);
        // Activate the abilitly again
        isAbilityAvailable = true;
        hasCooldownStarted = false;
    }
    private void OnEnable()
    {
        // Enable the game controls
        gameControls.PlayerActionMap.Enable();
    }

    private void OnDisable()
    {
        // Disable the game controls
        gameControls.PlayerActionMap.Disable();
    }
}
