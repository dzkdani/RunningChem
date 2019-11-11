using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soal_")]
public class soal : ScriptableObject
{
    const int maxOpsi = 5;
    public string tag;
    [TextArea] public string soalText;
    public string[] opsi = new string[maxOpsi];
    public int opsiBnr;
    
}
