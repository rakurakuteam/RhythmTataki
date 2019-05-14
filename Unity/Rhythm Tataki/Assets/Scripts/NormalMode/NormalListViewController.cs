using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NormalListViewController : MonoBehaviour
{
    /*
     * 노멀모드에서 ListView를 셋팅해주고 버튼에 대한 이벤트를 실행하는 클래스
     * 버튼 프리팹에는 ListObject가 셋팅되어있고
     * ListObject 안에는 Item과 Title UI, Title Image UI, Rnak(Stars) Image UI가 존재
     * 그리고 Item 에는 Title에 대한 Text, Title에 대한 Image, Rank에 대한 Image가 존재
     * 따라서, 버튼을 생성하는 과정은 먼저 버튼 안의 UI들에 들어갈 
     * Text나 Image등을 버튼 개수 만큼 Item List에 셋팅해놓고
     * 버튼 UI를 셋팅할 Item List 길이 만큼 반복하면서
     * Item안의 text나 image 등을 가져와서 바인딩 하는 것이다.
     */
    public GameObject ListObject;
    public Transform Content;
    public List<Item> ItemList;

    // Start is called before the first frame update
    void Start()
    {
        // 노래 리스트 셋팅
        AddListItem();

        // 바인딩
        Binding();
    }

    // ListObject안의 Item 셋팅
    private void AddListItem()
    {
        Item itemTemp;

        for (int i = 0; i < PlayerInformation.musicCount; i++)
        {
            itemTemp = new Item();

            itemTemp.titleImage = PlayerInformation.titleSprites[i];
            itemTemp.title = PlayerInformation.titleList[i];
            itemTemp.rankImage = PlayerInformation.rankSprites[i];
            itemTemp.onItemClick = new Button.ButtonClickedEvent();
            itemTemp.onItemClick.AddListener(delegate { ItemClick_Result(); });

            // 리스트뷰의 항목인 Item을 만들어서 ItemList에 추가한다.
            ItemList.Add(itemTemp);
        }
    }

    // Item 리스트의 Item을 리스트뷰에 바인딩 함으로써
    // 실제 리스트뷰의 컨텐츠에 추가하고 화면에 보여주도록 한다.
    private void Binding()
    {
        GameObject btnItemTemp;
        ListObject itemobjectTemp;

        foreach (Item item in this.ItemList)
        {
            //추가할 오브젝트를 생성한다.
            btnItemTemp = Instantiate(this.ListObject) as GameObject;
            //오브젝트가 가지고 있는 'ListObject'를 찾는다.
            itemobjectTemp = btnItemTemp.GetComponent<ListObject>();

            //각 속성 입력
            itemobjectTemp.titleText.text = item.title;
            itemobjectTemp.titleImage.sprite = item.titleImage;
            itemobjectTemp.rankImage.sprite = item.rankImage;

            itemobjectTemp.item.onClick = item.onItemClick;

            //화면에 추가
            btnItemTemp.transform.SetParent(this.Content);
            // 스케일을 1로 지정한다 이것을 추가하지 않으면 프리팹의 스케일이 너무 커짐.
            btnItemTemp.transform.localScale = Vector3.one;
        }
    }

    // 아이템 클릭
    public void ItemClick_Result()
    {
        Debug.Log("Clicked Item");
        /*
         * 사용자가 선택한 버튼의 정보를 가지고 온다.
         * 먼저 using UnityEngine.EventSystems; namespace를 가지고오고
         * 게임오브젝트를 생성한다. 그 다음에 선택된 오브젝트에서 ListObject를 가지고 오면된다.
         */
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        ListObject listObject = obj.GetComponent<ListObject>();
        Debug.Log(listObject.item);

        // 그 뒤 UI에서 text, Image 등을 가지고 와서 PlayerInformation에 저장한다.
        PlayerInformation.selectedMusic = listObject.titleText.text;
        PlayerInformation.musicTitle = listObject.titleText.text;
        Debug.Log(listObject.titleText.text);
        PlayerInformation.titleImage = listObject.titleImage.sprite;
        Debug.Log(listObject.titleImage.sprite);

        // 마지막으로 레벨 선택 씬으로 넘겨준다.
        SceneManager.LoadScene("SetLevelScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
