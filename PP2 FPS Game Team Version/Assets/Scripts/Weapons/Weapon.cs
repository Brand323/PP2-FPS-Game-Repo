using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Weapon Parameters
    public string weaponName;
    public float dropForce = 4f;
    protected Rigidbody weaponRb;
    protected Collider weaponCollider;
    private Animator weaponAnimator;

    
    private void Awake()
    {
        weaponRb = GetComponent<Rigidbody>();
        weaponCollider = GetComponent<Collider>();
        weaponAnimator = GetComponent<Animator>();
    }
}
