using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPanel : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float boostDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();

            if (playerMovement != null)
            {
                playerMovement.BoostSpeed(speedMultiplier, boostDuration);
            }
        }
    }
}
