using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BrickType
{
    TheGrass,//草
    TheWater,//水
    TheMountain,//山
}

public class PanelTerrainEditor : MonoBehaviour
{
    //砖块生成脚本
    public BrickConsole brickConsole;

    //输入行列用来生成地图 与实例化物体名字
    public InputField inputField_x, inputField_y, inputField_prefabName;

    public GameObject[] brickGameObjectsArrayUI;
    public GameObject[] brickGameObjectsArray;

    //UI摄像机
    public Camera camera_UI;

    //当前选中的砖块
    private GameObject brickGameObject;
    private Vector3 brickVector3;
    private int brickTypeThis;

    private void Update()
    {
        //跟随鼠标
        if (brickGameObject != null)
        {
            brickVector3 = camera_UI.ScreenToWorldPoint(Input.mousePosition);
            brickVector3.z = 100;
            brickGameObject.transform.position = brickVector3;

            UpdateMouseDown();
        }
    }

    //鼠标按下
    private void UpdateMouseDown()
    {      
        if (Input.GetKey(KeyCode.Mouse0))//左键
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);                  
            if (hit.collider != null)
            {
                Instantiate(brickGameObjectsArray[brickTypeThis], hit.transform.position,Quaternion.identity, hit.transform.parent);
                Destroy(hit.collider.gameObject);                
            }

        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))//右键
        {
            Destroy(brickGameObject);
        }
    }

    //按钮调用生成砖块的方法
    public void ButtonGeneratedBricks()
    {
        if (inputField_x.text != "")
        {
            brickConsole.number_x = int.Parse(inputField_x.text);
        }

        if (inputField_y.text != "")
        {
            brickConsole.number_y = int.Parse(inputField_y.text);
        }

        if (inputField_y.text != "" && inputField_x.text != "")
        {
            brickConsole.GeneratedBricks();
        }
    }

    //储存当前生成砖块预制体
    public void ButtonSaveObject()
    {       
        //输入预制体名字
        string str = inputField_prefabName.text;
        if (inputField_prefabName.text == "")
        {
            str = "DefaultName";
        }
#if UNITY_EDITOR
        //判断路径是否存在预制体 不存在创建
        if (!System.IO.File.Exists("Assets/" + str + ".prefab"))
        {
            PrefabUtility.SaveAsPrefabAsset(brickConsole.gameObject, "Assets/" + str + ".prefab");
        }
#else
        //生成在当前游戏文件夹下
        //Application.dataPath;
#endif
        //AssetDatabase.Refresh();
    }

    //拖动创造新的砖块
    public void ButtonItemBrickCreate(int brickType)
    {
        brickTypeThis = brickType;
        if (brickGameObject == null)
        {
            brickGameObject = Instantiate(brickGameObjectsArrayUI[brickType],this.transform);
        }
        else
        {
            Destroy(brickGameObject);
            brickGameObject = Instantiate(brickGameObjectsArrayUI[brickType], this.transform);
        }
        
    }
}
