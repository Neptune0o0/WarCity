using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayer : MonoBehaviour
{
    public RoleStruct roleStruct;

    //职业
    public RoleProfessional roleProfessional;

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
        switch (roleProfessional)
        {
            case RoleProfessional.TheWarrior:
                this.gameObject.AddComponent<RoleProfessionalTheWarrior>().rolePlayer = this;
                break;
            default:
                break;
        }
    }
}
