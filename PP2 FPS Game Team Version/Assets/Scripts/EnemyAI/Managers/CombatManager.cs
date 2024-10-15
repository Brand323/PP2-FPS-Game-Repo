using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    [Range(1, 4)][SerializeField] int difficulty;
    [Range(1, 8)][SerializeField] int attackingPlayerMax;
    public int attackingPlayerCurr;
    public int enemiesExisting;
    public int enemyArmySize;
    public int playerArmySize;

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

    public int GetAttackingPlayerMax()
    {
        return attackingPlayerMax;
    }
    public void SetDifficulty(int diff)
    {
        difficulty = diff;
        attackingPlayerMax = difficulty + 1;
    }
    public void SetCombatLogic(int enemyCount, int companionCount)
    {

    }
}