using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RoleType
{
    ThePlayerRole,//玩家角色
    TheEnemyRole,//敌人角色
}

public enum BrickType
{
    TheGrass,//草
    TheWater,//水
    TheMountain,//山
    TheCities,//城镇
    TheMagicCircle,//魔法阵
    TheCastle,//城堡   
}

public enum BrickTipType
{
    BrickTipMove,//移动
    BrickTipAttack,//攻击
}

//角色结构体
[System.Serializable]
public struct RoleStruct
{
    public string roleName;
    public int id, hp, mp, exp, lv,active;
    public bool isActive;
}

/// <summary>
/// 角色职业
/// </summary>
public enum RoleProfessional
{
    TheWarrior,//战士
}

public class PlayGameConsole : MonoBehaviour
{   
    //当前选中角色物体
    private GameObject currentRolaObject;
   
    // Update is called once per frame
    void Update()
    {
        UpdateMouseDown();
    }


    //鼠标按下
    private void UpdateMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetKey(KeyCode.Mouse0))//左键
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<ItemRoleType>()) //点击角色
                    {
                        //选中当前角色物体
                        currentRolaObject = hit.collider.gameObject;                        

                        switch (hit.collider.GetComponent<ItemRoleType>().roleType)
                        {
                            case RoleType.ThePlayerRole:
                                OnThePlayerRole();
                                break;
                            case RoleType.TheEnemyRole:
                                OnTheEnemyRole();
                                break;
                            default:
                                break;
                        }
                    }
                    else if (hit.collider.GetComponent<ItemBrick>())//点击地形
                    {

                    }
                    else if (hit.collider.GetComponent<ItemBrickTip>())
                    {
                        
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))//右键
            {

            }
        }

    }

    //点击玩家角色
    private void OnThePlayerRole()
    {
        RolePlayer rolePlayer = currentRolaObject.GetComponent<RolePlayer>();
        MapGameConsole.instance.currentRolePlayer = rolePlayer;

        //刷新显示玩家UI信息
        UiCanvasConsole.instance.InterfaceThePlayerRole(rolePlayer);
    }

    private void OnTheEnemyRole()
    { }
  
}
