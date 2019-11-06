using UnityEngine;

[CreateAssetMenu(fileName = "itemName_Manager")]
public class itemsManager : ScriptableObject
{
    public GameObject prefab;
    public boundaries scrBound;
    public int offset = 1;
    public float speed;
    public string tag;
}
