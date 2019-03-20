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

public class PlayGameConsole : MonoBehaviour
{
    //地砖父物体
    public GameObject playGame;
    //用来储存实例砖块
    private List<GameObject> brickArray_GameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        brickArray_GameObject = new List<GameObject>();

        for (int i = 0; i < playGame.transform.childCount; i++)
        {
            brickArray_GameObject.Add(playGame.transform.GetChild(i).gameObject);
        }
        
    }

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
        //刷新显示玩家UI信息
        UiCanvasConsole.instance.InterfaceThePlayerRole();
    }

    private void OnTheEnemyRole()
    { }
}
