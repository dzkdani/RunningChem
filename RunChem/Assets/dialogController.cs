using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogController : MonoBehaviour
{
    public kalimat textKalimat;

    public void StartDialogue()
    {
        FindObjectOfType<dialogManager>().SetDialogue(textKalimat);
    }
}

