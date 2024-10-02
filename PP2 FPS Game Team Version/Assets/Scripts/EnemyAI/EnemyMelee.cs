using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : BasicEnemyAI
{


    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        stopDistOrig = agent.stoppingDistance;
        angularSpeedOrig = agent.angularSpeed;
        speedOrig = agent.speed;

    }
    //do speed orig in the enemytype
    // Update is called once per frame
    void Update()
    {
    }


    public override void Death()
    {
        Destroy(gameObject);
    }


}
