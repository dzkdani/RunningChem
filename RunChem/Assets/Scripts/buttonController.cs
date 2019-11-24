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

   
   void Awake() {
       panelMateri.SetActive(false);
       panelLevel.SetActive(false);
       foreach (Button button in lvlButtons)
       {
           button.interactable = false;
       }
   }

   public GameObject panelLevel;
   public Button[] lvlButtons;
   public static int maxLvlUnlocked;

   public void play() 
   {
       s = this.GetComponent<AudioSource>();
       s.PlayOneShot(clip);

       maxLvlUnlocked = STARTING_LVL;

       StartCoroutine(delayButton());

       panelLevel.SetActive(true);

       for (int i = 0; i < maxLvlUnlocked; i++)
       {
           lvlButtons[i].interactable = true;
       } 
   }

   public void goToLvl(int lvl)
   {
       SceneManager.LoadScene("Level "+lvl);
   }

   bool isMateriOn = false;
   public GameObject panelMateri;
   [TextArea]
   public string[] kompetensi; 
   public TextMeshProUGUI kompetensiText, lvl_UInav;
   int materiNav;

   public void materi() 
   { 
       isMateriOn = true;
       panelMateri.SetActive(true);
       materiNav = STARTING_LVL;
       kompetensiText.SetText(kompetensi[materiNav]);
       lvl_UInav.SetText((materiNav).ToString()); 
   }

   public void nextMateri()
   {
       if (isMateriOn && materiNav < MAX_LVL)
       {
           kompetensiText.SetText(kompetensi[materiNav+=1]);
           lvl_UInav.SetText((materiNav+1).ToString());
       }
   }
   public void prevMateri()
   {
       if (isMateriOn && materiNav >= STARTING_LVL)
       {
           kompetensiText.SetText(kompetensi[materiNav-=1]);
           lvl_UInav.SetText((materiNav+1).ToString());
       }
   }
   public void exit() 
   { 
       panelMateri.SetActive(false);
       panelLevel.SetActive(false);
       isMateriOn = false; 
   }

   IEnumerator delayButton() { yield return new WaitForSeconds(1.0f); }
}
