using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;
    public Button[] levelButtons;
    public AudioClip turretReload;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached",0);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= levelReached) { continue; }
            levelButtons[i].interactable = false;
        }
    }
    public void Select (int levelNumber)
    {
        GetComponent<AudioSource>().PlayOneShot(turretReload);
        sceneFader.FadeTo(levelNumber);
    }
}
