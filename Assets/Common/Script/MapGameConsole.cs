using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayState
{
    TheNull,
    TheMove,
}

public class MapGameConsole : MonoBehaviour
{
    public static MapGameConsole instance;

    public static PlayState playstate;

    //当前选中角色物体
    [HideInInspector]
    public RolePlayer currentRolePlayer;

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
        playstate = PlayState.TheNull;

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
        int xDis, yDis;

        for (int i = 0; i < brickArray_ItemBrick.Count; i++)
        {
            xDis = brickArray_ItemBrick[i].x - currentRolePlayer.thisItemBrick.x;
            yDis = brickArray_ItemBrick[i].y - currentRolePlayer.thisItemBrick.y;
            xDis = Mathf.Abs(xDis);
            yDis = Mathf.Abs(yDis);

            if ((xDis + yDis) < 3 && (xDis + yDis) > 0)
            {
                if (brickArray_ItemBrick[i].brickType != BrickType.TheWater &&
                    brickArray_ItemBrick[i].brickType != BrickType.TheMountain)
                {
                    GameObject gameObject = Instantiate(BrickTipMove, brickArray_ItemBrick[i].transform.position, Quaternion.identity, brickArray_ItemBrick[i].transform);
                    gameObject.transform.position -= Vector3.forward;
                    brickTipMove_GameObject.Add(gameObject);
                }                
            }
        }

    }

    public void MoveAtAttack()
    {

    }

    public void MoveTo()
    { }
}
