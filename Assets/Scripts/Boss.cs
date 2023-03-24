using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public GameObject explosion;

    private GameManager gameManager;
    private GameObject Player;
    public GameObject objBullet;
    public Transform BulletPoint;
    public GameObject[] item;
    public int score = 50;

    public float hp = 1.0f;
    public float maxHp = 1.0f;
    public Image Hp;
    public float bossSize = 1.0f;
    public float bossSizeAdd = 0.1f;

    public enum BossPattern
    {
        None,
        Init,
        Missile01,
        Missile02,
    }

    public BossPattern pattern = BossPattern.None;
    public int bossMissile01MaxNum = 20;
    public int bossMissile01Num = 0;
    public float bossMissile01Delay = 0.5f;
    public float bossMissile01Time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gamManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gamManagerObject != null)
        {
            gameManager = gamManagerObject.GetComponent<GameManager>();
        }
        if (gameManager == null)
        {
            Debug.Log("게임 매니저가 존재하지 않습니다.");
        }

        Player = GameObject.FindGameObjectWithTag("Player");

        if (gameManager == null)
        {
            Debug.LogError("게임 매니저가 존재하지 않습니다.");
        }


    }

    // Update is called once per frame
    void Update()
    {
        switch (pattern)
        {
            case BossPattern.Init:
                {
                    if (this.transform.localScale.x < bossSize)
                    {
                        this.transform.localScale += Vector3.one * bossSizeAdd;
                    }
                    else
                    {
                        pattern = BossPattern.Missile01;
                        this.transform.localScale = Vector3.one * bossSize;
                    }
                }
                break;
            case BossPattern.Missile01:
                {
                    bossMissile01Time += Time.deltaTime;
                    if (bossMissile01Time > bossMissile01Delay && bossMissile01Num < bossMissile01MaxNum)
                    {
                        bossMissile01Time = 0f;
                        BossFireBullet(0);
                        bossMissile01Num++;
                        if (bossMissile01Num == bossMissile01MaxNum)
                        {
                            pattern = BossPattern.Missile02;
                            bossMissile01Num = 0;
                        }
                    }
                }
                break;
            case BossPattern.Missile02:
                {
                    bossMissile01Time += Time.deltaTime;
                    if (bossMissile01Time > bossMissile01Delay && bossMissile01Num < bossMissile01MaxNum)
                    {
                        bossMissile01Time = 0f;
                        BossFireBullet(1);
                        bossMissile01Num++;
                        if (bossMissile01Num == bossMissile01MaxNum)
                        {
                            pattern = BossPattern.Missile01;
                            bossMissile01Num = 0;
                        }
                    }
                }
                break;

        }
    }

    void BossFireBullet(int num)
    {
        if (Player != null)
        {
            switch (num)
            {
                case 0:
                    {

                        {
                            GameObject bullet = Instantiate(objBullet, BulletPoint.transform.position, this.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBullect(Player.transform.position);
                            // 총알 소리
                            //this.GetComponent<AudioSource>().Play();
                        }
                    }
                    break;
                case 1:
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            GameObject bullet = Instantiate(objBullet, BulletPoint.position, this.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBullect(Player.transform.position + Vector3.forward + new Vector3(-1 + i, 0, 0));
                        }
                    }
                    break;
            }
        }
    }

    public void SetScore()
    {
        gameManager.SetScore(score);

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        int itemNum = gameManager.CreateItem();
        if (itemNum != -1)
        {
            Instantiate(item[itemNum], this.transform.position, item[itemNum].transform.rotation);
        }

        Destroy(gameObject);
    }

    public void BossInit()
    {
        pattern = BossPattern.Init;

    }

    public void SetDamage(float damage)
    {

        hp -= damage;
        Hp.fillAmount = hp / maxHp;
        if (hp < 1.0f)
        {
            gameManager.SetScore(score);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            int itemNum = gameManager.CreateItem();
            if (itemNum != -1)
            {
                Instantiate(item[itemNum], this.transform.position, item[itemNum].transform.rotation);
            }
            //if (other.tag == "Player")
            //{
            //    gameManager.GameOver();
            //}
            gameManager.GameClear();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Barricate") || other.CompareTag("Item") || !gameManager.isBoss)
        {
            return;
        }

        if (other.CompareTag("Bullet"))
        {
            hp -= 1f;
            Hp.fillAmount = hp / maxHp;
            if (hp < 1.0f)
            {
                gameManager.SetScore(score);
                if (explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }

                int itemNum = gameManager.CreateItem();
                if (!other.CompareTag("Player") && itemNum != -1)
                {
                    Instantiate(item[itemNum], this.transform.position, item[itemNum].transform.rotation);
                }
                gameManager.GameClear();
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }
}

