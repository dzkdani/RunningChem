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
    

    private Vector2 spawnPosition;
    private const float offset = 1;
    private const float minX = -2.2f;
    private const float maxX = 2.2f;
    IEnumerator coinSpawner, enemySpawner;
    private bool soalReady = false;
    [SerializeField] private bool extraHealthSpwn = false;
    [SerializeField] private bool isSoalPopUp = false;

    void Start() {
        
        foreach (Timer time in itemTimer)
        {
            timerDictionary.Add(time.tag, time.timeVal);
        }

        coinSpawner = mainTimer("coin", timerDictionary["coin"]);
        enemySpawner = mainTimer("enemy", timerDictionary["enemy"]);

        if (gameStart)
        {
            StartCoroutine(coinSpawner);
            StartCoroutine(enemySpawner);
        }
    }

    void Update() {
        //healthextra
        if (healthBar.Instance.GetHealth() <= 2)
        {
            if (!extraHealthSpwn) {
                conditionalTimer("red");
                extraHealthSpwn = true;
            }
        } else if (healthBar.Instance.GetHealth() > 2) { extraHealthSpwn = false; }

        //soalpopup
        if(soalManager.Instance.checkSoal()) {
            if(!isSoalPopUp) {
                conditionalTimer("blue");
                isSoalPopUp = true;
            }
        }
    }

    void conditionalTimer(string tag)
    {
        spawnObj(tag);
    }

    void spawnObj(string tag) {
        spawnPosition = new Vector2 (Random.Range(minX, maxX), transform.position.y);

        var objectToSpawn = ObjectPooler.Instance.SpawnFromPool(tag, spawnPosition, Quaternion.identity);
    }

    IEnumerator mainTimer(string tag, int time) {
        while(gameStart) {
            yield return new WaitForSeconds(time);
            spawnObj(tag);
        }
    }
}
