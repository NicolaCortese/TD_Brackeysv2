using UnityEngine;

public class speedup : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Time.timeScale = 2;
        }   
    }
}
