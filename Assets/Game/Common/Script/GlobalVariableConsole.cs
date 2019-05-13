using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableConsole : MonoBehaviour
{
    public static GlobalVariableConsole instance;

    //玩家和敌人的金币总数
    public int playerGold, enemyGold;

    //每个职业招募需要金币
    public int valueTheFishMen, valueTheBirdMan, valueTheEnchanter, valueTheMonks, valueTheBruiser, valueTheBigMan, valueTheWindWarrior, valueTheWaterWarrior, valueTheFireWarrior;

    private void Awake()
    {
        instance = this;
    }    
}
