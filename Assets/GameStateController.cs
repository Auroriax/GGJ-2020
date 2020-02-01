using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public string[] LevelNames;
    public TextMeshProUGUI LevelText;

    public int Current { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        var search = LevelNames.Select((l, i) => new { name = l, i}).Where(l => l.name == currentScene.name).FirstOrDefault();
        if (search == null)
            Current = -1;
        else
            Current = search.i;

        if (LevelText != null)
        {
            var text = "Level ";
            if (Current == -1)
                text += "U";
            else
                text += (Current + 1).ToString();
            LevelText.text = text;
        }
    }

    public void ResetGame()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelCompleted()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        if (Current < LevelNames.Length - 1)
            SceneManager.LoadScene(LevelNames[Current + 1]);
        else if (LevelNames.Length == 0)
            ResetGame();
        else
            SceneManager.LoadScene(LevelNames[0]);
    }

    public void PreviousLevel()
    {
        if (Current > 0)
            SceneManager.LoadScene(LevelNames[Current - 1]);
        else
            ResetGame();
    }
}
