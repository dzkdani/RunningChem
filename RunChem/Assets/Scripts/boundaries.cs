using UnityEngine;

[CreateAssetMenu(fileName = "bounds")]
public class boundaries : ScriptableObject
{   
    private Vector2 screenBound;
    private Camera mainCam;
    public Vector2 GetScrBound()
    {
        return screenBound;
    }

    void OnEnable()
    {
        mainCam = Camera.main;
        screenBound = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 
                    Screen.height, mainCam.transform.position.z));
    }
}
