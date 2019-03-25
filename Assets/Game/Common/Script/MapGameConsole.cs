using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGameConsole : MonoBehaviour
{
    public static MapGameConsole instance;

    //当前选中角色物体
    [HideInInspector]
    public Role currentRolePlayer;

    //地砖父物体
    public GameObject playGame;
    //用来储存实例砖块
    private List<GameObject> brickArray_GameObject;
    private List<ItemBrick> brickArray_ItemBrick;

    public GameObject BrickTipMove, BrickTipAttack;
    private List<GameObject> brickTipMove_GameObject;
    private List<GameObject> brickTipAttack_GameObject;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        brickArray_GameObject = new List<GameObject>();
        brickArray_ItemBrick = new List<ItemBrick>();
        brickTipMove_GameObject = new List<GameObject>();
        brickTipAttack_GameObject = new List<GameObject>();

        for (int i = 0; i < playGame.transform.childCount; i++)
        {
            brickArray_GameObject.Add(playGame.transform.GetChild(i).gameObject);
            brickArray_ItemBrick.Add(playGame.transform.GetChild(i).GetComponent<ItemBrick>());
        }
    }

    //显示玩家可移动砖块提示
    public void MoveAtDistance()
    {
        //销毁攻击提示
        for (int i = 0; i < brickTipAttack_GameObject.Count; i++)
        {
            Destroy(brickTipAttack_GameObject[i]);
        }

        if (currentRolePlayer.roleStruct.active <= 0)
        {
            return;
        }

        int xDis, yDis;

        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            xDis = brickArray_ItemBrick[i].x - currentRolePlayer.thisItemBrick.x;
            yDis = brickArray_ItemBrick[i].y - currentRolePlayer.thisItemBrick.y;
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
    public void MoveTo(GameObject targetPoint, ItemBrick itemBrick)
    {
        currentRolePlayer.thisItemBrick.rolePlayer = null;

        currentRolePlayer.thisItemBrick = itemBrick;

        itemBrick.rolePlayer = currentRolePlayer.gameObject;

        Vector2 vector2 = targetPoint.transform.position;

        Tweener tweeners = currentRolePlayer.transform.DOMove(vector2, 1f);

        tweeners.SetEase(Ease.OutQuad);//设置动画的运动曲线，可以选择很多种

        tweeners.OnComplete(MoveToEnd);//设置一个事件，就是动画播放完成后执行下一步，可以执行某一个函数。        

        //销毁移动提示
        for (int i = 0; i < brickTipMove_GameObject.Count; i++)
        {
            Destroy(brickTipMove_GameObject[i]);
        }

        currentRolePlayer.roleStruct.active -= 1;
    }

    //移动结束
    public void MoveToEnd()
    {
    }

    //攻击提示显示
    public void AttackAtDistance()
    {
        //销毁移动提示
        for (int i = 0; i < brickTipMove_GameObject.Count; i++)
        {
            Destroy(brickTipMove_GameObject[i]);
        }

        if (currentRolePlayer.roleStruct.active <= 0)
        {
            return;
        }

        int xDis, yDis;

        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            xDis = brickArray_ItemBrick[i].x - currentRolePlayer.thisItemBrick.x;
            yDis = brickArray_ItemBrick[i].y - currentRolePlayer.thisItemBrick.y;
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
    public void AttackTo(GameObject targetEnemy)
    {
        //切换战斗场景
        SceneConsole.instance.LoadScene(SceneFight.TheGrass);

        //赋值战斗场景 人物属性
        SceneConsole.instance.rolePlayer = currentRolePlayer;
        SceneConsole.instance.roleEnemy = targetEnemy.GetComponent<Role>();

        //销毁攻击提示
        for (int i = 0; i < brickTipAttack_GameObject.Count; i++)
        {
            Destroy(brickTipAttack_GameObject[i]);
        }

        currentRolePlayer.roleStruct.active -= 1;
    }
}
