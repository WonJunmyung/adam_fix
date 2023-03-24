using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject MenuBack;
    public GameObject Manual;
    public GameObject Story;
    public GameObject Rank;
    public GameObject Setting;

    public Image BackMusic;
    public Image BackSound;


    public Sprite[] OnOff;

    public Text rankText;
    public AudioSource backMusicSource;

    public Text textID;


    // Start is called before the first frame update
    void Start()
    {

        BackMusic.sprite = OnOff[GameDataManager.Instance.isMusic];
        BackSound.sprite = OnOff[GameDataManager.Instance.isSound];

        if (backMusicSource != null && GameDataManager.Instance.isMusic == 1)
        {
            backMusicSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene("stage01");
        }
    }

    public void BtnStart()
    {
        SceneManager.LoadScene("stage01");
        GameDataManager.Instance.SetInitPlayer();
    }

    public void BtnManual()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenManual", 1.5f);

    }

    public void BtnStory()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenStory", 1.5f);
    }
    public void BtnRank()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenRank", 1.5f);
    }

    public void BtnSetting()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenSetting", 1.5f);
        BackMusic.sprite = OnOff[GameDataManager.Instance.isMusic];
        BackSound.sprite = OnOff[GameDataManager.Instance.isSound];
    }

    public void BtnExit()
    {
        Application.Quit();
    }

    void OpenManual()
    {
        Manual.SetActive(true);
        Manual.GetComponent<Animator>().SetTrigger("Open");
    }

    void OpenMenuBack()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Open");
    }

    void OpenStory()
    {
        Story.SetActive(true);
        Story.GetComponent<Animator>().SetTrigger("Open");
    }

    void OpenRank()
    {
        Rank.SetActive(true);
        Rank.GetComponent<Animator>().SetTrigger("Open");

        if (GameDataManager.Instance.gameDataGroup.rank.Count != 0)
        {
            rankText.text = "";
            for (int i = 0; i < GameDataManager.Instance.gameDataGroup.rank.Count; i++)
            {
                rankText.text += GameDataManager.Instance.curId + "  " + (int)(GameDataManager.Instance.gameDataGroup.rank[i].stageTime / 60.0f) +
                    " `` "
                    + (int)(GameDataManager.Instance.gameDataGroup.rank[i].stageTime % 60.0f)
                    + "    SCORE : " + GameDataManager.Instance.gameDataGroup.rank[i].stageScore + "\n";
                ;
            }
        }
    }

    void OpenSetting()
    {
        Setting.SetActive(true);
        Setting.GetComponent<Animator>().SetTrigger("Open");
    }



    public void BtnBack(int num)
    {
        switch (num)
        {
            case 0: // MENUAL
                Manual.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;
            case 1: // STORY
                Story.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;
            case 2: // RANK
                Rank.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;
            case 3:
                Setting.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;
        }
    }

    public void BtnBackMusic()
    {

        if (GameDataManager.Instance.isMusic == 0)
        {
            GameDataManager.Instance.isMusic = 1;
        }
        else
        {
            GameDataManager.Instance.isMusic = 0;
        }
        PlayerPrefs.SetInt("Music", GameDataManager.Instance.isMusic);
        BackMusic.sprite = OnOff[GameDataManager.Instance.isMusic];

    }

    public void BtnBackSound()
    {
        if (GameDataManager.Instance.isSound == 0)
        {
            GameDataManager.Instance.isSound = 1;
        }
        else
        {
            GameDataManager.Instance.isSound = 0;
        }
        PlayerPrefs.SetInt("Sound", GameDataManager.Instance.isSound);
        BackSound.sprite = OnOff[GameDataManager.Instance.isSound];
    }

    public void SetID()
    {
        GameDataManager.Instance.SetID(textID.text);
    }
}

