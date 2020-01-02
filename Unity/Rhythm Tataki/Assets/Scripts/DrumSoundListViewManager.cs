using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrumSoundListViewManager : MonoBehaviour
{
    public GameObject toggleItem;
    public Transform content;
    public List<InItem> toggleItemList;
    public ToggleGroup leftToggleGroup;
    public int drumType; // 왼쪽 드럼인지 오른쪽 드럼인지.

    // Start is called before the first frame update
    void Start()
    {
        // 토글 안의 내용을 바인딩하기 전에 inItem들을 셋팅
        AddListInItem();
        // 바인딩
        Binding();
    }


    // ListObject안의 Item 셋팅
    private void AddListInItem()
    {
        InItem itemTemp;

        for (int i = 0; i < PlayerInformation.drumSoundCount; i++)
        {

            itemTemp = new InItem();

            itemTemp.soundName = PlayerInformation.soundNameList[i];
            itemTemp.soundFileName = PlayerInformation.soundFileNameList[i];
            itemTemp.OnToggleClick = new Toggle.ToggleEvent();
            itemTemp.OnToggleClick.AddListener(delegate { ToggleClick_Result(); });

            // 리스트뷰의 항목인 Item을 만들어서 ItemList에 추가한다.
            toggleItemList.Add(itemTemp);
        }
    }

    // InItem 리스트의 InItem을 리스트뷰에 바인딩 함으로써
    // 실제 리스트뷰의 컨텐츠에 추가하고 화면에 보여주도록 한다.
    private void Binding()
    {
        GameObject toggleItemTemp;
        ToggleItem itemobjectTemp;

        foreach (InItem item in this.toggleItemList)
        {
            //추가할 오브젝트를 생성한다.
            toggleItemTemp = Instantiate(this.toggleItem) as GameObject;
            //오브젝트가 가지고 있는 'ToggleItem'를 찾는다.
            itemobjectTemp = toggleItemTemp.GetComponent<ToggleItem>();

            //각 속성 입력
            itemobjectTemp.soundsName.text = item.soundName;
            itemobjectTemp.soundFileName.text = item.soundFileName;
            itemobjectTemp.item.onValueChanged = item.OnToggleClick;
            // 토글 그룹설정
            itemobjectTemp.item.group = leftToggleGroup;

            //화면에 추가
            toggleItemTemp.transform.SetParent(this.content);
            // 스케일을 1로 지정한다 이것을 추가하지 않으면 프리팹의 스케일이 너무 커짐.
            toggleItemTemp.transform.localScale = Vector3.one;
            
        }
    }

    public void ToggleClick_Result()
    {
        // Debug.Log("토글버튼 클릭됨");

        GameObject obj = EventSystem.current.currentSelectedGameObject;
        ToggleItem toggleItem = obj.GetComponent<ToggleItem>();

        // 클릭된 이름의 효과음 재생하여 사용자가 어떤 소리인지 알수있게해줌
        // 재생할 파일명은 PlayerInformation의 SoundFileNameList임.
        
        if(toggleItem.item.isOn)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            // 리소스에서 오디오 파일을 불러온다.
            AudioClip audioClip = Resources.Load<AudioClip>("EffectSounds/" + toggleItem.soundFileName.text);
            audioSource.clip = audioClip;
            if (drumType == 0)
            {
                // 선택된 소리를 기본 소리로 셋팅한다
                PlayerInformation.leftDrumSound = audioClip;
            }
            else
            {
                // 선택된 소리를 기본 소리로 셋팅한다
                PlayerInformation.rightDrumSound = audioClip;
            }
            // 재생
            audioSource.Play();
        }
    }
}
