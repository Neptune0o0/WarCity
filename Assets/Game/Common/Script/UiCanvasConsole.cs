using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCanvasConsole : MonoBehaviour
{
    public static UiCanvasConsole instance;

    public GameObject playerRolePanel,roleProfessionalPanel, castlePanelPage;

    public Text roleName, id, hp, mp, exp, lv, isActive;

    public GameObject[] playerProfessionalPanel;

    public GameObject buttonTurn, buttonOpenCastlePanel;

    bool castlePanelMove;

    private void Awake()
    {
        instance = this;

        castlePanelMove = false;
        castlePanelPage.SetActive(false);
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

    //打开城堡界面
    public void ButtonOpenCastlePanel()
    {
        if (castlePanelMove == true)
        {
            return;
        }

        RectTransform rectTransform = castlePanelPage.GetComponent<RectTransform>();

        if (castlePanelPage.activeSelf == false)
        {
            castlePanelPage.SetActive(true);
            Tweener tweeners = castlePanelPage.transform.DOLocalMoveX(rectTransform.transform.localPosition.x + rectTransform.rect.width, 1f);
            buttonOpenCastlePanel.transform.SetParent(castlePanelPage.transform);
            tweeners.OnComplete(CastlePanelMoveEnd);
        }
        else
        {
            Tweener tweeners = castlePanelPage.transform.DOLocalMoveX(rectTransform.transform.localPosition.x - rectTransform.rect.width, 1f);
            tweeners.OnComplete(TweenCallback);           
        }

        castlePanelMove = true;
    }

    private void TweenCallback()
    {
        buttonOpenCastlePanel.transform.SetParent(castlePanelPage.transform.parent);
        castlePanelPage.SetActive(false);
        CastlePanelMoveEnd();
    }

    private void CastlePanelMoveEnd()
    {
        castlePanelMove = false;
    }

    public void InterfaceTheRole(Role rolePlayer)
    {
        if (playerRolePanel.activeSelf == false)
        {
            playerRolePanel.SetActive(true);
        }

        InterfaceThePlayerUI(rolePlayer.roleStruct);

        if (rolePlayer.roleType == RoleType.TheEnemyRole)
        {
            roleProfessionalPanel.SetActive(false);
        }
        else
        {
            roleProfessionalPanel.SetActive(true);

            //InterfaceThePlayerSkill(rolePlayer.roleStruct.roleProfessional);
        }
            
        //调用职业相关方法
        switch (rolePlayer.roleStruct.roleProfessional)
        {
            case RoleProfessional.TheFireWarrior:
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

    //回合结束按钮
    public void ButtonEndTheTurn()
    {
        PlayGameConsole.instance.PlayerTurnEnd();

        buttonTurn.SetActive(false);

        if (playerRolePanel.activeSelf == true)
        {
            playerRolePanel.SetActive(false);
        }

        if (castlePanelPage.activeSelf == true)
        {
            castlePanelPage.SetActive(false);
        }
    }

    //敌人回合结束
    public void EnemyEndTheTurn()
    {
        buttonTurn.SetActive(true);
    }

   
}
