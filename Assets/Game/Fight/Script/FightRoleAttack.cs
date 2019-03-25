using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightRoleAttack : MonoBehaviour
{
    public FightPlayer fightPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fightPlayer.attackDamage)
        {
            fightPlayer.attackDamage = false;
            FightConsole.instance.AttackInjuryMethod();
        }        
    }
   
}
