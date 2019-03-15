using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickConsole : MonoBehaviour
{
    //地砖物体
    public GameObject brick_GameObject;

    //地砖行列数量
    public int number_x, number_y;

    //用来临时储存实例出来的砖块
    private List<GameObject> brickArray_GameObject;

    private void Awake()
    {
        brickArray_GameObject = new List<GameObject>();
    }

    /// <summary>
    /// 生成砖块方法
    /// </summary>
    public void GeneratedBricks()
    {
        //清除之前生成的砖块
        if (brickArray_GameObject != null)
        {
            for (int i = 0; i < brickArray_GameObject.Count; i++)
            {
                Destroy(brickArray_GameObject[i]);
            }
            brickArray_GameObject.Clear();
        }

        //实例新的砖块
        Vector3 vector3 = new Vector3();
        for (int i = 0; i < number_x; i++)
        {
            for (int j = 0; j < number_y; j++)
            {
                vector3.x = i * 2;
                vector3.y = j * 2;
                brickArray_GameObject.Add(Instantiate(brick_GameObject, vector3, Quaternion.identity, this.transform));
            }
        }
    }
 
}
