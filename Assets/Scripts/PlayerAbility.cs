using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private GameControls gameControls;

    [SerializeField]
    private GameObject spawnSprite;
    [SerializeField]
    private float cooldownTime = 2.5f;
    [SerializeField]
    private float cooldown = 0;

    void Awake()
    {
        // Initialise required variables
        gameControls = new GameControls();
    }
    
    private void Update()
    {
        if (gameControls.PlayerActionMap.Ability.IsPressed() && !GameManager.Instance.player.GetComponent<EnemyCollision>().isImmune)//.WasPressedThisFrame())
        {
            Debug.Log("Ability Pressed");
            Instantiate(spawnSprite, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.layer = 8;
        }

        if (gameControls.PlayerActionMap.Ability.WasReleasedThisFrame() && !GameManager.Instance.player.GetComponent<EnemyCollision>().isImmune)
        {
            gameObject.layer = 0;
        }
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
