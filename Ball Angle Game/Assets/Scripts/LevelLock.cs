using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLock : MonoBehaviour
{
    private static LevelLock instance;
    public static LevelLock Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPref();
    }

    // Update is called once per frame
    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void Win(int level)
    {
        if (level == 1)
            PlayerPrefs.SetInt("level2", 1);
        else if (level == 2)
            PlayerPrefs.SetInt("level3", 1);
    }

    public void LoadPref()
    {
        if (!PlayerPrefs.HasKey("level2") || !PlayerPrefs.HasKey("level3"))
        {
            PlayerPrefs.SetInt("level2", 0);
            PlayerPrefs.SetInt("level3", 0);
        }
    }

    public void CheckPref()
    {
        Debug.Log(PlayerPrefs.GetInt("level2"));
        Debug.Log(PlayerPrefs.GetInt("level3"));
    }

    public void ResetPref()
    {
        PlayerPrefs.SetInt("level2", 0);
        PlayerPrefs.SetInt("level3", 0);
    }

    public void EnterLevelTwo()
    {
        if (PlayerPrefs.GetInt("level2") == 1)
        {

        }

    }
}
