using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelect : MonoBehaviour {
    public GameObject ModePanel;
    public GameObject SettingPanel;
    public void SelectMode()
    {
        ModePanel.SetActive(true);

    }

    public void BackSelect()
    {
        ModePanel.SetActive(false);
    }

    public void SettingOpen()
    {
        SettingPanel.SetActive(true);
    }

    public void SettingClose()
    {
        SettingPanel.SetActive(false);
    }

}
