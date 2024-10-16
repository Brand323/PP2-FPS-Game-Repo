using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] GameObject arrow;
    [SerializeField] float damageAmount;
    [SerializeField] Animator bowAnimator;
    public Transform arrowTransform;
    public Camera playerCam;
    public float maxAimDist = 100f;

    private bool canAttack = true;
    private bool isDrawingArrow = false;
    private bool bowIsFullyDrawn = false;
    private float drawTime = 1.0f;
    public float attackCooldown = 4.0f;

    private Coroutine drawCoroutine;



    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        weaponName = "Bow";
        

        if (transform.parent != null)
        {
            canAttack = true;
        }
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        HandleBowInput();
    }

    public void HandleBowInput()
    {
        if (!canAttack)
            return;

        // Right click will start drawing the bow
        if (Input.GetMouseButtonDown(1) && !isDrawingArrow)
            StartDrawingBow();
        // If right click is lifted before the bow is fully drawn it will cancel the animation
        if (Input.GetMouseButtonUp(1) && isDrawingArrow && !bowIsFullyDrawn)
            CancelBowDraw();
        // Left click while bow is fully drawn will shoot the arrow
        if (Input.GetMouseButtonDown(0) && bowIsFullyDrawn)
            ShootBow();
    }

    //protected override void DropWeapon()
    //{
    //    if (!isDrawingArrow && !bowIsFullyDrawn)
    //    {
    //        base.DropWeapon();
    //        isEquipped = false;
    //        Debug.Log("Bow dropped.");
    //    }
    //    else
    //    {
    //        Debug.Log("Cannot drop the bow while drawing!");
    //    }

    //}
    public void StartDrawingBow()
    {
        isDrawingArrow = true;
        drawCoroutine = StartCoroutine(DrawBowOverTime());
        bowAnimator.SetBool("IsDrawing", true);
    }

    private IEnumerator DrawBowOverTime()
    {
        yield return new WaitForSeconds(drawTime);

        bowIsFullyDrawn = true;
        isDrawingArrow = false;
        Debug.Log("Bow fully drawn.");
    }

    public void CancelBowDraw()
    {
        // Cancels the Draw Animation
        if (drawCoroutine != null)
        {
            StopCoroutine(drawCoroutine);
        }
        bowIsFullyDrawn = false;
        isDrawingArrow = false;

        // Updates the animation
        bowAnimator.SetBool("IsDrawing", false);
        Debug.Log("Draw canceled.");
    }

    public void ShootBow()
    {
        // Updates the animation
        bowAnimator.SetBool("IsDrawing", false);
        bowAnimator.SetTrigger("Shoot");


        canAttack = false;
        isDrawingArrow = false;
        bowIsFullyDrawn = false;

        // Shoots the arrow
        ShootArrow();

        StartCoroutine(ResetAfterCooldown());
    }

    private IEnumerator ResetAfterCooldown()
    {
        // Cooldown to shoot again
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        bowIsFullyDrawn = false;
        Debug.Log("Bow Attack Ready.");
    }

    protected override string GetWeaponName()
    {
        // Returns the name of the object
        return "Bow";
    }

    void ShootArrow()
    {
        // Aim the arrow towards the target
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 aimPoint;

        // Determine where the player is aiming (either hit point or far maxAimDist)
        if (Physics.Raycast(ray, out hit, maxAimDist))
            aimPoint = hit.point;
        else
            aimPoint = ray.GetPoint(maxAimDist);

        // Instantiate the arrow at the arrowTransform position with no initial rotation
        GameObject firedArrow = Instantiate(arrow, arrowTransform.position, Quaternion.identity);

        // Calculate the direction the arrow should face (from arrowTransform to aimPoint)
        Vector3 direction = (aimPoint - firedArrow.transform.position).normalized;

        // Ensure the arrow looks in the direction of the aim point using Quaternion.LookRotation
        firedArrow.transform.rotation = Quaternion.LookRotation(direction);

        // Pass the damage to the arrow script
        Arrow arrowScript = firedArrow.GetComponent<Arrow>();
        if (arrowScript == null)
        {
            Debug.LogError("Arrow script is missing on the arrow prefab!");
            return;
        }
        arrowScript.damageAmount = damageAmount;

        // Apply an initial force or velocity to the arrow to make it move forward
        Rigidbody arrowRb = firedArrow.GetComponent<Rigidbody>();
        if (arrowRb == null)
        {
            Debug.LogError("Rigidbody is missing on the arrow prefab!");
            return;
        }

        // Apply velocity to the arrow to launch it in the direction it is facing
        arrowRb.velocity = direction * arrowScript.speed;
    }

    public override void Equip()
    {
        isEquipped = true;
        weaponAnimator.enabled = true;
    }

    public override void Unequip()
    {
        isEquipped = false;
        weaponAnimator.enabled = false;
    }
}
