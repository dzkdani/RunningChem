using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    #region Timer
    [System.Serializable]
    public class Timer
    {
        public string tag;
        public int timeVal;
    }
    public List<Timer> itemTimer;
    public Dictionary<string, int> timerDictionary = new Dictionary<string, int>();
    #endregion
    
    private bool gameStart = true;
    

    [SerializeField] boundaries scrBound;
    private Vector2 spawnPosition;
    private const float offset = 1;

    IEnumerator coinSpawner, redSpawner, bluSpawner;

    void Start() {
        
        foreach (Timer time in itemTimer)
        {
            timerDictionary.Add(time.tag, time.timeVal);
        }

        coinSpawner = spawnTimer("coin", timerDictionary["coin"]);
        redSpawner = spawnTimer("red", timerDictionary["red"]);
        bluSpawner = spawnTimer("blue", timerDictionary["blue"]);

        StartCoroutine(coinSpawner);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopAllCoroutines();
            StartCoroutine(bluSpawner);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopAllCoroutines();
            StartCoroutine(redSpawner);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StopAllCoroutines();
            StartCoroutine(coinSpawner);
        }
    }

    void spawnObj(string tag) {
        spawnPosition = new Vector2 (Random.Range(-scrBound.GetScrBound().x+offset, 
                                                scrBound.GetScrBound().x-offset) 
                                                ,scrBound.GetScrBound().y*2);

        var objectToSpawn = ObjectPooler.Instance.SpawnFromPool(tag, spawnPosition, Quaternion.identity);
    }

    IEnumerator spawnTimer(string tag, int time) {
        while(gameStart) {
            yield return new WaitForSecondsRealtime(time);
            spawnObj(tag);
        }
    }
}
