using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightConsole : MonoBehaviour
{
    public static FightConsole instance;

    public GameObject player, enemy;

    private FightPlayer fightPlayer;
    private FightEnemy fightEnemy;

    private void Awake()
    {
        instance = this;

        Initial();
    }

    //初始化
    public void Initial()
    {
        fightPlayer = player.GetComponent<FightPlayer>();
        fightEnemy = enemy.GetComponent<FightEnemy>();
        if (SceneConsole.instance)
        {            
            fightPlayer.roleStruct = SceneConsole.instance.rolePlayer.roleStruct;          
            fightEnemy.roleStruct = SceneConsole.instance.roleEnemy.roleStruct;           
        }
        
    }

    public void AttackInjuryMethod()
    {
        fightEnemy.BeInjured(fightPlayer.roleStruct.attack, player);
    }
}
