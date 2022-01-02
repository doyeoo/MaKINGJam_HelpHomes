using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapControl : MonoBehaviour
{

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.stopTimer = true;

        if (gameManager.stageIndex == 6)
            SceneManager.LoadScene("GoodEnding");
        else
            Invoke("delayNextStage", 3);

        Debug.Log("몇번?");
    }
    
    void delayNextStage()
    {
        if(gameManager.stageIndex%2==0)
            gameManager.NextStage();
    }
}
