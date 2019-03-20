using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCanvasConsole : MonoBehaviour
{
    public static UiCanvasConsole instance;

    public GameObject playerRolePanel;

    private void Awake()
    {
        instance = this;
    }
  
    public void InterfaceThePlayerRole()
    { }
}
