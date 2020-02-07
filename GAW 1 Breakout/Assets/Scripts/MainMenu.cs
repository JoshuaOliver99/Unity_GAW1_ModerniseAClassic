using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
