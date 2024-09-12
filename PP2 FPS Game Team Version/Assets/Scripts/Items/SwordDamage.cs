using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{

    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private Collider swordCollider;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other is CapsuleCollider)
            {
                I_Damage enemyDamage = other.GetComponent<I_Damage>();

                if (enemyDamage != null && swordCollider.enabled)
                {
                    enemyDamage.TakeDamage(damageAmount);
                    Debug.Log("sword did" + damageAmount + " damage");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
