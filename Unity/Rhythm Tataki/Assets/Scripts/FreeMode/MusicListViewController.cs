using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MusicListViewController : MonoBehaviour
{
    public GameObject musicListObject;
    public List<MusicItem> musicItemList;
    public Transform content;

    public List<string> musicFileName;

    // Start is called before the first frame update
    void Start()
    {
        // Free Select Song Manager에 있는 리스트를 가지고온다.
        musicFileName = GameObject.Find("Free Select Song Manager").GetComponent<FreeSelectSongManager > ().musicFileNameList;

        AddListInItem();

        Binding();
    }

    // ListObject안의 Item 셋팅
    public void AddListInItem()
    {
        MusicItem itemTemp;

        for (int i = 0; i < musicFileName.Count; i++)
        {

            itemTemp = new MusicItem();

            itemTemp.musicName = musicFileName[i];
            itemTemp.onItemClick = new Button.ButtonClickedEvent();
            itemTemp.onItemClick.AddListener(delegate { ToggleClick_Result(); });

            // 리스트뷰의 항목인 Item을 만들어서 ItemList에 추가한다.
            musicItemList.Add(itemTemp);
        }
    }

    // InItem 리스트의 InItem을 리스트뷰에 바인딩 함으로써
    // 실제 리스트뷰의 컨텐츠에 추가하고 화면에 보여주도록 한다.
    public void Binding()
    {
        GameObject btnItemTemp;
        MusicListObject itemobjectTemp;

        foreach (MusicItem item in this.musicItemList)
        {
            //추가할 오브젝트를 생성한다.
            btnItemTemp = Instantiate(this.musicListObject) as GameObject;
            //오브젝트가 가지고 있는 'MusicListObject'를 찾는다.
            itemobjectTemp = btnItemTemp.GetComponent<MusicListObject>();

            //각 속성 입력
            itemobjectTemp.musicName.text = item.musicName;
            itemobjectTemp.item.onClick = item.onItemClick;

            //화면에 추가
            itemobjectTemp.transform.SetParent(this.content);
            // 스케일을 1로 지정한다 이것을 추가하지 않으면 프리팹의 스케일이 너무 커짐.
            itemobjectTemp.transform.localScale = Vector3.one;

        }
    }

    public void ToggleClick_Result()
    {
        Debug.Log("버튼 클릭됨");

        GameObject obj = EventSystem.current.currentSelectedGameObject;
        MusicListObject listObject = obj.GetComponent<MusicListObject>();

        // 클릭된 이름의 효과음 재생하여 사용자가 어떤 소리인지 알수있게해줌
        // 재생할 파일명은 PlayerInformation의 SoundFileNameList임.

        //if (toggleItem.item.isOn)
        //{
        //    AudioSource audioSource = GetComponent<AudioSource>();
        //    // 리소스에서 오디오 파일을 불러온다.
        //    AudioClip audioClip = Resources.Load<AudioClip>("EffectSounds/" + toggleItem.soundFileName.text);
        //    audioSource.clip = audioClip;
        //    if (drumType == 0)
        //    {
        //        // 선택된 소리를 기본 소리로 셋팅한다
        //        PlayerInformation.leftDrumSound = audioClip;
        //    }
        //    else
        //    {
        //        // 선택된 소리를 기본 소리로 셋팅한다
        //        PlayerInformation.rightDrumSound = audioClip;
        //    }
        //    // 재생
        //    audioSource.Play();
        //}
    }
}
