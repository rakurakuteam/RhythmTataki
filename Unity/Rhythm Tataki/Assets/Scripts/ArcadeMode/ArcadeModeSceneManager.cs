using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeModeSceneManager : MonoBehaviour
{
    public void GoHome()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    public void GoAddBlockGame()
    {
        SceneManager.LoadScene("AddBlockGameScene");
    }

    public void GoDeleteBlockGame()
    {
        SceneManager.LoadScene("DeleteBlockGameScene");
    }
}
