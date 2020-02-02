using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableMenuLevelItem : MonoBehaviour
{
    public LevelData LevelData;

    public void StartLevel()
    {
        SceneManager.LoadScene(LevelData.SceneName);
    }
}
