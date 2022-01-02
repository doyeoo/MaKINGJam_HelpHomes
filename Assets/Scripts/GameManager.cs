using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class Item
{
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isClicked)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isClicked = _isClicked;
    }
    public string Type, Name, Explain, Number;
    public bool isClicked;}


public class GameManager : MonoBehaviour
{
    public int itemNum;
    public int stageIndex=0;
    public GameObject[] Stages;
    public Text timeText;
    private float time;
    private int beforeClikedItemIndex;

    public TextAsset ItemDatabase;
    public List<Item> AllItemList;

    public string clickedItem;
    public int clikedItemIndex;
    string[] line;

    public Image[] InvenItem;

    public GameObject restartBtn;
    public GameObject secondClock;
    public GameObject[] curtainAnim;
    bool curtainDown;
    private Vector3 curtainTarget;

    public bool stopTimer=false;
    public int audioPlayed; // 0: 소리x, 1: 시계초침, 2: 심장박동, 3:게임오버

    //사운드
    public AudioClip audioClearClip;
    public AudioClip audioTimeRunClip;
    public AudioClip audioTimeOverClip;
    public AudioClip audioHeartbeatClip;
    public AudioClip audioCurtainClip;
    



    public void NextStage()
    {
        Debug.Log("next stage");
        // 클리어 효과음
        // SoundManager.instance.SFXPlay("audioClear", audioClearClip);
        curtainDown = false;

        
        itemNum = 0;
        stopTimer = false;
        Debug.Log(stageIndex);
        if (stageIndex < Stages.Length - 1)
        {
            
            Stages[stageIndex].SetActive(false);
            // 아이템 다시 안보이게 하기
            for (int i = 0; i < InvenItem.Length; i++)
            {
                InvenItem[i].color = new Color(1, 1, 1, 0);
            }

            stageIndex++;
            Stages[stageIndex].SetActive(true);

            // 커튼 위치 초기화
            curtainTarget.x = 0;
            curtainTarget.y = 600;

            
            time = 45f;
            secondClock.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            // curtainAnim[stageIndex / 2].transform.position = new Vector3(480, 570, 0);
        }

        else
        {
            //다끝났으면 끝내는거
            Time.timeScale = 0;
            Debug.Log("클리어");
            //버튼 텍스트 수정
        }
        Debug.Log(stageIndex);


    }

    public void Restart()
    {
        //재시작
        SceneManager.LoadScene("MainScreen");
    }

    public int addItemNum()
    {
        itemNum++;
        return itemNum;
    }


// Start is called before the first frame update
    void Awake()
    {
        audioPlayed = 0;
        //전체 아이템 리스트 불러오기
        line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
        // 아이템 안보이게
        for (int i = 0; i < InvenItem.Length; i++)
        {
            InvenItem[i].color = new Color(1, 1, 1, 0);
        }
        clikedItemIndex = 21; // 마지막에가짜값
        beforeClikedItemIndex = 21;

        time = 45f;
    }

    // 아이템 획득
    public void ItemToInven(string _name)
    {
        // 색이 차는 사이에 다른 아이템을 클릭하면 즉시 채색
        InvenItem[beforeClikedItemIndex].color = new Color(1, 1, 1, 1);
        for (int i = 0; i < line.Length - 1; i++)
        {
            if (AllItemList[i].Name == _name)
            {
                clikedItemIndex = i;
                AllItemList[clikedItemIndex].isClicked = true;
                beforeClikedItemIndex = clikedItemIndex;
                break;
            }
        }
        Debug.Log(_name);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // Item Fade Effect
        if (AllItemList[clikedItemIndex].isClicked)
        {
            Color color = InvenItem[clikedItemIndex].color;
            if (color.a < 1)
            {
                color.a += Time.deltaTime;
            }

            InvenItem[clikedItemIndex].color = color;

        }

        // curtain Down
        if(curtainDown == true)
        {
            curtainTarget.y -= 3;
            curtainAnim[stageIndex / 2].transform.localPosition = curtainTarget;
            //curtainAnim[stageIndex / 2].transform.position = Vector3.MoveTowards(curtainAnim[stageIndex / 2].transform.position, curtainTarget, 2f);
            //curtainAnim[stageIndex/2].transform.Translate(0, -2.3f, 0);
        }



        //타이머
        if (stopTimer!=true)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            timeText.text = Mathf.Ceil(time).ToString();

            if (timeText.text == "10")
            {
                if (audioPlayed == 0)
                {
                    audioPlayed = 1;
                    SoundManager.instance.SFXPlay("audioTimeRun", audioTimeRunClip);

                }
            }
            if (timeText.text == "5")
            {
                if (audioPlayed == 1)
                {
                    audioPlayed = 2;
                    SoundManager.instance.SFXPlay("audioHeartbeat", audioHeartbeatClip);
                }
            }
            if (timeText.text == "0")
            {
                if (audioPlayed == 3)
                {
                    audioPlayed = 0;
                    SoundManager.instance.SFXPlay("audioTimeOverClip", audioTimeOverClip);
                    
                }
                SceneManager.LoadScene("BadEnding");
            }
            else
            {
                ClockMove();
            }
        }
        else
        {
            // 플레이 할 때도 커튼 약간 보이게끔
            //curtainAnim.SetActive(true);
            // if (stageIndex % 2 == 1 && stageIndex != 0)
                //Invoke("CurtainDown", 1);
                // CurtainDown();
        }

    }

    //시계 초침 이동
    void ClockMove()
    {
        secondClock.transform.Rotate(0, 0, -0.16f);
    }

    public void CurtainDown()
    {
        Debug.Log("커튼다운");
        curtainDown = true;
        SoundManager.instance.SFXPlay("audioClear", audioClearClip);
        SoundManager.instance.SFXPlay("audioCurtain", audioCurtainClip);

    }

    //ESC 누르면 게임 종료
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}

