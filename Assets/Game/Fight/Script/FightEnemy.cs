using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemy : MonoBehaviour
{
    public RoleStruct roleStruct;

    private Rigidbody2D rigidbody2d;

    public Vector2 forceDamager;
    private Vector2 forceDamagerNegative;

    private bool invincible = false;
    private float invincibleTime;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        forceDamagerNegative = new Vector2(-forceDamager.x, forceDamager.y);
    }

    private void Update()
    {
        if (invincible)
        {
            invincibleTime += Time.deltaTime;

            if (invincibleTime > 1f)
            {
                invincibleTime = 0;
                invincible = false;
            }
        }
    }


    public void BeInjured(int damage, GameObject player)
    {
        if (invincible)
        {
            return;
        }

        roleStruct.hp -= damage;
        

        if (roleStruct.hp <= 0)
        {
            //死亡
        }

        //无敌
        invincible = true;

        //被击退
        if (player.transform.localScale.x == -1)
        {
            rigidbody2d.AddForce(forceDamagerNegative, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody2d.AddForce(forceDamager, ForceMode2D.Impulse);
        }
    }
}
