using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ItemBrick))]
public class Inspector : Editor
{
    //private SerializedObject obj; //序列化

    //private SerializedProperty brickType; //定义变量

    //void OnEnable()
    //{
    //    //obj = new SerializedObject(target);

    //    //brickType = obj.FindProperty("brickType");        
    //}

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //GUILayout.Label("This is a Label in a Custom Editor");
        //ItemBrick me = (ItemBrick)target;
        //me.brickType = (BrickType)EditorGUILayout.EnumPopup("type", me.brickType);

        //if (me.brickType == BrickType.TheCastle)
        //{
        //    GUILayout.Button("这是测试按钮！");
        //}
        //else if (me.brickType == BrickType.TheCities)
        //{
        //    GUILayout.Label("这是测试文字！");

            ////EditorGUILayout.PropertyField(brickType);

            ////if (brickType.enumValueIndex == 0)
            ////{
            ////    GUILayout.Button("这是测试按钮！");
            ////}
            ////else if (brickType.enumValueIndex == 1)
            ////{
            ////    GUILayout.Label("这是测试文字！");

        //}
    }

}
