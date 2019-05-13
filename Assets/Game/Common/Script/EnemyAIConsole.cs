using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制敌人行动规则的脚本
/// </summary>
public class EnemyAIConsole : MonoBehaviour
{
    public static EnemyAIConsole instance;

    int indexMax;
    public static int index = 0;

    private void Awake()
    {
        instance = this;

    }

    //敌人思考方法
    public void EnemyThinking()
    {
        indexMax = PlayGameConsole.rolesEnemy.Count;

        if (indexMax <=0)
        {
            PlayGameConsole.instance.EnemyTurnEnd();
            return;
        }

        if (PlayGameConsole.rolesEnemy[index].roleStruct.active <= 0)
        {
            index++;
            if (index >= indexMax)
            {
                PlayGameConsole.instance.EnemyTurnEnd();
                return;
            }
        }

        MapGameConsole.instance.currentRole = PlayGameConsole.rolesEnemy[index];

        //判断是否可以直接攻击
        if (MapGameConsole.instance.JudgeDistances(JudgeRecently(PlayGameConsole.rolesEnemy[index]).thisItemBrick,
            PlayGameConsole.rolesEnemy[index].thisItemBrick) == 1)
        {
            StartCoroutine(Attack());
        }
        else
        {
            StartCoroutine(Move());
        }

    }

    private IEnumerator Move()
    {
        MapGameConsole.instance.MoveAtDistance();

        yield return new WaitForSeconds(1f);

        Role role = JudgeRecently(PlayGameConsole.rolesEnemy[index]);

        ItemBrick itemBrick = MapGameConsole.instance.AI_JudgeMoveTarget(role);

        MapGameConsole.instance.MoveTo(itemBrick, MoveEnd);
    }

    private void MoveEnd()
    {
        EnemyThinking();
    }

    private IEnumerator Attack()
    {
        MapGameConsole.instance.AttackAtDistance();

        yield return new WaitForSeconds(1f);

        Role role = JudgeRecently(PlayGameConsole.rolesEnemy[index]);

        ItemBrick itemBrick = MapGameConsole.instance.AI_JudgeMoveTarget(role);

        MapGameConsole.instance.AttackTo(itemBrick.rolePlayer, RoleType.TheEnemyRole);
    }

    public void AttackEnd()
    {
        EnemyThinking();
    }

    //判断距离敌人角色最近的玩家单位
    private Role JudgeRecently(Role role)
    {
        int dis = 0;
        int tempdis = 10000;
        int disIndex = 0;
        //判断距离最近的玩家
        for (int i = 0; i < PlayGameConsole.rolesPlayer.Count; i++)
        {
            dis = MapGameConsole.instance.JudgeDistances(role.thisItemBrick,
                PlayGameConsole.rolesPlayer[i].thisItemBrick);

            if (dis < tempdis)
            {
                tempdis = dis;
                disIndex = i;
            }
        }

        return PlayGameConsole.rolesPlayer[disIndex];
    }

   
}
