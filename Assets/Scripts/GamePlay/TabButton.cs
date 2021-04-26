using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    public GameObject charGo;
    public GameObject itemGo;

    private void Start()
    {
        charGo.SetActive(true);
        itemGo.SetActive(false);
    }
    public void OnClickBtnChar()
    {
        charGo.SetActive(true);
        itemGo.SetActive(false);
    }

    public void OnClickBtnItem()
    {
        charGo.SetActive(false);
        itemGo.SetActive(true);
    }
}
