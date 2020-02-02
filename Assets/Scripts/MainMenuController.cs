using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject ViewPort;
    public GameObject ItemHolderPrefab;
    public GameObject ItemPrefab;

    public LevelArray LevelFinder;

    bool once = false;

    void Update()
    {
        if (once)
            return;

        PopulateMenuItems();
        once = true;
    }

    public void PopulateMenuItems()
    {
        GameObject itemHolder = null;
        var itemsInHolder = 0;
        foreach(var level in LevelFinder.Levels)
        {
            if (itemHolder == null)
                itemHolder = Instantiate(ItemHolderPrefab, ViewPort.transform, false);

            var levelItem = Instantiate(ItemPrefab, itemHolder.transform, false);
            var levelData = levelItem.GetComponent<ClickableMenuLevelItem>();
            levelData.LevelData = level;
            itemsInHolder++;

            if (itemsInHolder >= 4)
            {
                itemHolder = null;
                itemsInHolder = 0;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
