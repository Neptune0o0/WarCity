using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightConsole : MonoBehaviour
{
    public static FightConsole instance;

    public GameObject player, enemy;

    private FightPlayer fightPlayer;
    private FightEnemy fightEnemy;

    //战斗倒计时
    private float timeCountDown;
    public TextMesh timeTextMesh;

    private void Awake()
    {
        instance = this;

        timeCountDown = 30f;

        Initial();
    }

    private void Update()
    {
        if (timeCountDown > 0)
        {
            timeCountDown -= Time.deltaTime;
            timeTextMesh.text = (int)timeCountDown + "s";
        }
        else
        {
            timeCountDown = 0;
            timeTextMesh.text = timeCountDown + "s";

            FightEnd();
        }
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

    //战斗结束
    public void FightEnd()
    {        
        if (SceneConsole.instance)
        {
            SceneConsole.instance.rolePlayer.roleStruct = fightPlayer.roleStruct;
            SceneConsole.instance.roleEnemy.roleStruct = fightEnemy.roleStruct;

            if (SceneConsole.instance.roleEnemy.roleStruct.hp <= 0)
            {
                SceneConsole.instance.roleEnemy.Die();
            }

            if (SceneConsole.instance.rolePlayer.roleStruct.hp <= 0)
            {
                SceneConsole.instance.rolePlayer.Die();
            }

            SceneConsole.instance.RemoveScene();
        }        
    }
}
