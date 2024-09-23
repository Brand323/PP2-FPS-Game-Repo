using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    [SerializeField] int difficulty;
    [SerializeField] int attackingPlayerMax;
    public int attackingPlayerCurr;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (difficulty <= 0)
        {
            difficulty = 1;
        }
        if (attackingPlayerMax == 0)
        {
            attackingPlayerMax = difficulty + 1;
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetDifficulty()
    {
        return difficulty;
    }

    public int GetAttackingPlayerMax() {
        return attackingPlayerMax;
    }

}