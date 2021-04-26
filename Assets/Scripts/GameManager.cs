using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject PauseUI;
    public GameObject[] playerList;
    private int indexCharter = -1;

    private GameObject player;
    [SerializeField] private Vector3 postionStart;


    //private PlayerController _Player;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        SelectCharacter(PlayerPrefs.GetInt(SAVE_PLAYER_PREFS.CHARACTER_SELECTED.ToString(), 0));
        //_Player = GameObject.FindObjectOfType<PlayerController>();
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

//    public void SaveGame()
//    {
//        Debug.Log("Save");
//        FileStream file = new FileStream(Application.persistentDataPath + "/PlayerController", FileMode.OpenOrCreate);

//        BinaryFormatter formatter = new BinaryFormatter();

//        formatter.Serialize(file, _Player.playerBase);

//        file.Close();
//}

//    public void LoadGame()
//    {
//        FileStream file = new FileStream(Application.persistentDataPath + "/PlayerController.dat", FileMode.Open);

//        BinaryFormatter formatter = new BinaryFormatter();

//        _Player.playerBase = (PlayerBasic)formatter.Deserialize(file);

//        file.Close();
//    }
}

public enum SAVE_PLAYER_PREFS
{
    highCoins,
    itemHP,
    itemSpeedShoes,
    itemMagnet,
    CHARACTER_SELECTED
}