using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound 
{
    public string tag;
    public AudioClip clip;
}

public class audioManager : MonoBehaviour
{
    public static audioManager Instance {get; private set;}

    AudioSource source;
    public List<Sound> clips = new List<Sound>();

    Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

    void Awake() {
        Instance = this;
        DontDestroyOnLoad(Instance);

        source = this.GetComponent<AudioSource>();

        foreach (Sound soundClip in clips)
        {
            clipDictionary.Add(soundClip.tag, soundClip.clip);
        }    
    }

    public void PlayAudio(string tag)
    {
        source.PlayOneShot(clipDictionary[tag]);
    }

}
