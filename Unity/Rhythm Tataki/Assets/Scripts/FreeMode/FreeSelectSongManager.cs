using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeSelectSongManager : MonoBehaviour
{

    public GameObject setupPanel;
    public GameObject setupButton;

    // Start is called before the first frame update
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

    public void AddButtonEvent()
    {
        Debug.Log("clicked Add Button");
        
    }
}
