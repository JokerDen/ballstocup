using System;
using System.IO;
using levels;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField]
    private LevelConfig[] levels;

    [SerializeField]
    private int currentIndex;

    public GameLevel Current;

    public LevelConfig GetCurrent()
    {
        var totalNum = levels.Length;
        if (totalNum <= 0) return null;
        int index = currentIndex % totalNum;

        if (index >= 0 && index < totalNum)
            return levels[index];
        
        Debug.LogError($"Can't get level {index} ({currentIndex}) out of {totalNum}");

        return null;
    }
}

[Serializable]
public class LevelConfig
{
    public string levelResource;
    public int totalBallsNum;
    public int requiredBallsNum;
}