using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soal_")]
public class soal : ScriptableObject
{
    public string tag;
    [TextArea] public string soalText;
    public string[] opsi = new string[5];
    public int opsiBnr;
    
}
