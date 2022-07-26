using System;
using System.IO;
using levels;
using UnityEngine;
using UnityEngine.Events;

public class LevelsManager : MonoBehaviour
{
    [SerializeField]
    private LevelConfig[] levels;

    [SerializeField]
    private int currentIndex;
    public int CurrentIndex => currentIndex;

    public GameLevel Current => current;
    private GameLevel current;

    public LevelEvent onLevelLoaded = new LevelEvent();

    public LevelConfig GetCurrentConfig()
    {
        var totalNum = levels.Length;
        if (totalNum <= 0) return null;
        int index = currentIndex % totalNum;

        if (index >= 0 && index < totalNum)
            return levels[index];
        
        Debug.LogError($"Can't get level {index} ({currentIndex}) out of {totalNum}");

        return null;
    }

    public void LoadLevel(int index)
    {
        if (current != null)
            Destroy(current.gameObject);
        current = null;

        currentIndex = index;
        var config = GetCurrentConfig();
        if (config != null)
        {
            var prefab = Resources.Load<GameLevel>(config.levelResource);  // TODO: unload on used
            current = Instantiate(prefab);
            current.SetUp(config.totalBallsNum, config.requiredBallsNum);
        }
        onLevelLoaded.Invoke(current);
    }
}

[Serializable]
public class LevelConfig
{
    public string levelResource;
    public int totalBallsNum;
    public int requiredBallsNum;
}

public class LevelEvent : UnityEvent<GameLevel>
{
    
}