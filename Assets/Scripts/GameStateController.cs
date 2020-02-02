using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public LevelArray LevelArray;
    public TextMeshProUGUI LevelText;

    private LevelData current;

    // Start is called before the first frame update
    void Start()
    {
        current = LevelArray.GetCurrent();

        if (LevelText != null)
        {
            var text = "";
            if (current == null)
                text += "U";
            else
                text += current.LevelNr.ToString();
            LevelText.text = text;
        }
    }

    public void ResetGame()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(LevelArray.MainMenuName);
    }

    public void LevelCompleted()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        var next = LevelArray.GetNextLevel();
        if (next == null)
            MainMenu();
        else
            SceneManager.LoadScene(next.SceneName);
    }

    public void PreviousLevel()
    {
        var prv = LevelArray.GetPreviousLevel();
        if (prv == null)
            MainMenu();
        else
            SceneManager.LoadScene(prv.SceneName);
    }
}
