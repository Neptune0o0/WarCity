using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayer : MonoBehaviour
{
    public RoleStruct roleStruct;

    public RoleProfessional roleProfessional;

    public ItemBrick thisItemBrick;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(this.transform.position, Vector3.forward*100);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && hit[i].collider.name== "BrickImage")
            {
                if (hit[i].collider.transform.parent.GetComponent<ItemBrick>())
                {
                    thisItemBrick = hit[i].collider.transform.parent.GetComponent<ItemBrick>();
                }
            }
        }
       

        switch (roleProfessional)
        {
            case RoleProfessional.TheWarrior:
                this.gameObject.AddComponent<RoleProfessionalTheWarrior>().rolePlayer = this;
                break;
            default:
                break;
        }
    }
}
