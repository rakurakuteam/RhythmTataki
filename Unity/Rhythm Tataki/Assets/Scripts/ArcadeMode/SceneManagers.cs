    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour {

    public void GoHome()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    public void GoFreeMode()
    {
        SceneManager.LoadScene("FreeModeScene");
    }

    public void GoArcadeMode()
    {
        SceneManager.LoadScene("ArcadeModeScene");
    }

    public void GoCloudGame()
    {
        SceneManager.LoadScene("CloudGameScene");
    }

    public void RetryFreeGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("FreeModeGameScene2");
    }

    public void GoLogin()
    {
        SceneManager.LoadScene("LogIn(e-mail etc)Scene");
    }

    public void GoSignIn()
    {
        SceneManager.LoadScene("SignInScene");
    }

    public void GoBlockGame()
    {
        SceneManager.LoadScene("DeleteBlockGameScene");
    }
    public void AddBlockGame()
    {
        SceneManager.LoadScene("AddBlockGameScene");
    }

}
