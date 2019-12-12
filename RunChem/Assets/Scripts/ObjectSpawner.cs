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
    IEnumerator coinSpawner, enemySpawner, redSpawner;

    void Start()
    {
        foreach (Timer time in itemTimer)
        {
            timerDictionary.Add(time.tag, time.timeVal);
        }

        coinSpawner = mainTimer("coin", timerDictionary["coin"]);
        enemySpawner = mainTimer("enemy", timerDictionary["enemy"]);
        redSpawner = mainTimer("red",timerDictionary["red"]);

        mainCoroutineStarter();
    }

    public void mainCoroutineStarter()
    {
        if (gameStart)
        {
            StartCoroutine(coinSpawner);
            StartCoroutine(enemySpawner);
            StartCoroutine(redSpawner);
        }
    }

    void Update()
    {
        extraHealthCheck();

        soalReadyCheck();
    }

    bool soalCheck = false;
    void soalReadyCheck()
    {
        if (soalManager.Instance.isPopUpSoal())
        {
            if (!soalCheck)
            {
                StopAllCoroutines();
                conditionalTimer("blue");
                soalCheck = true;
            }
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

    public void opsiJawabanTimer(string tag)
    {
        spawnOpsiJwbn(tag);
    }

    
    int i;
    public int getIteration() {return i;}

    void spawnOpsiJwbn(string tag)
    {
        for (i = 0; i < spawnPositionsX.Count; i++)
        {
            var objToSpwn = ObjectPooler.Instance.SpawnFromPool(tag, new Vector2(spawnPositionsX[i], transform.position.y)
                            , Quaternion.identity);
        }
    }

    void spawnObj(string tag) 
    {
        Vector2 spawnPosition = new Vector2 (spawnPositionsX[Random.Range(0, spawnPositionsX.Count)],
                                    transform.position.y);
        var objectToSpawn = ObjectPooler.Instance.SpawnFromPool(tag, spawnPosition, Quaternion.identity);
    }

    void conditionalTimer(string tag)
    {
        spawnObj(tag);
    }

    IEnumerator mainTimer(string tag, int time) {
        while(gameStart) {
            yield return new WaitForSeconds(time);
            spawnObj(tag);
        }
    }
}
