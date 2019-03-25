using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneFight
{
    TheGrass,
    TheWater,
}

public class SceneConsole : MonoBehaviour
{
    public static SceneConsole instance;

    public Role rolePlayer, roleEnemy;

    private void Awake()
    {
        instance = this;               
    }

    public void LoadScene(SceneFight sceneFight)
    {
        switch (sceneFight)
        {
            case SceneFight.TheGrass:
                SceneManager.LoadScene("FightTheGrass", LoadSceneMode.Additive);

                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }

                break;
            case SceneFight.TheWater:
                break;
            default:
                break;
        }

    }
}
