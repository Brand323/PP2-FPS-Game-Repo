using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Amount of damage to apply to the player.
    [SerializeField] public float damageAmount = 1f;

    // Detect player collision.
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<FirstPersonController>().TakeDamage(damageAmount);
        }

    }

}
