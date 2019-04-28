using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUIConsole : MonoBehaviour
{
    public static FightUIConsole instance;

    public Image playerHp, enemyHp;

    private void Awake()
    {
        instance = this;
    }

    public void UpdatePlayerHp(float fill)
    {
        playerHp.fillAmount = fill;
    }

    public void UpdateEnemyHp(float fill)
    {
        enemyHp.fillAmount = fill;
    }
}
