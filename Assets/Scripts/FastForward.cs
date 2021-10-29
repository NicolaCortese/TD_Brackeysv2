using UnityEngine;

public class FastForward : MonoBehaviour
{
    public void FastForwarding()
    {
        if (Time.timeScale == 2f)
        {
            Time.timeScale = 1f;
        }
        else
            Time.timeScale = 2f;
    }
}
