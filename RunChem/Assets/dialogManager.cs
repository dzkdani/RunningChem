using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogManager : MonoBehaviour
{
    public Animator profAnim;
    public TextMeshProUGUI KalimatUIText; 
    Queue<string> queKalimat = new Queue<string>();
    public void SetDialogue(kalimat kalimat)
    {
         queKalimat.Clear();

         foreach (string textKalimat in kalimat.daftarKalimat)
         {
             queKalimat.Enqueue(textKalimat);
         }

         DisplayNextText();
    }

    public void DisplayNextText()
    {
        if (queKalimat.Count == 0)
        {
            EndKalimat();
            return;
        }

        profAnim.SetTrigger("EndDialog");

        string tempKalimat = queKalimat.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSetterText(tempKalimat));
    }

    IEnumerator TypeSetterText(string kalimat)
    {
        KalimatUIText.text = "";
        foreach (char huruf in kalimat.ToCharArray())
        {
            KalimatUIText.text += huruf;
            yield return null;
        }

    }

    private void EndKalimat()
    {
        sceneTransitionController.Instance.toNextLevel();
    }
}
