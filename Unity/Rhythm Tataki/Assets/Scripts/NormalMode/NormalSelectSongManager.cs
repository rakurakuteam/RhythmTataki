using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalSelectSongManager : MonoBehaviour
{
    public Image musicTitleImage;
    public GameObject setupPanel;
    public GameObject setupButton;
        
    
    void Start()
    {
        setupPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HomeButtonEvent()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    public void SettingButtonEvent()
    {
        // Debug.Log("셋팅버튼 클릭됨");
        setupPanel.SetActive(true);
        setupButton.SetActive(false);
    }

    public void BackButtonEvent()
    {
        setupPanel.SetActive(false);
        setupButton.SetActive(true);
    }

    public void ReloadButtonEvent()
    {
        Debug.Log("clicked Reload Button");
    }
}
