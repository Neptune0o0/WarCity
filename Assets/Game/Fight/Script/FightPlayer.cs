using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayer : MonoBehaviour
{
    public RoleStruct roleStruct;

    public Vector2 forceJump;
    //是否在地面上
    private Rigidbody2D rigidbody2d;
    public bool grounded = true;

    public GameObject attackOnject;
    public bool attack = true;
    public bool attackDamage = true;

    private Animator animator;
    private Vector3 vector3ScaleMinus;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        vector3ScaleMinus = new Vector3(-1, 1, 1);

        attackOnject.SetActive(false);
    }

    private void Update()
    {
        //跳跃按键
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (attack)
            {
                Attack();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 5, Input.GetAxis("Vertical") * Time.deltaTime * 5, 0);
        //}

        //移动按键
        if (Input.GetAxis("Horizontal") != 0)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 5, 0, 0);

            //转身
            if (Input.GetAxis("Horizontal") > 0)
            {
                this.transform.localScale = Vector3.one;
            }
            else
            {
                this.transform.localScale = vector3ScaleMinus;
            }
        }


    }

    //这个时候在地面上
    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }

    private void Jump()
    {
        rigidbody2d.AddForce(forceJump, ForceMode2D.Impulse);
        grounded = false;
    }

    private void Attack()
    {
        attackOnject.SetActive(true);

        attack = false;
        animator.Play("RoleAttack");
    }

    private void AttackEnd()
    {
        attackOnject.SetActive(false);

        attack = true;
        attackDamage = true;
    }
}
