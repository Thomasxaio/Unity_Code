using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Show_Btn : MonoBehaviour 
{
    public GameObject Hide_Show_Clicked;

    public void HideShow()
    {
        if(!Hide_Show_Clicked.activeInHierarchy)
        { Hide_Show_Clicked.SetActive(true); }
        else
        { Hide_Show_Clicked.SetActive(false); }
    }
}
