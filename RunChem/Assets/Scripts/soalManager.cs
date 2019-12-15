using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class soalManager : MonoBehaviour
{
    public static soalManager Instance {get; private set;}
    public TextMeshProUGUI soalText;
    public List<TextMeshProUGUI> opsiText = new List<TextMeshProUGUI>();
    public TextMeshProUGUI soalTextUI;
    public TextMeshProUGUI coinCounterUI;

    GameObject panelSoal;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
        
        initPanel();
    }
    void initPanel()
    {
        totalSoal = soalList.Count;
        soalLeft = totalSoal;
        panelSoal = GameObject.FindGameObjectWithTag("Soal");
        panelSoal.SetActive(false);
        panelSelesaiLevel.SetActive(false);
        soalTextUI.SetText((totalSoal-soalLeft)+"/"+totalSoal);
        coinCounterUI.SetText("0");
    }

    public void addToCount(int a) { currentCount += a; }
    public int getCount() {return currentCount;}
    public bool isPopUpSoal() {return isSoal;}
    public void popUpSoal() {SoalPanel();}
    public bool isSoalEnd() {return soalLeft == 0;}

    readonly int minCount = 0;
    int soalTriggerCount = 10;
    int currentCount = 0;
    static int totalSoal;
    [SerializeField] int soalLeft;
    [SerializeField] bool isSoal = false;
    [SerializeField] int jawabanBenar;
    [SerializeField] List<soal> soalList = new List<soal>();

    void Update() {
        coinCounterUI.SetText("<b>"+currentCount.ToString()+"</b>");    
        if (currentCount == soalTriggerCount && soalLeft > 0)
        {
            isSoal = true;
            soalTriggerCount += 10;
        } else { isSoal = false; }
    }

    #region POP UP SOAL
    void SoalPanel()
    {
        panelSoal.SetActive(true);
        int rand = UnityEngine.Random.Range(0, soalList.Count);
        soalText.SetText(soalList[rand].soalText);
        for (int i = 0; i < opsiText.Count; i++)
        {
            opsiText[i].SetText(soalList[rand].opsi[i]);
        }

        jawabanBenar = soalList[rand].opsiBnr;

        StartCoroutine(SoalPopUpTimer());
        
        soalList.RemoveAt(rand);
        soalLeft = soalList.Count;
        soalTextUI.SetText("<b>"+(totalSoal-soalLeft)+"/"+totalSoal+"</b>");
    }

    [SerializeField] float popUpDuration;
    public float cekJawabanDuration;

    public void okLanjut()
    {
        StopCoroutine(SoalPopUpTimer());
        ObjectSpawner.Instance.opsiJawabanTimer("opsi");
        panelSoal.SetActive(false);
    }

    IEnumerator SoalPopUpTimer()
    {
        yield return new WaitForSecondsRealtime(30.0f);
    } 
    #endregion

    #region  CEK JAWABAN
    public Image emojiImg;
    GameObject panelEmoji;
    [SerializeField] List<Sprite> emojiList = new List<Sprite>();
    const int DAMAGE_SOAL = 4;
    const int SKOR_JWBN = 10;
    int currentSkor = 0;
    public TextMeshProUGUI skorTextUI;
    public void chekJawaban(int jwb)
    {   
        if (jwb == jawabanBenar)
        {
            currentSkor+=SKOR_JWBN;
            emojiImg.sprite = emojiList[0];
            audioManager.Instance.PlayAudio("jawabanBnr");
            skorTextUI.SetText("<b>"+currentSkor.ToString()+"</b>");
            soalBenar+=1;

        } else { 
            emojiImg.sprite = emojiList[1];
            healthBar.Instance.takeDamage(DAMAGE_SOAL);
            audioManager.Instance.PlayAudio("jawabanSlh"); 
        }

        emojiImg.color = new Color(emojiImg.color.r, emojiImg.color.g, emojiImg.color.b, 255);
         
        StartCoroutine(endSoal());
    }

    IEnumerator endSoal()
    {
        yield return new WaitForSeconds(cekJawabanDuration);

        emojiImg.color = new Color(emojiImg.color.r, emojiImg.color.g, emojiImg.color.b, 0);

        if (soalLeft != 0)
        {
            ObjectSpawner.Instance.mainCoroutineStarter();
        } else if (soalLeft == 0)
        {
            isSoal = false;
            StopAllCoroutines();
            ObjectSpawner.Instance.StopAllCoroutines();
            EndLevel();
        }
    }
    #endregion

    public GameObject panelSelesaiLevel;
    public TextMeshProUGUI TextSkorAkhir, textSoalBenar, textCoinTotal;
    int soalBenar = 0;
    private void EndLevel()
    {
        TextSkorAkhir.text = skorTextUI.text;
        textCoinTotal.text = coinCounterUI.text;
        textSoalBenar.text = soalBenar.ToString();

        panelSelesaiLevel.SetActive(true);
    }
}
