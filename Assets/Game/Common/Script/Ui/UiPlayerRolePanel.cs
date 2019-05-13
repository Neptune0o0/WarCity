using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerRolePanel : MonoBehaviour
{
    public static UiPlayerRolePanel instance;

    public Text roleName, hp, mp, exp, lv, isActive;

    public Image image_hp, image_mp, image_active, image_exp, image_brick;

    public GameObject roleProfessionalPanel;

    //public GameObject[] playerProfessionalPanel;

    private void Awake()
    {
        instance = this;
    }

    public void ProfessionalPanelSetActive(Role rolePlayer)
    {
        if (rolePlayer.roleType == RoleType.TheEnemyRole)
        {
            roleProfessionalPanel.SetActive(false);
        }
        else
        {
            roleProfessionalPanel.SetActive(true);

            //InterfaceThePlayerSkill(rolePlayer.roleStruct.roleProfessional);
        }

    }

    public void InterfaceThePlayerUI(Role role)
    {
        RoleStruct roleStruct = role.roleStruct;
        roleName.text = roleStruct.roleName;        
        hp.text = roleStruct.hp + "";
        mp.text = roleStruct.mp + "";
        exp.text = roleStruct.exp + "";
        lv.text = roleStruct.lv + "";
        isActive.text = roleStruct.active+"";

        image_hp.fillAmount = (float)roleStruct.hp / roleStruct.maxHp;
        image_mp.fillAmount = (float)roleStruct.mp / roleStruct.maxMp;
        image_exp.fillAmount = (float)roleStruct.exp / roleStruct.maxExp;
        image_active.fillAmount = (float)roleStruct.active / 2;
     
        image_brick.sprite = role.thisItemBrick.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    //private void InterfaceThePlayerSkill(RoleProfessional roleProfessional)
    //{
    //    for (int i = 0; i < playerProfessionalPanel.Length; i++)
    //    {
    //        playerProfessionalPanel[i].SetActive(false);
    //    }
    //    playerProfessionalPanel[(int)roleProfessional].SetActive(true);
    //}

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
}
