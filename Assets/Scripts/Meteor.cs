using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public GameObject explosion;

    public int scoreValue;
    private GameManager gameManager;
    public int score = 20;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.CompareTag("Barricate") || other.CompareTag("Item"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Bullet"))
        {
            gameManager.SetScore(score);
        }

        //if (other.tag == "Player")
        //{
        //    gameManager.GameOver();
        //}

        //Destroy(other.gameObject);
        gameManager.listEnemys.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void SetScore()
    {
        gameManager.SetScore(score);

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        //gameManager.listEnemys.Remove(this.gameObject);
        Destroy(gameObject);
    }
}

