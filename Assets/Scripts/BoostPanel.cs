using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPanel : MonoBehaviour
{
    public float speedMultiplier = 2f; // How much to multiply the speed
    public float boostDuration = 2f; // How long the boost lasts

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has entered the boost area
        if (collision.CompareTag("Player"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();

            if (playerMovement != null)
            {
                playerMovement.BoostSpeed(speedMultiplier, boostDuration); // Boost player's speed
            }
        }
    }
}
