using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransitionController : MonoBehaviour
{
    public static sceneTransitionController Instance {get; set;}
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        //DontDestroyOnLoad(Instance);
    }
    public Animator animator;
    private int lvlToLoad;

    void FadeOutLevel(int lvlIndex)
    {
        lvlToLoad = lvlIndex;
        animator.SetTrigger("isFadeOut");
    }

    public void toNextLevel()
    {
        FadeOutLevel(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(lvlToLoad);
    }
}
