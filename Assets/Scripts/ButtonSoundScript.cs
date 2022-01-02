using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundScript : MonoBehaviour
{
    public AudioClip audioButtonclip;

    void OnMouseDown()
    {
        // 효과음
        SoundManager.instance.SFXPlay("audioButton", audioButtonclip);
    }
}
