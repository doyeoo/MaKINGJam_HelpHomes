using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        Invoke("SceneMoveDelay", 0.25f); // 0.5초 후 이동
    }

    public void SceneMoveDelay()
    {
        switch (this.gameObject.name)
        {
            case "HowToPlayBtn":
                SceneManager.LoadScene("HowToPlay");
                break;
            case "GameStartBtn":
                SceneManager.LoadScene("Map");
                break;
            case "RestartBtn":
                SceneManager.LoadScene("Map");
                break;
        }
    }
}
