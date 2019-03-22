using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCanvasConsole : MonoBehaviour
{
    public static UiCanvasConsole instance;

    public GameObject playerRolePanel,roleProfessionalPanel;

    public Text roleName, id, hp, mp, exp, lv, isActive;

    public GameObject[] playerProfessionalPanel;

    private void Awake()
    {
        instance = this;
    }

    public void ButtonMove()
    {
        //显示玩家移动位置UI
        MapGameConsole.instance.MoveAtDistance();
    }

    public void ButtonAttack()
    {
        //显示玩家攻击距离UI
        MapGameConsole.instance.AttackAtDistance();
    }

    public void InterfaceTheRole(RolePlayer rolePlayer)
    {
        if (playerRolePanel.activeSelf == false)
        {
            playerRolePanel.SetActive(true);
        }

        InterfaceThePlayerUI(rolePlayer.roleStruct);

        if (rolePlayer.GetComponent<ItemRoleType>().roleType == RoleType.TheEnemyRole)
        {
            roleProfessionalPanel.SetActive(false);
        }
        else
        {
            roleProfessionalPanel.SetActive(true);

            InterfaceThePlayerSkill(rolePlayer.roleProfessional);
        }
            
        //调用职业相关方法
        switch (rolePlayer.roleProfessional)
        {
            case RoleProfessional.TheWarrior:
                RoleButtonRegister_Warrior();
                break;
            default:
                break;
        }
    }

    private void InterfaceThePlayerUI(RoleStruct roleStruct)
    {
        roleName.text = roleStruct.roleName;
        id.text = roleStruct.id + "";
        hp.text = roleStruct.hp + "";
        mp.text = roleStruct.mp + "";
        exp.text = roleStruct.exp + "";
        lv.text = roleStruct.lv + "";
        isActive.text = roleStruct.isActive ? "可移动" : "不可移动";
    }

    private void InterfaceThePlayerSkill(RoleProfessional roleProfessional)
    {
        for (int i = 0; i < playerProfessionalPanel.Length; i++)
        {
            playerProfessionalPanel[i].SetActive(false);
        }
        playerProfessionalPanel[(int)roleProfessional].SetActive(true);       
    }

    private void RoleButtonRegister_Warrior()
    {

    }
}
