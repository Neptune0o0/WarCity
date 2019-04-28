using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CastleType
{
    ThePlayerCastle,//玩家
    TheEnemyCastle,//敌人
}

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
    ThePlayerRound,
    TheEnemyRound,
}

//角色结构体
[System.Serializable]
public struct RoleStruct
{
    public string roleName;
    public int id, hp, mp, speed, exp, lv, active,attack;
    public bool isActive;
    public int maxHp, maxMp, maxExp;
    //职业
    public RoleProfessional roleProfessional;
}

/// <summary>
/// 角色职业
/// </summary>
public enum RoleProfessional
{
    ThePrince,//王子
    TheFishMen,
    TheBirdMan,
    TheEnchanter,
    TheMonks,
    TheBruiser,
    TheBigMan,
    TheWindWarrior,
    TheWaterWarrior,
    TheFireWarrior,
    TheElementMan,
    TheElementalsWind,
    TheElementalsWater,
    TheElementalsFire,
}


public class PlayGameConsole : MonoBehaviour
{
    public static PlayGameConsole instance;

    public static List<Role> rolesPlayer;
    public static List<Role> rolesEnemy;

    public static PlayState playState;

    private void Awake()
    {
        instance = this;

        playState = PlayState.ThePlayerRound;

        rolesPlayer = new List<Role>();
        rolesEnemy = new List<Role>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playState == PlayState.ThePlayerRound)
        {
            UpdateMouseDown();
        }
        
    }

    //鼠标按下
    private void UpdateMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() )
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
                            switch (itemBrick.rolePlayer.GetComponent<Role>().roleType)
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
                                    MapGameConsole.instance.MoveTo(itemBrick);
                                    break;
                                case BrickTipType.BrickTipAttack:
                                    if (itemBrick.rolePlayer != null && itemBrick.rolePlayer.GetComponent<Role>().roleType == RoleType.TheEnemyRole)
                                    {
                                        print("点击攻击");
                                        MapGameConsole.instance.AttackTo(itemBrick.rolePlayer,RoleType.ThePlayerRole);
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
        Role rolePlayer = gameObject.GetComponent<Role>();
        MapGameConsole.instance.currentRole = rolePlayer;

        //刷新显示玩家UI信息
        UiCanvasConsole.instance.InterfaceTheRole(rolePlayer);
    }

    //点击敌人角色
    private void OnTheEnemyRole(GameObject gameObject)
    {
        ////刷新显示玩家UI信息
        //UiCanvasConsole.instance.InterfaceTheRole(gameObject.GetComponent<RolePlayer>());
    }

    //玩家回合结束
    public void PlayerTurnEnd()
    {
        playState = PlayState.TheEnemyRound;

        for (int i = 0; i < rolesEnemy.Count; i++)
        {
            rolesEnemy[i].roleStruct.active = 2;
        }

        EnemyTurn();
    }

    //敌人回合
    private void EnemyTurn()
    {
        EnemyAIConsole.index = 0;

        EnemyAIConsole.instance.EnemyThinking();       
    }

    //敌人回合结束
    public void EnemyTurnEnd()
    {
        playState = PlayState.ThePlayerRound;

        for (int i = 0; i < rolesPlayer.Count; i++)
        {
            rolesPlayer[i].roleStruct.active = 2;
        }

        UiCanvasConsole.instance.EnemyEndTheTurn();
    }
}
