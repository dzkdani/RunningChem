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
        soalTextUI.SetText((totalSoal-soalLeft)+"/"+totalSoal);
        coinCounterUI.SetText("0");
    }

    public void addToCount(int a) { currentCount += a; }
    public int getCount() {return currentCount;}
    public bool isPopUpSoal() {return isSoal;}
    public void popUpSoal() {SoalPanel();}
    public bool isSoalEnd() {return soalLeft == 0;}

    readonly int soalTriggerCount = 10;
    readonly int minCount = 0;
    int currentCount = 0;
    static int totalSoal;
    [SerializeField] int soalLeft;
    [SerializeField] bool isSoal = false;
    [SerializeField] int jawabanBenar;
    [SerializeField] List<soal> soalList = new List<soal>();

    void Update() {
        if (currentCount == soalTriggerCount && soalLeft > 0)
        {
            isSoal = true;
            currentCount = minCount;
        } else { isSoal = false; }
        coinCounterUI.SetText(currentCount.ToString());    
    }

    void SoalPanel()
    {
        panelSoal.SetActive(true);
        int rand = Random.Range(0, soalList.Count);
        soalText.SetText(soalList[rand].soalText);
        for (int i = 0; i < opsiText.Count; i++)
        {
            opsiText[i].SetText(soalList[rand].opsi[i]);
        }

        jawabanBenar = soalList[rand].opsiBnr;

        StartCoroutine(opsiJawaban());
        
        soalList.RemoveAt(rand);
        soalLeft = soalList.Count;
        soalTextUI.SetText((totalSoal-soalLeft)+"/"+totalSoal);
    }

    public float popUpDuration;
    public float cekJawabanDuration;

    IEnumerator opsiJawaban()
    {
        yield return new WaitForSecondsRealtime(popUpDuration);

        panelSoal.SetActive(false);

        ObjectSpawner.Instance.opsiJawabanTimer("opsi");
    }

    public Image emojiImg;
    GameObject panelEmoji;
    [SerializeField] List<Sprite> emojiList = new List<Sprite>();

    public void chekJawaban(int jwb)
    {
        
        if (jwb == jawabanBenar)
        {
            emojiImg.sprite = emojiList[0];
        } else { 
            emojiImg.sprite = emojiList[1];
            healthBar.Instance.takeDamage(2); 
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
        }
    }
}
