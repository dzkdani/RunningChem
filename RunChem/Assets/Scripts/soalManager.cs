using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class soalManager : MonoBehaviour
{
    public static soalManager Instance {get; private set;}
    public TextMeshProUGUI soalText;
    public List<TextMeshProUGUI> opsiText = new List<TextMeshProUGUI>();
    GameObject panelSoal;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
        
        initPanelSoal();
    }
    void initPanelSoal()
    {
        panelSoal = GameObject.FindGameObjectWithTag("Soal");
        panelSoal.SetActive(false);
    }

    public void addToCount(int a) { currentCount += a; }
    public int getCount() {return currentCount;}
    public bool checkSoal() {return isSoal;}
    public void popUpSoal() {SoalPanel();}

    const int soalTriggerCount = 10;
    const int minCount = 0;
    [SerializeField] int currentCount = 0;
    [SerializeField] bool isSoal = false;


    public List<soal> soalList = new List<soal>();


    void Update() {
        if (currentCount == soalTriggerCount)
        {
            isSoal = true;
            currentCount = minCount;
        } else { isSoal = false; }    
    }

    void SoalPanel()
    {
        panelSoal.SetActive(true);

        soalText.SetText("soalList[Random.Range(0, soalList.Count)].soalText");
        
        Time.timeScale = 0f;

        StartCoroutine(soalEndTimer());
    }

    IEnumerator soalEndTimer()
    {
        yield return new WaitForSecondsRealtime(1f);

        panelSoal.SetActive(false);

        ObjectSpawner.Instance.mainCoroutineStarter();

        Time.timeScale = 1f;
    }
}
