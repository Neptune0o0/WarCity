using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGameConsole : MonoBehaviour
{
    public static MapGameConsole instance;

    //当前选中角色物体
    [HideInInspector]
    public Role currentRole;

    //地砖父物体
    public GameObject playGame;
    //用来储存实例砖块
    private List<ItemBrick> brickArray_ItemBrick;
    //玩家与敌人城堡脚本
    private Castle castlePlayer, castleEnemy;

    public GameObject BrickTipMove, BrickTipAttack;
    private List<GameObject> brickTipMove_GameObject;
    private List<GameObject> brickTipAttack_GameObject;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        brickArray_ItemBrick = new List<ItemBrick>();
        brickTipMove_GameObject = new List<GameObject>();
        brickTipAttack_GameObject = new List<GameObject>();

        Castle castle;
        for (int i = 0; i < playGame.transform.childCount; i++)
        {
            brickArray_ItemBrick.Add(playGame.transform.GetChild(i).GetComponent<ItemBrick>());
            
            if (playGame.transform.GetChild(i).GetComponent<Castle>())
            {
                castle = playGame.transform.GetChild(i).GetComponent<Castle>();
                if (castle.castleType == CastleType.ThePlayerCastle)
                {
                    castlePlayer = playGame.transform.GetChild(i).GetComponent<Castle>();
                }
                else if (playGame.transform.GetChild(i).GetComponent<Castle>().castleType == CastleType.TheEnemyCastle)
                {
                    castleEnemy = playGame.transform.GetChild(i).GetComponent<Castle>();
                }              
            }
           
        }
    }

    //判断两个地砖的距离
    public int JudgeDistances(ItemBrick initial, ItemBrick target)
    {
        int xDis, yDis;

        xDis = target.x - initial.x;
        yDis = target.y - initial.y;
        xDis = Mathf.Abs(xDis);
        yDis = Mathf.Abs(yDis);

        return xDis + yDis;
    }

    //显示玩家可移动砖块提示
    public void MoveAtDistance()
    {
        //销毁攻击提示
        for (int i = 0; i < brickTipAttack_GameObject.Count; i++)
        {
            Destroy(brickTipAttack_GameObject[i]);
        }

        //销毁移动提示
        for (int i = 0; i < brickTipMove_GameObject.Count; i++)
        {
            Destroy(brickTipMove_GameObject[i]);
        }

        if (currentRole.roleStruct.active <= 0)
        {
            return;
        }

        int xDis, yDis;

        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            xDis = brickArray_ItemBrick[i].x - currentRole.thisItemBrick.x;
            yDis = brickArray_ItemBrick[i].y - currentRole.thisItemBrick.y;
            xDis = Mathf.Abs(xDis);
            yDis = Mathf.Abs(yDis);

            if ((xDis + yDis) < 4 && (xDis + yDis) > 0)
            {
                if (brickArray_ItemBrick[i].brickType != BrickType.TheWater &&
                    brickArray_ItemBrick[i].brickType != BrickType.TheMountain &&
                    brickArray_ItemBrick[i].rolePlayer == null)
                {
                    GameObject gameObject = Instantiate(BrickTipMove, brickArray_ItemBrick[i].transform.position, Quaternion.identity, brickArray_ItemBrick[i].transform);
                    gameObject.transform.position -= Vector3.forward;
                    brickArray_ItemBrick[i].brickTip = gameObject;
                    brickTipMove_GameObject.Add(gameObject);
                }
            }
        }
    }

    //移动方法
    public void MoveTo(ItemBrick itemBrick,TweenCallback tweenCallback = null)
    {
        currentRole.thisItemBrick.rolePlayer = null;

        currentRole.thisItemBrick = itemBrick;

        itemBrick.rolePlayer = currentRole.gameObject;

        Vector2 vector2 = itemBrick.transform.position;

        Tweener tweeners = currentRole.transform.DOMove(vector2, 1f);

        tweeners.SetEase(Ease.OutQuad);//设置动画的运动曲线，可以选择很多种

        tweeners.OnComplete(tweenCallback);//设置一个事件，就是动画播放完成后执行下一步，可以执行某一个函数。        

        //销毁移动提示
        for (int i = 0; i < brickTipMove_GameObject.Count; i++)
        {
            Destroy(brickTipMove_GameObject[i]);
        }

        currentRole.roleStruct.active -= 1;
    }    

    //攻击提示显示
    public void AttackAtDistance()
    {
        //销毁移动提示
        for (int i = 0; i < brickTipMove_GameObject.Count; i++)
        {
            Destroy(brickTipMove_GameObject[i]);
        }

        //销毁攻击提示
        for (int i = 0; i < brickTipAttack_GameObject.Count; i++)
        {
            Destroy(brickTipAttack_GameObject[i]);
        }

        if (currentRole.roleStruct.active <= 0)
        {
            return;
        }

        int xDis, yDis;

        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            xDis = brickArray_ItemBrick[i].x - currentRole.thisItemBrick.x;
            yDis = brickArray_ItemBrick[i].y - currentRole.thisItemBrick.y;
            xDis = Mathf.Abs(xDis);
            yDis = Mathf.Abs(yDis);

            if ((xDis + yDis) < 2 && (xDis + yDis) > 0)
            {
                GameObject gameObject = Instantiate(BrickTipAttack, brickArray_ItemBrick[i].transform.position, Quaternion.identity, brickArray_ItemBrick[i].transform);
                gameObject.transform.position -= Vector3.forward;
                brickArray_ItemBrick[i].brickTip = gameObject;
                brickTipAttack_GameObject.Add(gameObject);
            }
        }
    }

    //玩家攻击选中敌人
    public void AttackTo(GameObject targetRole, RoleType roleType)
    {
        //切换战斗场景
        SceneConsole.instance.LoadScene(SceneFight.TheGrass);

        //赋值战斗场景 人物属性
        if (roleType == RoleType.ThePlayerRole)
        {
            SceneConsole.instance.rolePlayer = currentRole;
            SceneConsole.instance.roleEnemy = targetRole.GetComponent<Role>();
        }
        else
        {
            SceneConsole.instance.rolePlayer = targetRole.GetComponent<Role>();
            SceneConsole.instance.roleEnemy = currentRole;
        }
       

        //销毁攻击提示
        for (int i = 0; i < brickTipAttack_GameObject.Count; i++)
        {
            Destroy(brickTipAttack_GameObject[i]);
        }

        currentRole.roleStruct.active -= 1;
    }

    //AI移动判断移动的目标点
    public ItemBrick AI_JudgeMoveTarget(Role targetRole)
    {
        int dis = 0;
        int tempdis = 10000;
        int disIndex = 0;

        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            if (brickArray_ItemBrick[i].brickTip != null)
            {
                dis = JudgeDistances(brickArray_ItemBrick[i], targetRole.thisItemBrick);
                if (dis < tempdis)
                {
                    tempdis = dis;
                    disIndex = i;
                }
            }
        }

        return brickArray_ItemBrick[disIndex];
    }
    

}
