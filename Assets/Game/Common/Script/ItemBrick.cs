﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBrick : MonoBehaviour
{
    //坐标
    public int x, y;

    //当前地砖类型
    public BrickType brickType;

    //当前地砖的单位物体 和提示物体
    public GameObject rolePlayer,brickTip;

    //是否被标记查找
    public bool isTag;

    public int dis;
}


