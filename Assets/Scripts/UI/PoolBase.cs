using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{
    public static PoolBase instance;
    public Dictionary<string, GameObject> objPool;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        objPool = new Dictionary<string, GameObject>();
    }

    public GameObject GetObj(string key, GameObject objCreate = null, Transform tParemt = null)
    {
        GameObject obj;
        if (objPool.ContainsKey(key))
        {
            obj = objPool[key];
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                if (tParemt != null)
                    obj.transform.SetParent(tParemt);
            }
        }
        else
        {
            obj = Instantiate(objCreate);
            if (tParemt == null)
                obj.transform.SetParent(transform);
            else obj.transform.SetParent(tParemt);
            objPool.Add(key, obj);
        }
        obj.transform.SetAsLastSibling();
        return obj;
    }
}