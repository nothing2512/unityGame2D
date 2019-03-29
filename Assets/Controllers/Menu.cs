using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void play()
    {
        // set health
        Constants.health = Constants.maxHealth;

        //set fly power
        Constants.flyPower = Constants.maxFly;

        // Load Main Game Scene
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        // Exiting applications
        Application.Quit();
    }

    public void GoHome()
    {
        SceneManager.LoadScene("IndexUI");
    }
}
