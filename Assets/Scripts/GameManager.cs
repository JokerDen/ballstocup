using UnityEngine;

/**
 * "GameManager" naming used because of Unity has custom unique gear icon for this Script name :)
 */
public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 0.02f;
    }
}
