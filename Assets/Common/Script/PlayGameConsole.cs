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

public enum PlayState
{
    TheNull,
    TheMove,
    TheAttck,
}

//角色结构体
[System.Serializable]
public struct RoleStruct
{
    public string roleName;
    public int id, hp, mp, speed, exp, lv, active;
    public bool isActive;
}

/// <summary>
/// 角色职业
/// </summary>
public enum RoleProfessional
{
    TheWarrior,//战士
}


public class PlayStateClass
{
    public static PlayState playstate;
}

public class PlayGameConsole : MonoBehaviour
{
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
            if (Input.GetKeyDown(KeyCode.Mouse0))//左键
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.transform.parent.GetComponent<ItemBrick>())
                    {
                        ItemBrick itemBrick = hit.transform.parent.GetComponent<ItemBrick>();

                        if (itemBrick.rolePlayer != null && itemBrick.brickTip == null)//点击角色
                        {
                            switch (itemBrick.rolePlayer.GetComponent<ItemRoleType>().roleType)
                            {
                                case RoleType.ThePlayerRole:
                                    OnThePlayerRole(itemBrick.rolePlayer);
                                    print("点击玩家角色：" + itemBrick.rolePlayer.name);
                                    break;
                                case RoleType.TheEnemyRole:
                                    OnTheEnemyRole(itemBrick.rolePlayer);
                                    print("点击敌人角色：" + itemBrick.rolePlayer.name);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (itemBrick.rolePlayer == null && itemBrick.brickTip == null)//点击地形
                        {
                            print("点击：" + itemBrick.brickType);
                        }
                        else if (itemBrick.brickTip != null)//点击移动或者攻击
                        {
                            switch (itemBrick.brickTip.GetComponent<ItemBrickTip>().brickTipType)
                            {
                                case BrickTipType.BrickTipMove:
                                    print("点击移动");
                                    MapGameConsole.instance.MoveTo(hit.collider.gameObject, itemBrick);
                                    break;
                                case BrickTipType.BrickTipAttack:
                                    if (itemBrick.rolePlayer != null && itemBrick.rolePlayer.GetComponent<ItemRoleType>().roleType == RoleType.TheEnemyRole)
                                    {
                                        print("点击攻击");
                                        MapGameConsole.instance.AttackTo(itemBrick.rolePlayer);
                                    }                             
                                    break;
                                default:
                                    break;
                            }                            
                        }                       
                    }                   
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))//右键
            {

            }
        }

    }

    //点击玩家角色
    private void OnThePlayerRole(GameObject gameObject)
    {
        RolePlayer rolePlayer = gameObject.GetComponent<RolePlayer>();
        MapGameConsole.instance.currentRolePlayer = rolePlayer;

        //刷新显示玩家UI信息
        UiCanvasConsole.instance.InterfaceTheRole(rolePlayer);
    }

    //点击敌人角色
    private void OnTheEnemyRole(GameObject gameObject)
    {
        ////刷新显示玩家UI信息
        //UiCanvasConsole.instance.InterfaceTheRole(gameObject.GetComponent<RolePlayer>());
    }

}
