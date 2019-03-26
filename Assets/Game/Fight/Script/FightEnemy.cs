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

    //private bool flashingDirection;
    //private float flashing = 1f;
    //private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        forceDamagerNegative = new Vector2(-forceDamager.x, forceDamager.y);
    }

    private void Update()
    {
        if (invincible)
        {
            invincibleTime += Time.deltaTime;

            //if (flashingDirection)
            //{
            //    flashing -= Time.deltaTime * 5;
            //    spriteRenderer.color = new Color(1f,1f,1f, flashing);

            //    if (flashing <= 0)
            //    {
            //        flashing = 0;
            //        flashingDirection = false;
            //    }
            //}
            //else
            //{
            //    flashing += Time.deltaTime * 5;
            //    spriteRenderer.color = new Color(1f, 1f, 1f, flashing);

            //    if (flashing >= 1)
            //    {
            //        flashing = 1;
            //        flashingDirection = true;
            //    }
            //}

            if (invincibleTime > 1f)
            {
                invincibleTime = 0;
                invincible = false;

                //spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
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
            Die();

            return;
        }

        ////无敌
        //invincible = true;        

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

    //死亡方法
    private void Die()
    {
        //数据传递到大地图
        FightConsole.instance.FightEnd();       
    }
}
