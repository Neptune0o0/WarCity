using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightConsole : MonoBehaviour
{
    public static FightConsole instance;

    public GameObject player, enemy;

    public GameObject playerPos;

    private FightPlayer fightPlayer;
    private FightEnemy fightEnemy;

    //战斗倒计时
    private float timeCountDown;
    public TextMesh timeTextMesh;

    public GameObject[] role;

    private void Start()
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
        player = Instantiate(role[(int)SceneConsole.instance.rolePlayer.roleStruct.roleProfessional], playerPos.transform.position, Quaternion.identity);
        player.transform.SetParent(this.transform);

        fightPlayer = player.GetComponent<FightPlayer>();
        fightEnemy = enemy.GetComponent<FightEnemy>();
        if (SceneConsole.instance)
        {
            fightPlayer.roleStruct = SceneConsole.instance.rolePlayer.roleStruct;
            fightEnemy.roleStruct = SceneConsole.instance.roleEnemy.roleStruct;
        }

        FightUIConsole.instance.UpdatePlayerHp((float)fightPlayer.roleStruct.hp/ fightPlayer.roleStruct.maxHp);
        FightUIConsole.instance.UpdateEnemyHp((float)fightEnemy.roleStruct.hp / fightEnemy.roleStruct.maxHp);
    }

    public void AttackInjuryMethod()
    {
        fightEnemy.BeInjured(fightPlayer.roleStruct.attack, player);

        FightUIConsole.instance.UpdateEnemyHp((float)fightEnemy.roleStruct.hp / fightEnemy.roleStruct.maxHp);
    }

    //战斗结束
    public void FightEnd()
    {        
        if (SceneConsole.instance)
        {
            //玩家增加经验
            if (fightPlayer.roleStruct.maxLv > fightPlayer.roleStruct.lv)
            {
                fightPlayer.roleStruct.exp += 10;
                if (fightPlayer.roleStruct.exp >= fightPlayer.roleStruct.maxExp)
                {
                    fightPlayer.roleStruct.lv += 1;
                    print("bingo ！等级提升！ 是否增加相应的属性？？？");
                    if (fightPlayer.roleStruct.lv == fightPlayer.roleStruct.maxLv)
                    {
                        fightPlayer.roleStruct.exp = fightPlayer.roleStruct.maxLv;
                    }
                    else
                    {
                        fightPlayer.roleStruct.exp = 0;
                    }
                    
                   
                }
            }
            


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
