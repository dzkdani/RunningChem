using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class buttonController : MonoBehaviour
{
   const int STARTING_LVL = 1;
   const int MAX_LVL = 2;
   AudioSource s;
   public AudioClip clip;

   
   void Awake()
    {
        initUI();
    }

    public GameObject buttonsPanel;
    void initUI()
    {
        turnOffAllPanel();
        foreach (Button button in lvlButtons)
        {
            button.interactable = false;
        }
    }

   void playAudio()
   {
        s = this.GetComponent<AudioSource>();
        s.PlayOneShot(clip);
   }

   #region PLAY 
   public GameObject panelLevel;
   public Button[] lvlButtons;
   public static int maxLvlUnlocked;
   
   public void play()
    {
        playAudio();

        maxLvlUnlocked = STARTING_LVL;

        buttonsPanel.SetActive(false);
        panelLevel.SetActive(true);

        for (int i = 0; i < maxLvlUnlocked; i++)
        {
            lvlButtons[i].interactable = true;
        }
    }

    public void goToDialogue()
    {
        playAudio();
        StartCoroutine(delayButton());
        sceneTransitionController.Instance.toNextLevel();
    }

    public void goToLvl(int lvl)
   {
       playAudio();
       StartCoroutine(delayButton());
       SceneManager.LoadScene("Level "+lvl);
   }
   #endregion

   #region MATERI 
   bool isMateriOn = false;
   public GameObject panelMateri;
   [TextArea]
   public string[] kompetensi; 
   public TextMeshProUGUI kompetensiText, lvl_UInav;
   int materiCount;

   public void materi() 
   {
       isMateriOn = true;
       buttonsPanel.SetActive(false);
       playAudio(); 
       panelMateri.SetActive(true);
       materiCount = STARTING_LVL-1;
       kompetensiText.SetText(kompetensi[materiCount]);
       lvl_UInav.SetText((materiCount+1).ToString()); 
   }

   public void nextMateri()
   {
       playAudio();
       if (isMateriOn && materiCount < MAX_LVL)
       {
           kompetensiText.SetText(kompetensi[materiCount+=1]);
           lvl_UInav.SetText((materiCount+1).ToString());
       }
   }
   public void prevMateri()
   {
       playAudio();
       if (isMateriOn && materiCount >= STARTING_LVL)
       {
           kompetensiText.SetText(kompetensi[materiCount-=1]);
           lvl_UInav.SetText((materiCount+1).ToString());
       }
   }
   #endregion
  
   #region RULE
   bool isRuleOn = false;
   int ruleCurrent = 0;
   public GameObject panelRule;
   public TextMeshProUGUI ruleText, rule_UInav;
   [System.Serializable]
   public struct Rule
   {
       public string tag;
       [TextArea]
       public string ruleText;
       public Sprite[] imgRule;
   }
   public Image[] imgRules;
   public List<Rule> rules = new List<Rule>();
   public void rule()
   {
       isRuleOn = true;
       buttonsPanel.SetActive(false);

       playAudio();
       panelRule.SetActive(true);

       ruleText.SetText(rules[ruleCurrent].ruleText);
       rule_UInav.SetText(rules[ruleCurrent].tag);

       for (int i = 0; i < imgRules.Length; i++)
       {
           imgRules[i].sprite = rules[ruleCurrent].imgRule[i];
       } 
   }

   int navCount = 0;
   public void nextRule()
   {
       playAudio();
       if (isRuleOn && ruleCurrent < rules.Count-1)
       {
           ruleText.SetText(rules[ruleCurrent+=1].ruleText);
           rule_UInav.SetText(rules[navCount+=1].tag);
           for (int i = 0; i < imgRules.Length; i++)
           {
                imgRules[i].sprite = rules[ruleCurrent].imgRule[i];
           } 
       }
   }
   public void prevRule()
   {
       playAudio();
       if (isRuleOn && ruleCurrent >= 1)
       {
            ruleText.SetText(rules[ruleCurrent-=1].ruleText);
            rule_UInav.SetText(rules[navCount-=1].tag);
            for (int i = 0; i < imgRules.Length; i++)
            {
                imgRules[i].sprite = rules[ruleCurrent].imgRule[i];
            }
       }
   }
   #endregion

   #region EXIT
   public void exit()
    {
        playAudio();
        turnOffAllPanel();
        if (isRuleOn)
        {
            isRuleOn = false;
        }
        if (isMateriOn)
        {
            isMateriOn = false;
        }
        buttonsPanel.SetActive(true);
    }
    private void turnOffAllPanel()
    {
        panelMateri.SetActive(false);
        panelLevel.SetActive(false);
        panelRule.SetActive(false);
        volSlider.SetActive(false);
    }
    #endregion

   public GameObject volSlider; 
   public void volume()
   {
       playAudio();
       volSlider.SetActive(true);
   }
    IEnumerator delayButton() { yield return new WaitForSeconds(3.0f); }
}
