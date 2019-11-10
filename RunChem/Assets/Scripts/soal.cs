using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soal_")]
public class soal : ScriptableObject
{
    public string tag;
    private const int maxOpsi = 5;
    [TextArea] public string soalText;
    public string[] opsi = new string[maxOpsi];
    public int opsiBnr;
    
}
