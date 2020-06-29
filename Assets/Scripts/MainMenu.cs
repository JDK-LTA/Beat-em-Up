using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Break();
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }
}
