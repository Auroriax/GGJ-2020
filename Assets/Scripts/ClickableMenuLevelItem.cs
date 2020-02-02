using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableMenuLevelItem : MonoBehaviour
{
    public LevelData LevelData;
    public TextMeshProUGUI ButtonText;

    public void Start()
    {
        ButtonText.text = LevelData.LevelNr.ToString();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(LevelData.SceneName);
    }
}
