using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject PauseUI;
    public GameObject[] playerList;
    private int indexCharter = -1;

    private GameObject player;
    public Vector3 postionStart;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        SelectCharacter(PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.CHARACTER_SELECTED.ToString(), 0));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PoolBase.instance.GetObj("PauseUI", PauseUI, UIManager.Instance.popuTransform).transform.localPosition = Vector3.zero; ;
        }
    }

    public void SelectCharacter(int index)
    {
        if (index != indexCharter)
        {
            indexCharter = index;
            PlayerPrefs.SetInt(SAVE_PLAYER_PREFS.CHARACTER_SELECTED.ToString(), index);
            if (player != null)
            {
                postionStart = player.transform.position;
                Destroy(player);
            }

            var newPlayer = Instantiate(playerList[index]);
            newPlayer.transform.position = postionStart;
            player = newPlayer;
        }
    }
    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}

    //private void SaveGame()
    //{
    //    PlayerPrefs.SetInt(SAVE_PLAYER_PREFS.HP.ToString(), player.GetComponent<PlayerController>().playerBase.ourHealth);
    //    PlayerPrefs.SetString(SAVE_PLAYER_PREFS.POSITION.ToString(), $"{player.transform.position.x}|{player.transform.position.y}");
    //}

    //public void LoadGame()
    //{
    //    if (PlayerPrefs.HasKey(SAVE_PLAYER_PREFS.POSITION.ToString()))
    //    {
    //        var positon = PlayerPrefs.GetString(SAVE_PLAYER_PREFS.POSITION.ToString()).Split('|');
    //        GameManager.instance.postionStart = new Vector3(float.Parse(positon[0]), float.Parse(positon[1]));

    //    }
    //}
}

public enum SAVE_PLAYER_PREFS
{
    highCoins,
    itemHP,
    itemSpeedShoes,
    itemMagnet,
    CHARACTER_SELECTED,
    HP,
    POSITION
}