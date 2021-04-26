using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    private readonly int itemHP = 100;
    private readonly int itemShoes = 150;
    private readonly int itemMagnet = 200;

    public Text txtHighCoins;
    public Text txtHP;
    public Text txtShoes;
    public Text txtMagnet;
    public Text txtNoty;
    public GameObject goNoty;

    private void Start()
    {
        txtHighCoins.text = " " + UIManager.Instance.GetCoin();
        txtHP.text = " " + itemHP;
        txtShoes.text = " " + itemShoes;
        txtMagnet.text = " " + itemMagnet;
        goNoty.SetActive(false);
    }

    public void OnBuyItemHP()
    {
        if (UIManager.Instance.GetCoin() >= itemHP)
        {
            UIManager.Instance.AddCoin(-itemHP);

            UIManager.Instance.AddItemHP(1);
            txtHighCoins.text = UIManager.Instance.GetCoin().ToString();

            goNoty.SetActive(true);
            txtNoty.text = "Successfull!";
        }
        else
        {
            goNoty.SetActive(true);
            txtNoty.text = "<color=#FF0400>Failed!</color>";
        }
    }

    public void OnBuyItemShoes()
    {
        if (UIManager.Instance.GetCoin() >= itemShoes)
        {
            UIManager.Instance.AddCoin(-itemShoes);

            UIManager.Instance.AddItemShoes(1);

            txtHighCoins.text = UIManager.Instance.GetCoin().ToString();

            goNoty.SetActive(true);
            txtNoty.text = "Successfull!";
        }
        else
        {
            goNoty.SetActive(true);
            txtNoty.text = "<color=#FF0400>Failed!</color>";
        }
    }

    public void OnBuyItemMagnet()
    {
        if (UIManager.Instance.GetCoin() >= itemMagnet)
        {
            UIManager.Instance.AddCoin(-itemMagnet);

            UIManager.Instance.AddItemMagnet(1);
            txtHighCoins.text = UIManager.Instance.GetCoin().ToString();

            goNoty.SetActive(true);
            txtNoty.text = "Successfull!";
        }
        else
        {
            goNoty.SetActive(true);
            txtNoty.text = "<color=#FF0400>Failed!</color>";
        }
    }
}