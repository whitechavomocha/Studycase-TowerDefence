using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;

    private List<GameObject> pool;
    private GameObject prefabParent;

    private void Awake()
    {
        prefabParent = new GameObject(prefab.name + "Pool");
        pool = new List<GameObject>();
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(CreateInstance());
        }
    }

    private GameObject CreateInstance()
    {
        GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
        instance.transform.SetParent(prefabParent.transform);
        instance.SetActive(false);
        return instance;
    }

    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        return CreateInstance();
    }

    public static void ReturnThePool(GameObject instance)
    {
        instance.SetActive(false);
    }
}