using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Enemys;
    public Vector3 spawnValue;
    public int enemyCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float bossTime;

    public GameObject BtnExit;
    public GameObject BtnNext;

    public int stageNum = 0;


    public float[] itemPer = new float[4];

    public List<GameObject> listEnemys = new List<GameObject>();

    public enum GameStatus
    {
        none,
        play,
        gameOver,
        gameClear
    }


    public Text TextScore;

    public Text MenuScore;
    public Text PlayTime;


    public GameStatus gameStatus = GameStatus.none;

    public GameObject MenuBack;
    float gameTime;
    public AudioSource backMusicSource;

    public Boss Boss;
    public bool isBoss = false;

    public Text Title;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameStatus.play;
        StartCoroutine(SpawnEnemy());

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
            SceneManager.LoadScene("Menu");
        }
        if (gameStatus == GameStatus.play)
        {
            gameTime += Time.deltaTime;
        }
        if (!isBoss)
        {
            if (gameTime > bossTime)
            {
                StopAllCoroutines();

                Invoke("BossInit", 2.0f);
                isBoss = true;
            }
        }
    }

    void BossInit()
    {
        Boss.BossInit();
    }


    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = Enemys[Random.Range(0, Enemys.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                listEnemys.Add(Instantiate(enemy, spawnPosition, spawnRotation));
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        }
    }



    public void GameOver()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Open");
        Title.text = "GAME OVER";
        gameStatus = GameStatus.gameOver;

        MenuScore.text = "SCORE : " + GameDataManager.Instance.gameScore;
        PlayTime.text = string.Format("{0:0}", gameTime / 60.0f) + " `` " + string.Format("{0:0}", gameTime % 60.0f);
        //GameDataManager.Instance.SaveData(GameDataManager.Instance.gameScore, " " + GameDataManager.Instance.gameTime + " " + GameDataManager.Instance.curId);
        GameDataManager.Instance.gameScore = 0;
        GameDataManager.Instance.gameTime = 0;
    }

    public void GameClear()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Open");
        Title.text = "GAME CLEAR";
        if (stageNum == 0)
        {
            BtnNext.SetActive(true);
            BtnExit.SetActive(false);
        }
        else
        {
            BtnNext.SetActive(false);
            BtnExit.SetActive(true);
        }
        gameStatus = GameStatus.gameClear;
        MenuScore.text = "SCORE : " + GameDataManager.Instance.gameScore;
        PlayTime.text = string.Format("{0:0}", gameTime / 60.0f) + " `` " + string.Format("{0:0}", gameTime % 60.0f);
    }

    public void ButtonExit()
    {
        GameExit();
    }

    public void ButtonNext()
    {
        NextStage();
    }

    public int CreateItem()
    {
        int per = Random.Range(0, 100);

        if (per > itemPer[3])
        {
            return -1;
        }
        else if (per > itemPer[2])
        {
            return 3;
        }
        else if (per > itemPer[1])
        {
            return 2;
        }
        else if (per > itemPer[0])
        {
            return 1;
        }
        else
        {
            return 0;
        }

    }

    public void SetScore(int _score)
    {

        GameDataManager.Instance.gameScore += _score;
        TextScore.text = "SCORE : " + GameDataManager.Instance.gameScore;
    }


    public void BtnMenu()
    {
        //GameDataManager.Instance.SaveData(GameDataManager.Instance.gameScore, " " + GameDataManager.Instance.gameTime + " " + GameDataManager.Instance.curId);
        GameDataManager.Instance.gameScore = 0;
        GameDataManager.Instance.gameTime = 0;
        SceneManager.LoadScene("Menu");

    }

    public void NextStage()
    {
        SceneManager.LoadScene("Stage02");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Stage01");
        GameDataManager.Instance.gameScore = 0;
        GameDataManager.Instance.gameTime = 0;
        GameDataManager.Instance.SetInitPlayer();
    }


    public void GameExit()
    {
        Application.Quit();
    }



}

