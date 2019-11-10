using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soalManager : MonoBehaviour
{
    public static soalManager Instance {get; set;}
    void Awake() {
        Instance = this;
        DontDestroyOnLoad(Instance);


        panelSoal = GameObject.FindGameObjectWithTag("Soal");
        panelSoal.SetActive(false);
    }
    public void addToCount(int a) { currentCount += a; }
    public int getCount() {return currentCount;}
    public bool checkSoal() {return isSoal;}
    public void popUpSoal() {panelSoal.SetActive(true);}

    GameObject panelSoal;
    const int soalTriggerCount = 10;
    const int minCount = 0;
    [SerializeField] int currentCount = 0;
    [SerializeField] bool isSoal = false;

    void Update() {
        if (currentCount == soalTriggerCount)
        {
            isSoal = true;
            currentCount = minCount;
        } else { isSoal = false; }    
    }

}
