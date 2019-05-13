using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCanvasConsole : MonoBehaviour
{
    public static UiCanvasConsole instance;

    //玩家角色界面 城堡界面
    public GameObject playerRolePanel, castlePanelPage;     

    //结束回合按钮 打开城堡管理界面按钮
    public GameObject buttonTurn, buttonOpenCastlePanel;

    bool castlePanelMove;

    //选中提示预制体 用来生成
    public GameObject roleSelectTip;
    private GameObject roleTempSelectTip;

    //城堡UI界面脚本
    public UiCastlePanel UiCastlePanel;
    
    //城堡界面 初始位置用来控制动画
    private float castleMoveDistance;

    private void Awake()
    {
        instance = this;

        castlePanelMove = false;
        castlePanelPage.SetActive(false);

      
    }

    private void Start()
    {
        RectTransform rectTransform = castlePanelPage.GetComponent<RectTransform>();
        castleMoveDistance = rectTransform.transform.localPosition.x;
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

    //选中角色 显示相关UI的方法
    public void InterfaceTheRole(Role rolePlayer)
    {
        if (playerRolePanel.activeSelf == false)
        {
            playerRolePanel.SetActive(true);
        }

        //刷新UI显示
        UiPlayerRolePanel.instance.InterfaceThePlayerUI(rolePlayer);

        //打开关闭相应按钮技能界面
        UiPlayerRolePanel.instance.ProfessionalPanelSetActive(rolePlayer);

        //移动提示相关
        if (roleTempSelectTip != null)
        {
            Destroy(roleTempSelectTip);
        }
        roleTempSelectTip = Instantiate(roleSelectTip, rolePlayer.transform);
        roleTempSelectTip.transform.position = rolePlayer.transform.position + Vector3.forward;

        ////调用职业相关方法
        //switch (rolePlayer.roleStruct.roleProfessional)
        //{
        //    case RoleProfessional.TheFireWarrior:
        //        RoleButtonRegister_Warrior();
        //        break;
        //    default:
        //        break;
        //}
    }
      

    
    //回合结束按钮
    public void ButtonEndTheTurn()
    {
        PlayGameConsole.instance.PlayerTurnEnd();

        buttonTurn.SetActive(false);
        buttonOpenCastlePanel.SetActive(false);

        //关闭角色界面
        if (playerRolePanel.activeSelf == true)
        {
            playerRolePanel.SetActive(false);
        }

        //关闭城堡界面
        if (castlePanelPage.activeSelf == true)
        {
            RectTransform rectTransform = castlePanelPage.GetComponent<RectTransform>();
            Tweener tweeners = castlePanelPage.transform.DOLocalMoveX(castleMoveDistance, 1f);
            tweeners.OnComplete(TweenCallback);
        }

        //销毁选中提示
        if (roleTempSelectTip != null)
        {
            Destroy(roleTempSelectTip);
        }

        //重置打开所有城堡出战按钮
        UiCastlePanel.OpenButtonToPalay();
    }

    //敌人回合结束
    public void EnemyEndTheTurn()
    {
        buttonTurn.SetActive(true);
        buttonOpenCastlePanel.SetActive(true);
    }

    //改变选中提示的状态
    public void ChangeRoleSelectTip(bool state)
    {
        if (roleTempSelectTip != null)
        {
            roleTempSelectTip.SetActive(state);
        }
    }
}
