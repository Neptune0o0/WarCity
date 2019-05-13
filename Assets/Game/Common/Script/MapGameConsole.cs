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

    private GameObject sceneConsoleObject;//场景总的父物体
  
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

        sceneConsoleObject = SceneConsole.instance.gameObject;
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

    //判断两个地砖的距离 A*算法
    public int JudgeDistancesAStar(ItemBrick initial, ItemBrick target)
    {
        List<ItemBrick> itemBricks = new List<ItemBrick>();

        for (int i = 0; i < 100; i++)
        {
            if (i == 0)
            {
                itemBricks = JudgeAroundBrick(initial);

                for (int j = 0; j < itemBricks.Count; j++)
                {
                    itemBricks[j].dis = 1;
                }
            }
            else
            {
                List<ItemBrick> itemsTemp = new List<ItemBrick>();
                for (int j = 0; j < itemBricks.Count; j++)
                {
                    itemsTemp.AddRange(JudgeAroundBrick(itemBricks[j]));
                }

                for (int j = 0; j < itemsTemp.Count; j++)
                {
                    itemsTemp[j].dis = i + 1;
                }
                itemBricks.AddRange(itemsTemp);
            }

            for (int j = 0; j < itemBricks.Count; j++)
            {
                if (itemBricks[j] == target)
                {
                    return itemBricks[j].dis;
                }
            }
        }


        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            brickArray_ItemBrick[i].isTag = false;
            brickArray_ItemBrick[i].dis = 0;
        }

        return 0;
    }

    //销毁相关移动 或者 攻击 提示
    public void DestroyBrickTip(bool move = true, bool attack = true)
    {
        if (attack == true)
        {
            //销毁攻击提示
            for (int i = 0; i < brickTipAttack_GameObject.Count; i++)
            {
                Destroy(brickTipAttack_GameObject[i]);
            }
        }

        if (move == true)
        {
            //销毁移动提示
            for (int i = 0; i < brickTipMove_GameObject.Count; i++)
            {
                Destroy(brickTipMove_GameObject[i]);
            }
        }

    }

    //显示玩家可移动砖块提示
    public void MoveAtDistance()
    {
        //销毁相关提示
        DestroyBrickTip();

        if (currentRole.roleStruct.active <= 0)
        {
            return;
        }

        List<ItemBrick> itemBricks = new List<ItemBrick>();

        for (int i = 0; i < currentRole.roleStruct.speed; i++)
        {
            if (i == 0)
            {
                itemBricks = JudgeAroundBrick(currentRole.thisItemBrick);
            }
            else
            {
                List<ItemBrick> itemsTemp = new List<ItemBrick>();
                for (int j = 0; j < itemBricks.Count; j++)
                {
                    itemsTemp.AddRange(JudgeAroundBrick(itemBricks[j]));
                }
                itemBricks.AddRange(itemsTemp);
            }
        }

        //实例化移动提示
        for (int i = 0; i < itemBricks.Count; i++)
        {
            GameObject gameObject = Instantiate(BrickTipMove, itemBricks[i].transform.position, Quaternion.identity, itemBricks[i].transform);
            gameObject.transform.position -= Vector3.forward;
            itemBricks[i].brickTip = gameObject;
            brickTipMove_GameObject.Add(gameObject);
        }

        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            brickArray_ItemBrick[i].isTag = false;
        }
    }

    //移动方法
    public void MoveTo(ItemBrick itemBrick, TweenCallback tweenCallback = null)
    {
        currentRole.thisItemBrick.rolePlayer = null;

        currentRole.thisItemBrick = itemBrick;

        itemBrick.rolePlayer = currentRole.gameObject;

        Vector2 vector2 = itemBrick.transform.position;

        Tweener tweeners = currentRole.transform.DOMove(vector2, 1f);

        tweeners.SetEase(Ease.OutQuad);//设置动画的运动曲线，可以选择很多种

        tweeners.OnComplete(tweenCallback);//设置一个事件，就是动画播放完成后执行下一步，可以执行某一个函数。        

        DestroyBrickTip(true, false);

        currentRole.roleStruct.active -= 1;

        UiCanvasConsole.instance.ChangeRoleSelectTip(false);

        if (currentRole.roleType == RoleType.ThePlayerRole)
        {
            //刷新UI显示
            UiPlayerRolePanel.instance.InterfaceThePlayerUI(currentRole);
        }
       
    }

    //攻击提示显示
    public void AttackAtDistance()
    {
        DestroyBrickTip();

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

    //攻击选中敌人
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

        DestroyBrickTip(false, true);

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

    //判断周围上下左右的可移动地砖
    public List<ItemBrick> JudgeAroundBrick(ItemBrick itemBrick)
    {
        List<ItemBrick> itemBricks = new List<ItemBrick>();

        itemBrick.isTag = true;

        Vector2[] vector2s = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < brickArray_ItemBrick.Count; i++)
            {
                if (brickArray_ItemBrick[i].isTag == true)
                {
                    continue;
                }
                if (brickArray_ItemBrick[i].x == itemBrick.x + vector2s[j].x &&
                    brickArray_ItemBrick[i].y == itemBrick.y + vector2s[j].y)
                {
                    brickArray_ItemBrick[i].isTag = true;

                    if (((brickArray_ItemBrick[i].brickType != BrickType.TheWater &&
                    brickArray_ItemBrick[i].brickType != BrickType.TheMountain &&
                    brickArray_ItemBrick[i].rolePlayer == null))
                    || (brickArray_ItemBrick[i].brickType == BrickType.TheWater && currentRole.roleStruct.roleProfessional == RoleProfessional.TheFishMen)
                    || (brickArray_ItemBrick[i].brickType == BrickType.TheMountain && currentRole.roleStruct.roleProfessional == RoleProfessional.TheBirdMan))
                    {
                        itemBricks.Add(brickArray_ItemBrick[i]);
                    }
                }
            }
        }

        return itemBricks;
    }

    //判断城堡当前是否有单位存在 false没有 true有
    public bool JudgeCastleHaveUnit(CastleType castleType)
    {
        if (castleType == CastleType.ThePlayerCastle)
        {
            if (castlePlayer.itemBrick.rolePlayer == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (castleType == CastleType.TheEnemyCastle)
        {
            if (castleEnemy.itemBrick.rolePlayer == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    //在城堡位置创建角色
    public void CreateRoleUnit(GameObject role)
    {
        GameObject gameObject = Instantiate(role, castlePlayer.transform.position + Vector3.back * 2, Quaternion.identity);

        gameObject.transform.SetParent(sceneConsoleObject.transform);
    }

}
