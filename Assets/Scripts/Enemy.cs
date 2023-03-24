using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;

    private GameManager gameManager;
    public float speed;
    public float ThrowPower = 50.0f;
    private GameObject Player;
    public GameObject objBullet;
    public Transform BulletPoint;
    public float delay = 0.5f;
    public float fireRate = 1.0f;
    public GameObject[] item;
    public int score = 50;

    public float hp = 1.0f;
    public float maxHp = 1.0f;
    public Transform Hp;

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

        if (Player == null)
        {
            Debug.Log("게임 Player 존재하지 않습니다.");
        }


        this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Invoke("ThrowPlayer", Random.Range(0.5f, 1.5f));
        InvokeRepeating("fireBullet", delay, fireRate);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void fireBullet()
    {
        if (Player != null)
        {
            GameObject bullet = Instantiate(objBullet, BulletPoint.transform.position, this.transform.rotation);
            bullet.GetComponent<Bullet>().SetBullect(Player.transform.position);
            // 총알 소리
            //this.GetComponent<AudioSource>().Play();
        }
    }





    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Barricate") || other.CompareTag("Item"))
        {
            return;
        }

        if (other.CompareTag("Bullet"))
        {
            hp -= 1f;

            Hp.localScale = new Vector3((hp / maxHp), Hp.localScale.y, 1);
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
                gameManager.listEnemys.Remove(this.gameObject);

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
            gameManager.listEnemys.Remove(this.gameObject);
            Destroy(gameObject);
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

    void ThrowPlayer()
    {
        if (Player != null)
        {
            Vector3 dir = Player.transform.position - this.transform.position;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x, 0, 0) * ThrowPower);
        }
    }



    public void SetDamage(float damage)
    {
        hp -= damage;

        Hp.localScale = new Vector3((hp / maxHp), Hp.localScale.y, 1);
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
            gameManager.listEnemys.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
}

