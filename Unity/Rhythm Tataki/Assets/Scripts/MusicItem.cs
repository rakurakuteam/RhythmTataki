using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class MusicItem
{
    // 버튼안에 들어갈 요소들

    public string musicName;

    // 클릭 이벤트
    public Button.ButtonClickedEvent onItemClick;
}
