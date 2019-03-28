using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public enum GameState
    {
        Play, Win, Lose
    }

    public static GameState gameState;
    public static bool playerHited = false;
    public static float maxFly = 100f;
    public static float flyPower = 100f;
}
