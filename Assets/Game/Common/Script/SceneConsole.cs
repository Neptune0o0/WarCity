using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneFight
{
    TheGrass,
    TheWater,
}

/// <summary>
/// 控制场景加载相关的脚本
/// </summary>
public class SceneConsole : MonoBehaviour
{
    public static SceneConsole instance;

    public Role rolePlayer, roleEnemy;

    public static string currentSceneName;

    private void Awake()
    {
        instance = this;               
    }

    public void LoadScene(SceneFight sceneFight)
    {
        switch (sceneFight)
        {
            case SceneFight.TheGrass:
                currentSceneName = "FightTheGrass";               
                break;
            case SceneFight.TheWater:
                break;
            default:
                break;
        }

        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Additive);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    public void RemoveScene()
    {
        SceneManager.UnloadSceneAsync(currentSceneName);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        //敌人回合战斗结束 
        if (PlayGameConsole.playState == PlayState.TheEnemyRound)
        {
            EnemyAIConsole.instance.AttackEnd();
        }
        //玩家回合战斗结束
        else
        {
            //刷新UI显示
            UiPlayerRolePanel.instance.InterfaceThePlayerUI(rolePlayer);
        }
    }
}
