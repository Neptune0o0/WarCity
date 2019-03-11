using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTerrainEditor : MonoBehaviour
{
    //砖块生成脚本
    public BrickConsole brickConsole;

    //输入行列用来生成地图
    public InputField inputField_x, inputField_y;

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
}
