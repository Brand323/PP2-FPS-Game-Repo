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

    // Dropping the weapon using rigidbody forces
    public virtual void Drop()
    {
        // Disable the weapon animator while not equipped
        if (weaponAnimator != null)
            weaponAnimator.enabled = false;

        // Adjust weapon physics
        weaponRb.isKinematic = false;
        weaponRb.useGravity = true;
        weaponCollider.enabled = true;

        // Detatch the weapon from the player's weapon container
        transform.parent = null;

        // Apply force to simulate dropping
        weaponRb.AddForce(transform.forward * dropForce, ForceMode.Impulse);
    }

    // Picking up a weapon
    public virtual void Pickup(Transform parentTransform)
    {
        // Enable the animator when the weapon is equipped
        if (weaponAnimator != null)
            weaponAnimator.enabled = true;

        // Adjust weapon physics
        weaponRb.isKinematic = true;
        weaponRb.useGravity = false;
        weaponCollider.enabled = true;

        // Reparent the weapon to the weapon holder and then reset its position to match
        transform.parent = parentTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
