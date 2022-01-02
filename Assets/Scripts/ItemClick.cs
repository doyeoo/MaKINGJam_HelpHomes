using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemClick : MonoBehaviour
{
    public GameManager gameManager;
    public ChangeScene ChangeScene;

    public AudioClip audioGetItemclip;
    public AudioClip audioClearclip;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        ChangeScene = GetComponent<ChangeScene>();
        // randomLocation();
    }

    //void randomLocation()
    //{
    //    //위치 랜덤하게
    //    float min = -2.0f;
    //    float max = 4.0f;
    //    float randomX = Random.Range(min, max);
    //    float randomY = Random.Range(min, max/10.0f);
    //    float randomZ = 0;
    //    this.transform.position = new Vector3(randomX, randomY, randomZ);
    //}

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 효과음
            SoundManager.instance.SFXPlay("audioGetItem", audioGetItemclip);

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            

            this.gameObject.SetActive(false);


            
            gameManager.ItemToInven(this.name);
            gameManager.itemNum++;

            //itemNum 개수 다 차면 다음 페이지로 코드 더러움
            if ((gameManager.itemNum == 5 && gameManager.stageIndex==1) || (gameManager.itemNum == 7 && gameManager.stageIndex == 3) || (gameManager.itemNum == 9 && gameManager.stageIndex == 5))
            {
                // SoundManager.instance.SFXPlay("audioClear", audioClearclip);
                Debug.Log("넘어가라");
                gameManager.CurtainDown();
                Invoke("delayNextStage", 3.5f);
                gameManager.stopTimer = true;
            }

            if (gameManager.itemNum == 9 && gameManager.stageIndex == 5)
                SceneManager.LoadScene("GoodEnding");
        }
    }
    void delayNextStage()
    {
        gameManager.NextStage();
    }

}
