using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelArray : MonoBehaviour
{
    public string[] LevelNames;

    public LevelData[] Levels { get; private set; }
    private int currentLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        Levels = LevelNames.Select((l, i) => new LevelData { LevelNr = i, SceneName = l }).ToArray();

        var currentScene = SceneManager.GetActiveScene();
        var current = Levels.Select((l, i) => new { Level = l, i }).Where(l => l.Level.SceneName == currentScene.name).FirstOrDefault();
        if (current == null)
            currentLevelIndex = -1;
        else
            currentLevelIndex = current.i;
    }

    public LevelData GetCurrent()
    {
        if (currentLevelIndex == -1)
            return null;
        return Levels[currentLevelIndex];
    }

    public LevelData GetNextLevel()
    {
        if (LevelNames.Length == 0)
            return null;
        else if (currentLevelIndex == -1)
            return Levels[0];
        else if (currentLevelIndex < LevelNames.Length - 1)
            return Levels[currentLevelIndex + 1];
        else 
            return null;
    }

    public LevelData GetPreviousLevel()
    {
        if (currentLevelIndex > 0)
            return Levels[currentLevelIndex - 1];
        else
            return null;
    }
}

public class LevelData
{
    public int LevelNr;
    public string SceneName;
}
