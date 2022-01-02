using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    private Image image;
    private GameObject panel;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color color = image.color;

        if (color.a > 0)
        {
            color.a -= Time.deltaTime;
        }
        else
            gameObject.SetActive(false);

        image.color = color;

        if (SceneManager.GetActiveScene().name == "Map")
            Invoke("StartGame", 3);
    }
    void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
