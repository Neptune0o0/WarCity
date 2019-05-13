using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制城堡管理界面UI相关逻辑的脚本 招募相关功能
/// </summary>
public class UiCastlePanel : MonoBehaviour
{
    public Text gold;

    public Text textTheFishMen, textTheBirdMan, textTheEnchanter, textTheMonks, textTheBruiser, textTheBigMan, textTheWindWarrior, textTheWaterWarrior, textTheFireWarrior;

    public Sprite[] imageItem;

    public GameObject roleItem;//角色显示预制

    public GameObject content;//生成角色预制的父物体

    public GameObject[] roleArray;//用于生成的角色预制体数组

    private int roleNumber;

    private List<UiContentRoleItem> uiContentRoleItems;

    // Start is called before the first frame update
    void Start()
    {
        uiContentRoleItems = new List<UiContentRoleItem>();

        gold.text = "当前金币：" + GlobalVariableConsole.instance.playerGold;
        textTheFishMen.text = "鱼人：" + GlobalVariableConsole.instance.valueTheFishMen;
        textTheBirdMan.text = "飞行家：" + GlobalVariableConsole.instance.valueTheBirdMan;
        textTheEnchanter.text = "魔法师：" + GlobalVariableConsole.instance.valueTheEnchanter;
        textTheMonks.text = "僧侣：" + GlobalVariableConsole.instance.valueTheMonks;
        textTheBruiser.text = "格斗家：" + GlobalVariableConsole.instance.valueTheBruiser;
        textTheBigMan.text = "大个：" + GlobalVariableConsole.instance.valueTheBigMan;
        textTheWindWarrior.text = "风战士：" + GlobalVariableConsole.instance.valueTheWindWarrior;
        textTheWaterWarrior.text = "水战士：" + GlobalVariableConsole.instance.valueTheWaterWarrior;
        textTheFireWarrior.text = "火战士：" + GlobalVariableConsole.instance.valueTheFireWarrior;
    }

    //按钮招募方法
    public void ButtonRecruiting(int index)
    {        
        string nameTemp = "";
        bool goldEnoughJudge = false;
        switch (index)
        {
            case 1:
                nameTemp = "鱼人";
                goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheFishMen); break;
            case 2:
                nameTemp = "飞行家"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheBirdMan); break;
            case 3:
                nameTemp = "魔法师"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheEnchanter); break;
            case 4:
                nameTemp = "僧侣"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheMonks); break;
            case 5:
                nameTemp = "格斗家"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheBruiser); break;
            case 6:
                nameTemp = "大个"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheBigMan); break;
            case 7:
                nameTemp = "风战士"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheWindWarrior); break;
            case 8:
                nameTemp = "水战士"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheWaterWarrior); break;
            case 9:
                nameTemp = "火战士"; goldEnoughJudge = DelGold(GlobalVariableConsole.instance.valueTheFireWarrior); break;
            default:
                break;
        }

        if (goldEnoughJudge == true)
        {
            roleNumber++;

            GameObject gameObject = Instantiate(roleItem, content.transform);

            uiContentRoleItems.Add(gameObject.GetComponent<UiContentRoleItem>());
            gameObject.GetComponent<UiContentRoleItem>().Initialized(imageItem[index - 1], nameTemp, roleNumber, ButtonToPlay,(RoleProfessional)index);
        }
       
    }

    //招募判断金币是否足够方法 如果足够减去 招募成功
    private bool DelGold(int cost)
    {
        int temp = GlobalVariableConsole.instance.playerGold;
        temp -= cost;
        if (temp < 0)
        {
            print("金币不足");
            return false;
        }
        else
        {
            GlobalVariableConsole.instance.playerGold -= cost;
        }

        gold.text = "当前金币：" + GlobalVariableConsole.instance.playerGold;
        return true;
    }

    //点击UI上的出战按钮
    private void ButtonToPlay(UiContentRoleItem uiContentRole)
    {
        roleNumber--;

        uiContentRoleItems.Remove(uiContentRole);

        for (int i = 0; i < uiContentRoleItems.Count; i++)
        {
            uiContentRoleItems[i].numberRole.text = "编号：" + (i+1);
        }

        CreateRoleUnit(uiContentRole.role);
    }

    //创建角色在大地图
    public void CreateRoleUnit(RoleProfessional role)
    {
        MapGameConsole.instance.CreateRoleUnit(roleArray[(int)role]);      
    }

    //打开所有城堡出战按钮
    public void OpenButtonToPalay()
    {
        for (int i = 0; i < uiContentRoleItems.Count; i++)
        {
            uiContentRoleItems[i].buttonToPlay.SetActive(true);
        }
    }
}
