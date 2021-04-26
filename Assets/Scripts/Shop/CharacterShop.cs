using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour
{
    [SerializeField] private ItemCharaterShopSelect[] characterItem;
    public Text txtHighCoins;
    public Text txtNoty;
    public GameObject goNoty;
    private readonly int[] coinBuy = new int[] { 0, 1000, 2000, 3000, 4000, 5000 };
    public GameObject popUpShop;
    private int indexSelected;

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("body_0"))
        {
            PlayerPrefs.SetInt("body_0", 1);
        }
        indexSelected = PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.CHARACTER_SELECTED.ToString(), 0);
        StartCoroutine(Reload());
    }

    private void OnDisable()
    {
        GameManager.instance.SelectCharacter(indexSelected);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < characterItem.Length; i++)
        {
            characterItem[i].SetCoin(coinBuy[i]);
            characterItem[i].SetActiveBuy(PlayerPrefs.GetInt("body_" + i, 0) <= 0);
            int t = i;
            characterItem[i].btnSelected.onClick.AddListener(() => ClickButtonSelect(t));
        }
        txtHighCoins.text = UIManager.Instance.GetCoin().ToString();
        ClickButtonSelect(indexSelected);
    }

    private void ClickButtonSelect(int t)
    {
        for (int i = 0; i < characterItem.Length; i++)
        {
            characterItem[i].SetSelector(i == t);
            if (t != indexSelected)
            {
                indexSelected = t;
            }
        }
    }

    public void ClicBuy(int index)
    {
        if (UIManager.Instance.GetCoin() >= coinBuy[index])
        {
            UIManager.Instance.AddCoin(-coinBuy[index]);
            txtHighCoins.text = UIManager.Instance.GetCoin().ToString();
            goNoty.SetActive(true);
            txtNoty.text = "Successfull!";
            characterItem[index].SetActiveBuy(false);
            PlayerPrefs.SetInt("body_" + index, 1);
            PlayerPrefs.Save();
        }
        else
        {
            goNoty.SetActive(true);
            txtNoty.text = "<color=#FF0400>Failed!</color>";
        }
    }

    public void OnShop()
    {
        if (popUpShop.gameObject.activeSelf == false)
        {
            popUpShop.gameObject.SetActive(true);
        }
        else
        {
            popUpShop.gameObject.SetActive(false);
        }
    }

    public void BtnBack()
    {
        goNoty.SetActive(false);
        gameObject.SetActive(false);
    }
}