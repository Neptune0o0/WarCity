using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    public RoleType roleType;

    public RoleStruct roleStruct;  

    [HideInInspector]
    public ItemBrick thisItemBrick;

    // Start is called before the first frame update
    void Start()
    {
        //初始化给当前地砖赋值
        RaycastHit2D[] hit = Physics2D.RaycastAll(this.transform.position, Vector3.forward*100);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && hit[i].collider.name== "BrickImage")
            {
                if (hit[i].collider.transform.parent.GetComponent<ItemBrick>())
                {
                    thisItemBrick = hit[i].collider.transform.parent.GetComponent<ItemBrick>();
                    thisItemBrick.rolePlayer = this.gameObject;
                }
            }
        }

        //初始化根据职业赋值相应职业脚本
        switch (roleStruct.roleProfessional)
        {
            case RoleProfessional.TheFireWarrior:
                this.gameObject.AddComponent<RoleTheFireWarrior>().rolePlayer = this;
                break;
            case RoleProfessional.ThePrince:
                break;
            case RoleProfessional.TheFishMen:
                break;
            case RoleProfessional.TheBirdMan:
                break;
            case RoleProfessional.TheEnchanter:
                break;
            case RoleProfessional.TheMonks:
                break;
            case RoleProfessional.TheBruiser:
                break;
            case RoleProfessional.TheBigMan:
                break;
            case RoleProfessional.TheWindWarrior:
                this.gameObject.AddComponent<RoleTheWindWarrior>().rolePlayer = this;                
                break;
            case RoleProfessional.TheWaterWarrior:
                this.gameObject.AddComponent<RoleTheWaterWarrior>().rolePlayer = this;
                break;
            default:
                break;
        }

        //初始化在注册在list中
        if (roleType == RoleType.TheEnemyRole)
        {
            PlayGameConsole.rolesEnemy.Add(this);
        }
        else
        {
            PlayGameConsole.rolesPlayer.Add(this);
        }

    }

    //大地图下角色死亡
    public void Die()
    {
        Destroy(this.gameObject);

        if (roleType == RoleType.TheEnemyRole)
        {
            PlayGameConsole.rolesEnemy.Remove(this);
        }
        else
        {
            PlayGameConsole.rolesPlayer.Remove(this);
        }
    }
}
