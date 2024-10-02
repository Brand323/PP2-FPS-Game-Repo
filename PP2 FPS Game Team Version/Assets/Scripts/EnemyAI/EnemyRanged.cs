using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : BasicEnemyAI
{
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform shootPos;
    [Range(1, 10)][SerializeField] float shootRate;
    [SerializeField] int animSpeedTrans;

    Animator anim;
    bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        stopDistOrig = agent.stoppingDistance;
        angularSpeedOrig = agent.angularSpeed;
        speedOrig = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
