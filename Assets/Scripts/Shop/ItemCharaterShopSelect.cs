using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCharaterShopSelect : MonoBehaviour
{
    public Button btnSelected;
    [SerializeField] private Button btnBuy;

    [SerializeField] private Text txtCoin, txtSelector;

    public void SetCoin(int coinsell)
    {
        txtCoin.text = coinsell.ToString();
    }

    public void SetSelector(bool isSelected)
    {
        txtSelector.text = !isSelected ? "Select" : "Selected";
    }

    public void SetActiveBuy(bool isShow = false)
    {
        btnBuy.gameObject.SetActive(isShow);
        btnSelected.gameObject.SetActive(!isShow);
    }
}