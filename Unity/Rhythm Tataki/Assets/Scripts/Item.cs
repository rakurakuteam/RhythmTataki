using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    // 버튼안에 들어갈 요소들
    // Serializable 해주는 것은 직렬처리를 해주는 것이다.
    
    public string title;        // 타이틀
    public Sprite rankImage;    // 점수에 따른 별의 개수 이미지
    public Sprite titleImage;   // 타이틀 이미지

    // 클릭 이벤트
    public Button.ButtonClickedEvent onItemClick;
}
