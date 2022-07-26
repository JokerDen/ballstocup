using ui;
using UnityEngine;

/**
 * "GameManager" naming used because of Unity has custom unique gear icon for this Script name :)
 */
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager current => instance;
    
    public LevelsManager levels;
    public GameUI ui;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 0.02f;
    }

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
    }

    public void Restart()
    {
        levels.LoadLevel(levels.CurrentIndex);
    }

    // Broadcasted from Inputable
    public void OnInputDeltaX(float deltaX)
    {
        levels.Current.MoveX(deltaX);
    }

    public void ChangeLevel(int diff)
    {
        var idx = Mathf.Max(levels.CurrentIndex + diff, 0);
        levels.LoadLevel(idx);
    }
}