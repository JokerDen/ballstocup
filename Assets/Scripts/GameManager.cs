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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
        
        if (Input.GetKeyDown(KeyCode.S))
            levels.Current.ballsSpawner.Spawn();
    }

    private void Restart()
    {
        
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

    // Broadcasted from Inputable
    public void OnInputDeltaX(float deltaX)
    {
        levels.Current.rotator.Rotate(deltaX);
    }
}