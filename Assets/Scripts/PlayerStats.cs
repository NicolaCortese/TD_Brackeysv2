using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Lives;
    public int startMoney = 400;
    public int startLives = 20;
    public Text moneyText;

    public static int wavesSurvived;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;

        wavesSurvived = 0;
    }
    
}
