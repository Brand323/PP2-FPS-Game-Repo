using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotSpeed;
    [SerializeField] float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        target = gameManager.instance.player.transform;
        rb = GetComponent<Rigidbody>();
        adjustForDifficulty();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - rb.position;
        Vector3 rot = Vector3.Cross(transform.forward, dir);
        rb.angularVelocity = rot * rotSpeed;
        rb.velocity = transform.forward * speed;
    }
    public void adjustForDifficulty()
    {
        speed += CombatManager.instance.GetDifficulty()/2;
    }
    public void SetTarget(Transform targetNew)
    {
        target = targetNew;
    }
}
