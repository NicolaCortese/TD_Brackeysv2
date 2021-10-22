using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void Select (int levelNumber)
    {
        sceneFader.FadeTo(levelNumber);
    }
}
