using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuSiderUpdate : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;

    [SerializeField] CannonInMenu cannon;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("sound") || !PlayerPrefs.HasKey("music") || !PlayerPrefs.HasKey("SaveSense"))
        {
            PlayerPrefs.SetFloat("sound", 0.5f);
            _mixer.SetFloat("sound", Mathf.Log10(0.5f) * 30f);
            PlayerPrefs.SetFloat("music", 0.5f);
            _mixer.SetFloat("music", Mathf.Log10(0.5f) * 30f);
            PlayerPrefs.SetFloat("SaveSense", 50f);
            cannon.SetSensitvity(50f);
        }
        else
        {
            float soundVolume = PlayerPrefs.GetFloat("sound");
            _mixer.SetFloat("sound", Mathf.Log10(soundVolume) * 30f);
            float musicVolume = PlayerPrefs.GetFloat("music");
            _mixer.SetFloat("music", Mathf.Log10(musicVolume) * 30f);
            float sensitivity = PlayerPrefs.GetFloat("SaveSense");
            cannon.SetSensitvity(sensitivity);
        }

    }
}
