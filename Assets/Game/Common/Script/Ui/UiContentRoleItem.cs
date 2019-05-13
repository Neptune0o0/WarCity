using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiContentRoleItem : MonoBehaviour
{
    public Image imageRole;

    public Text numberRole, professionalRole;

    public GameObject buttonToPlay;

    public delegate void ButtonDelegate(UiContentRoleItem uiContentRole);
    public event ButtonDelegate ButtonEvent;

    [HideInInspector]
    public RoleProfessional role;

    public void Initialized(Sprite image, string professional, int roleNumber, ButtonDelegate action, RoleProfessional roleProfessional)
    {
        imageRole.sprite = image;

        numberRole.text = "编号：" + roleNumber;

        professionalRole.text = "职业：" + professional;

        ButtonEvent = action;

        role = roleProfessional;

        buttonToPlay.SetActive(false);
    }

    public void ButtonToPlay()
    {
        //判断是否可以出城（城堡上是否有玩家脚色）
        if (MapGameConsole.instance.JudgeCastleHaveUnit(CastleType.ThePlayerCastle))
        {
            print("城堡上有单位");
            return;
        }

        ButtonEvent(this);

        Destroy(this.gameObject);       
    }
}
