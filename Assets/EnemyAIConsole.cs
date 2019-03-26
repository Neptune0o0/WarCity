﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIConsole : MonoBehaviour
{
    public static EnemyAIConsole instance;

    int indexMax;
    int index = 0;

    private void Awake()
    {
        instance = this;

    }

    public void EnemyThinking()
    {
        indexMax = PlayGameConsole.rolesEnemy.Count;

        MapGameConsole.instance.currentRole = PlayGameConsole.rolesEnemy[index];

        if (PlayGameConsole.rolesEnemy[index].roleStruct.active <= 0)
        {
            PlayGameConsole.instance.EnemyTurnEnd();
            return;
        }

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