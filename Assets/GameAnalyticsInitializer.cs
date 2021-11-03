using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsInitializer : MonoBehaviour
{  
    void Start()
    {
        GameAnalytics.Initialize();
    }

}
