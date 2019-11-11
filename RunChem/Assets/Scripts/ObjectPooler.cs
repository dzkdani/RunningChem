using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region ObjectsPools
    [System.Serializable]
    public struct Pool 
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    #endregion

    #region Singleton
    public static ObjectPooler Instance {get; private set;}
    private void Awake() {
        Instance = this; 
        DontDestroyOnLoad(Instance);

        initPool();   
    }

    #endregion

    void initPool()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQue.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectQue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 pos, Quaternion rot)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        spawnObject(pos, rot, objectToSpawn);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private static void spawnObject(Vector2 pos, Quaternion rot, GameObject objectToSpawn)
    {
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;
    }

    public void ReturnToPool(GameObject obj, string tag) {
        obj.gameObject.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
