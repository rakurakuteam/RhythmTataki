using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    /*
     * 오브젝트플러 방법은 게임의 노트에 적용하며
     * 수많은 노트들을 생성 하고 삭제하는 방식이 아니랃
     * 한 번에 일정수량을 만들어 놓고 활성화 비활성화 하는 식으로 하여
     * 메모리 및 게임의 성능을 더욱 좋게 하기 위해 사용한다.
     * 
     * // 2차원 List를 사용
     * Note 1 : 10개
     * Note 2 : 10개
     */
    public List<GameObject> Notes;
    private List<List<GameObject>> poolsOfNotes;
    public int noteCount = 10;
    private bool more = true; // 더 필요한 경우 생성하기 위해서

    // Start is called before the first frame update
    void Start()
    {
        // 각 노트종류별 노트를 생성한다. 
        // 2차원 리스트임
        poolsOfNotes = new List<List<GameObject>>();
        for (int i = 0; i < Notes.Count; i++)
        {
            poolsOfNotes.Add(new List<GameObject>());
            for (int n = 0; n < noteCount; n++)
            {
                GameObject obj = Instantiate(Notes[i]);
                obj.SetActive(false); // 생성 후 비활성화
                poolsOfNotes[i].Add(obj);
            }
        }
    }

    /*
     * 오브젝트풀에서 노트를 가지고옴
     * 이 함수를 호출하면 해당 노트타입의 10개의 노트들을 하나씩 확인하여
     * 비활성인 노트를 return 해준다.
     * 노트는 게임 실행시 미리 만들어 놓는데 모두 비활성 상태이다.
     * 그래서 활성상태인 노트는 넘어가고 비활성일 때 해당 노트를 return한다는 말
     */ 
    public GameObject getObject(int noteType)
    {
        
        foreach (GameObject obj in poolsOfNotes[noteType - 1])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // 새로운 노트 추가
        if (more)
        {
            GameObject obj = Instantiate(Notes[noteType - 1]);
            poolsOfNotes[noteType - 1].Add(obj);
            return obj;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
