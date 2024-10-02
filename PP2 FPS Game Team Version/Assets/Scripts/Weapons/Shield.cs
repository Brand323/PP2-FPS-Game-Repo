using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    protected Animator shieldAnimator;
    public bool isEquipped;

    void Awake()
    {
        shieldAnimator = GetComponent<Animator>();

        if (shieldAnimator != null)
            shieldAnimator.enabled = true;
    }

    public void TriggerBlock(bool isBlocking)
    {
        shieldAnimator.SetBool("IsBlocking", isBlocking);
    }


    public void Equip()
    {
        isEquipped = true;
        gameObject.SetActive(true);
        shieldAnimator.enabled = true;
    }

    public void Unequip()
    {
        isEquipped = false;
        gameObject.SetActive(false);
        shieldAnimator.enabled = false;
    }
}
