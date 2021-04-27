using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Coin Data")]
    private int highCoins = 0;

    [SerializeField] private Text highCoinsText, txtLives;
    public GameObject coinWinLv;

    [Header("Item Data")]
    public Text txtItemHP;

    [SerializeField] private Text txtItemSpeedShoes;
    [SerializeField] private Text txtItemMagnet;
    private int itemHP;
    private int itemSpeedShoes;
    private int itemMagnet;

    [Header("Game Render")]
    public GameObject Boss;
    public GameObject goNextLevel;
    public GameObject Door;

    [HideInInspector]
    public PlayerController player;

    public int levelLoad;

    [SerializeField] private GameObject gameOverUI, shopUI, settingUI;

    public static int _remainingLives = 3;

    [SerializeField] private Slider sliderHP;
    [SerializeField] private Slider sliderStamina;

    public int hp = 100;
    public float stamina = 100f;
    public Transform popuTransform;

    private void Awake()
    {
        Instance = this;
        itemHP = PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.itemHP.ToString(), 0);
        itemSpeedShoes = PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.itemSpeedShoes.ToString(), 0);
        itemMagnet = PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.itemMagnet.ToString(), 0);
        highCoins = PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.highCoins.ToString(), 0);
        hp = PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.HP.ToString(), 100);
        //highCoins = 100000;
        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        SetValueHpSlider(hp);
        highCoinsText.text = " " + highCoins.ToString();

        if (PlayerPrefs.HasKey("highCoins"))
        {
            Scene ActiveScreen = SceneManager.GetActiveScene();
            if (ActiveScreen.buildIndex != 1)
            {
                highCoins = PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.highCoins.ToString());
            }
        }

        txtLives.text = " x " + _remainingLives;
        // Item
        txtItemHP.text = itemHP.ToString();
        txtItemSpeedShoes.text = itemSpeedShoes.ToString();
        txtItemMagnet.text = itemMagnet.ToString();
    }

    private void Update()
    {
        CheckBossDied();
    }

    public void CheckBossDied()
    {
        if (Boss == null)
        {
            Door.SetActive(true);
        }
    }

    public void ClickButtonShop()
    {
        PoolBase.instance.GetObj("shop", shopUI, popuTransform).transform.localPosition = Vector3.zero;
    }

    public void ClickSetting()
    {
        PoolBase.instance.GetObj("setting", settingUI, popuTransform).transform.localPosition = Vector3.zero;
    }

    public void SetValueHpSlider(int hp)
    {
        this.hp = hp;
        sliderHP.value = hp;
    }

    public void SetValueStaminaSlider(float stamina)
    {
        this.stamina = stamina;
        sliderStamina.value = stamina;
    }

    public void Death()
    {
        _remainingLives--;
        txtLives.text = " x " + _remainingLives;
        if (_remainingLives <= 0)
        {
            GameObject obj = PoolBase.instance.GetObj("GameOver", gameOverUI, popuTransform);
            obj.transform.localPosition = Vector3.zero;
        }
    }

    public int GetCoin()
    {
        return highCoins;
    }

    public void AddCoin(int coin)
    {
        highCoins += coin;
        highCoinsText.text = highCoins.ToString();
        PlayerPrefs.SetInt(SAVE_PLAYER_PREFS.highCoins.ToString(), highCoins);
        PlayerPrefs.Save();
    }

    public void AddItemHP(int number)
    {
        itemHP += number;
        txtItemHP.text = itemHP.ToString();
        PlayerPrefs.SetInt(SAVE_PLAYER_PREFS.itemHP.ToString(), itemHP);
        PlayerPrefs.Save();
    }

    public void AddItemShoes(int number)
    {
        itemSpeedShoes += number;
        txtItemSpeedShoes.text = itemSpeedShoes.ToString();
        PlayerPrefs.SetInt(SAVE_PLAYER_PREFS.itemSpeedShoes.ToString(), itemSpeedShoes);
        PlayerPrefs.Save();
    }

    public void AddItemMagnet(int number)
    {
        itemMagnet += number;
        txtItemMagnet.text = itemMagnet.ToString();
        PlayerPrefs.SetInt(SAVE_PLAYER_PREFS.itemMagnet.ToString(), itemMagnet);
        PlayerPrefs.Save();
    }

    public void OnClickUseItemHP()
    {
        if (itemHP > 0)
        {
            if (player.playerBase.IsFullHP())
            {
                player.AddHealth(20);
                AddItemHP(-1);
            }
        }
    }

    public void OnClickUseItemShoes()
    {
        if (itemSpeedShoes > 0)
        {
            player.playerBase.maxSpeed = 6f;
            player.playerBase.speed = 100f;
            AddItemShoes(-1);
            StartCoroutine(TimeSpeedItem(5));
        }
    }

    public void OnClickUseItemMagnet()
    {
        if (itemMagnet > 0)
        {
            player.playerBase.isMagnet = true;
            AddItemMagnet(-1);
            StartCoroutine(TimeMagnetItem(10));
        }
    }

    private IEnumerator TimeSpeedItem(float time)
    {
        yield return new WaitForSeconds(time);
        player.playerBase.maxSpeed = 3f;
        player.playerBase.speed = 50f;
        yield return 0;
    }

    private IEnumerator TimeMagnetItem(float time)
    {
        yield return new WaitForSeconds(time);
        player.playerBase.isMagnet = false;
        yield return 0;
    }

    public void OnClickFire()
    {
        if (Time.time > player.nextFire)
        {
            player.nextFire = Time.time + player.fireRate;
            player.Fire();
        }
    }
}