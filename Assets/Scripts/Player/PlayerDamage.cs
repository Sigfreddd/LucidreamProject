using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private GameOver gameOver;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AudioClip hornSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hurting"))
        {
            if (collision.gameObject.CompareTag("Car"))
                AudioManager.instance.PlayClipAt(hornSound, transform.position);

            int distance = playerMovement.GetMaxSteps();
            gameOver.ActivateGameOver(distance);
            Destroy(gameObject);
        }
    }
}
