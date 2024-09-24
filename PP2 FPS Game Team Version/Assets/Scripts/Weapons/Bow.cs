using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public Transform arrowTransform;
    public Camera playerCam;
    public float maxAimDist = 100f;

    private Animator bowAnimator;
    private bool isEquipped = false;
    private bool canAttack = true;
    private bool isDrawingArrow = false;
    private bool bowIsFullyDrawn = false;
    private bool bowShot = false;
    private float drawTime = 1.0f;
    public float attackCooldown = 4.0f;

    private Coroutine drawCoroutine;



    // Start is called before the first frame update
    void Start()
    {
        weaponName = "Bow";
        bowAnimator = GetComponent<Animator>();
        

        if (transform.parent != null)
        {
            isEquipped = true;
            canAttack = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBowInput();
        UpdateArrowAim();
    }

    public void HandleBowInput()
    {
        if (!canAttack)
            return;

        if (Input.GetMouseButtonDown(1) && !isDrawingArrow)
            StartDrawingBow();

        if (Input.GetMouseButtonUp(1) && isDrawingArrow && !bowIsFullyDrawn)
            CancelBowDraw();

        if (Input.GetMouseButtonDown(0) && bowIsFullyDrawn)
            ShootBow();
    }

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
        if (drawCoroutine != null)
        {
            StopCoroutine(drawCoroutine);
        }
        bowIsFullyDrawn = false;
        isDrawingArrow = false;
        bowAnimator.SetBool("IsDrawing", false);
        Debug.Log("Draw canceled.");
    }

    public void ShootBow()
    {
        bowAnimator.SetBool("IsDrawing", false);
        bowAnimator.SetTrigger("Shoot");
        bowShot = true;
        canAttack = false;
        isDrawingArrow = false;
        bowIsFullyDrawn = false;

        StartCoroutine(ResetAfterCooldown());
    }

    private IEnumerator ResetAfterCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        bowIsFullyDrawn = false;
        Debug.Log("Bow Attack Ready.");
    }

    private void UpdateArrowAim()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 aimPoint;

        if (Physics.Raycast(ray, out hit, maxAimDist))
            aimPoint = hit.point;
        else
            aimPoint = ray.GetPoint(maxAimDist);

        arrowTransform.LookAt(aimPoint);
    }
}
