using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cheat : Singleton<Cheat>
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (GameObject.Find("Player") != null)
            {
                GameObject.Find("Player").GetComponent<Player>().SetBomb();
            }
            if (GameObject.Find("GameManager") != null)
            {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().isBoss)
                {
                    Destroy(GameObject.Find("Boss"));
                    GameObject.Find("GameManager").GetComponent<GameManager>().GameClear();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (GameObject.Find("Player") != null)
            {
                GameDataManager.Instance.upgrade = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            // 스킬의 쿨타임 및 횟수 초기화
            if (GameObject.Find("Player") != null)
            {
                GameObject.Find("Player").GetComponent<Player>().SetMaxSkill();
            }

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            // 내구도 초기화
            if (GameObject.Find("Player") != null)
            {
                GameObject.Find("Player").GetComponent<Player>().SetMaxHp();
            }
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            // 연료 초기화
            if (GameObject.Find("Player") != null)
            {
                GameObject.Find("Player").GetComponent<Player>().SetMaxFuel();
            }
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            // 스테이지 이동
            switch (SceneManager.GetActiveScene().name)
            {
                case "Menu":
                    {
                        SceneManager.LoadScene("Stage01");
                    }
                    break;
                case "Stage01":
                    {
                        SceneManager.LoadScene("Stage02");
                    }
                    break;
                case "Stage02":
                    {
                        SceneManager.LoadScene("Menu");
                    }
                    break;
            }
        }
    }
}

