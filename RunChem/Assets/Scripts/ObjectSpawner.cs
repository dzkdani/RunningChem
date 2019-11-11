using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectSpawner : MonoBehaviour
{
    #region Timer
    [System.Serializable]
    public struct Timer
    {
        public string tag;
        public int timeVal;
    }
    public List<Timer> itemTimer;
    public Dictionary<string, int> timerDictionary = new Dictionary<string, int>();
    #endregion
    
    #region Singleton
    public static ObjectSpawner Instance {get; private set;}
    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    #endregion

    bool gameStart = true;
    
    [SerializeField] List<float> spawnPositionsX = new List<float>();
 
    public bool extraHealthSpwn = false;
    IEnumerator coinSpawner, enemySpawner;

    void Start()
    {
        foreach (Timer time in itemTimer)
        {
            timerDictionary.Add(time.tag, time.timeVal);
        }

        coinSpawner = mainTimer("coin", timerDictionary["coin"]);
        enemySpawner = mainTimer("enemy", timerDictionary["enemy"]);

        mainCoroutineStarter();
    }

    public void mainCoroutineStarter()
    {
        if (gameStart)
        {
            StartCoroutine(coinSpawner);
            StartCoroutine(enemySpawner);
        }
    }

    void Update()
    {
        extraHealthCheck();

        soalCheck();
    }

    void soalCheck()
    {
        if (soalManager.Instance.checkSoal())
        {
            StopAllCoroutines();
            conditionalTimer("blue");
        }
    }

    void extraHealthCheck()
    {
        if (healthBar.Instance.CurrentHealthMin())
        {
            if(!extraHealthSpwn)
            {
                conditionalTimer("red");
                extraHealthSpwn = true; 
            }
        }
    }

    void conditionalTimer(string tag)
    {
        spawnObj(tag);
    }

    void spawnObj(string tag) {
        Vector2 spawnPosition = new Vector2 (spawnPositionsX[Random.Range(0, spawnPositionsX.Count)],
                                    transform.position.y);
        var objectToSpawn = ObjectPooler.Instance.SpawnFromPool(tag, spawnPosition, Quaternion.identity);
    }

    IEnumerator mainTimer(string tag, int time) {
        while(gameStart) {
            yield return new WaitForSeconds(time);
            spawnObj(tag);
        }
    }
}
