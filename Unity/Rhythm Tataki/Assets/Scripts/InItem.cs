using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class InItem
{
    public string soundName;
    public string soundFileName;

    // 이벤트
    public Toggle.ToggleEvent OnToggleClick;
}
