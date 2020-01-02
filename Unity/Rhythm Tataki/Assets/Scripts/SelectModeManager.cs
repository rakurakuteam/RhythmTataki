using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectModeManager : MonoBehaviour {
    public void GoHome()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    public void GoFreeMode()
    {
        SceneManager.LoadScene("FreeMusicSelectScene");
    }

    public void GoNormalMode()
    {
        SceneManager.LoadScene("NormalMusicSelectScene");
    }

    public void GoArcadeMode()
    {
        SceneManager.LoadScene("ArcadeModeScene");
    }

    public void GoCloudGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
