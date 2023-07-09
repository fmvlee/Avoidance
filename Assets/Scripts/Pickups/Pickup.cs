using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private int scoreIncreaseValue = 0;
    [SerializeField]
    private PickupType pickupType;
    public enum PickupType { Life, Score }


    /// NEED TO ADD SOUND TO THE PICKUPS

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            switch (pickupType)
            {
                case PickupType.Life:
                    GameManager.Instance.lifeManager.AddLife();
                    break;
                case PickupType.Score:
                    GameManager.Instance.scoreManager.AddScore(scoreIncreaseValue);
                    break;
                default:
                    return;
            }
            Destroy(gameObject);
        }
    }
}
