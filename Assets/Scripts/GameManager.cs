using UnityEngine;

/**
 * "GameManager" naming used because of Unity has custom unique gear icon for this Script name :)
 */
public class GameManager : MonoBehaviour
{
    public LevelsManager levels;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 0.02f;
    }

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        var level = levels.GetCurrent();
        if (level == null)
        {
            Debug.Log("Can't load level");
            return;
        }

        // var gameLevel = Resources.Load<Transform>(level.levelResource);
    }
}