using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject explosion;
    // 총알 딜레이
    public float bulletTime = 0.1f;
    // 총알 딜레이만큼 시간이 지나갔는지 체크
    public float reloadTime = 0f;



    Rigidbody thisRigi;
    // 플레이어의 이동속도
    public float speed = 2.0f;
    // 총알 프리팹
    public GameObject objBullet;
    // 총알이 생성될 위치
    public Transform BulletPoint;


    public float explosionDamage = 10.0f;
    GameManager gameManager;


    public Image Fuel;
    public Image Hp;
    public Image Bomb;
    public Image Fix;

    public Text NoBomb;
    public Text BombNum;
    public Text NoFix;

    public GameObject effectBomb;


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

        thisRigi = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        fireBullet();
        if (gameManager.gameStatus == GameManager.GameStatus.play)
        {
            DecreaseFuel();
        }
    }

    void fireBullet()
    {
        reloadTime += Time.deltaTime;
        GameDataManager.Instance.bombing += Time.deltaTime;
        GameDataManager.Instance.fixing += Time.deltaTime;

        if (Input.GetButton("Fire1") && (bulletTime <= reloadTime))
        {
            reloadTime = 0f;
            switch (GameDataManager.Instance.upgrade)
            {
                case 0:
                    {
                        GameObject bullet = Instantiate(objBullet, BulletPoint.position, this.transform.rotation);
                        bullet.GetComponent<Bullet>().SetBullect(BulletPoint.position + Vector3.forward);
                    }
                    break;
                case 1:
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            GameObject bullet = Instantiate(objBullet, BulletPoint.position, this.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBullect(BulletPoint.position + Vector3.forward + new Vector3(-1 + i, 0, 0));
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Vector3 point = BulletPoint.position + new Vector3(-1 + ((float)i / 2.0f), 0, 0);
                            GameObject bullet = Instantiate(objBullet, point, this.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBullect(point + Vector3.forward);
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            GameObject bullet = Instantiate(objBullet, BulletPoint.position, this.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBullect(BulletPoint.position + Vector3.forward + new Vector3(-1 + ((float)i / 2.0f), 0, 0));
                        }
                    }
                    break;
            }
            // 총알 소리
            if (GameDataManager.Instance.isSound == 1)
            {
                this.GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetButtonDown("Fire2") && (GameDataManager.Instance.bombTime <= GameDataManager.Instance.bombing))
        {
            if (GameDataManager.Instance.bomb == 0)
            {
                GameDataManager.Instance.isBomb = true;
                //bombing = 0;
            }
            else
            {
                GameDataManager.Instance.bomb--;
                BombNum.text = "x" + GameDataManager.Instance.bomb;
                GameDataManager.Instance.bombing = 0;
                Instantiate(effectBomb, effectBomb.transform.position, effectBomb.transform.rotation);

                for (int i = 0; i < gameManager.listEnemys.Count; i++)
                {
                    if (gameManager.listEnemys[i].GetComponent<Enemy>() == null)
                    {
                        gameManager.listEnemys[i].GetComponent<Meteor>().SetScore();
                    }
                    else
                    {
                        gameManager.listEnemys[i].GetComponent<Enemy>().SetScore();
                    }
                }
                gameManager.listEnemys.Clear();

                GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < objs.Length; i++)
                {
                    Destroy(objs[i]);
                }
                if (gameManager.isBoss)
                {
                    GameObject[] boss = GameObject.FindGameObjectsWithTag("Boss");


                    for (int i = 0; i < boss.Length; i++)
                    {

                        boss[i].GetComponent<Boss>().SetDamage(explosionDamage);
                    }
                }
            }
        }
        else if (Input.GetButtonDown("Fire2") && (GameDataManager.Instance.bombTime > GameDataManager.Instance.bombing))
        {
            GameDataManager.Instance.isBomb = true;
        }

        if (GameDataManager.Instance.isBomb)
        {
            NoBomb.color = new Color(NoBomb.color.r, NoBomb.color.g, NoBomb.color.b, NoBomb.color.a + Time.deltaTime * GameDataManager.Instance.NoBombSpeed);
            if (NoBomb.color.a >= 1.0f)
            {
                GameDataManager.Instance.isBomb = false;
                NoBomb.color = new Color(NoBomb.color.r, NoBomb.color.g, NoBomb.color.b, 0);
            }
        }
        Bomb.fillAmount = GameDataManager.Instance.bombing / GameDataManager.Instance.bombTime;



        if (Input.GetButtonDown("Fire3") && (GameDataManager.Instance.fixTime <= GameDataManager.Instance.fixing))
        {
            if (GameDataManager.Instance.hp < GameDataManager.Instance.maxHp)
            {
                GameDataManager.Instance.hp = GameDataManager.Instance.maxHp;
            }
            Hp.fillAmount = GameDataManager.Instance.hp / GameDataManager.Instance.maxHp;
            GameDataManager.Instance.fixing = 0;
        }
        else if (Input.GetButtonDown("Fire3") && (GameDataManager.Instance.fixTime > GameDataManager.Instance.fixing) && !GameDataManager.Instance.isFix)
        {
            GameDataManager.Instance.isFix = true;
        }


        if (GameDataManager.Instance.isFix)
        {
            NoFix.color = new Color(NoFix.color.r, NoFix.color.g, NoFix.color.b, NoFix.color.a + Time.deltaTime * GameDataManager.Instance.NoFixSpeed);
            if (NoFix.color.a >= 1.0f)
            {
                GameDataManager.Instance.isFix = false;
                NoFix.color = new Color(NoFix.color.r, NoFix.color.g, NoFix.color.b, 0);
            }
        }
        Fix.fillAmount = GameDataManager.Instance.fixing / GameDataManager.Instance.fixTime;
    }

    public void SetBomb()
    {
        for (int i = 0; i < gameManager.listEnemys.Count; i++)
        {
            if (gameManager.listEnemys[i].GetComponent<Enemy>() == null)
            {
                gameManager.listEnemys[i].GetComponent<Meteor>().SetScore();
            }
            else
            {
                gameManager.listEnemys[i].GetComponent<Enemy>().SetScore();
            }
        }
        gameManager.listEnemys.Clear();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name == "Boss")
            {
                if (gameManager.isBoss)
                {
                    objs[i].GetComponent<Enemy>().SetDamage(explosionDamage);
                }
            }
            else
            {
                Destroy(objs[i]);
            }
        }
    }

    void DecreaseFuel()
    {
        GameDataManager.Instance.fuelTimer += Time.deltaTime;

        if (GameDataManager.Instance.fuelTimer > GameDataManager.Instance.fuelTime)
        {
            GameDataManager.Instance.fuelTimer = 0f;
            GameDataManager.Instance.fuel -= 1.0f;
            Fuel.fillAmount = GameDataManager.Instance.fuel / GameDataManager.Instance.maxFule;
            if (GameDataManager.Instance.fuel == 0)
            {
                if (explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }
                gameManager.GameOver();
                Destroy(gameObject);
            }
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0.0f, moveZ);
        thisRigi.velocity = move * speed;

        // 현재 플레이의 위치의 월드 좌표계를 스크린 좌표계로 바꾼다.
        Vector3 posInWorld = Camera.main.WorldToScreenPoint(this.transform.position);

        // 스크린 좌표계에서 움직일 수 있는 범위를 제한한다.
        float posX = Mathf.Clamp(posInWorld.x, 0, Screen.width);
        float posZ = Mathf.Clamp(posInWorld.y, 0, Screen.height);

        // 제한된 이동을 다시 월드 좌표계로 변경한다.
        Vector3 posInScreen = Camera.main.ScreenToWorldPoint(new Vector3(posX, posZ, 0));

        // 이동시킨다.
        thisRigi.position = new Vector3(posInScreen.x, 0, posInScreen.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Barricate"))
        {
            return;
        }
        if (other.CompareTag("Item"))
        {
            switch (other.GetComponent<Item>().itemStatus)
            {
                case ItemStatus.fuel:
                    GameDataManager.Instance.fuelTimer = 0;
                    GameDataManager.Instance.fuel = GameDataManager.Instance.maxFule;
                    Fuel.fillAmount = 1.0f;
                    break;
                case ItemStatus.hp:
                    if (GameDataManager.Instance.hp < GameDataManager.Instance.maxHp)
                    {
                        GameDataManager.Instance.hp += 1f;
                    }
                    Hp.fillAmount = GameDataManager.Instance.hp / GameDataManager.Instance.maxHp;
                    break;
                case ItemStatus.upgrade:
                    if (GameDataManager.Instance.upgrade < GameDataManager.Instance.maxUpgrade)
                    {
                        GameDataManager.Instance.upgrade++;
                    }
                    break;
                case ItemStatus.bomb:
                    if (GameDataManager.Instance.bomb < GameDataManager.Instance.maxBomb)
                    {
                        GameDataManager.Instance.bomb++;
                    }
                    BombNum.text = "x" + GameDataManager.Instance.bomb;
                    break;
            }
            Destroy(other.gameObject);
            return;

        }

        if (other.CompareTag("Enemy"))
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            GameDataManager.Instance.hp -= 1f;
            Hp.fillAmount = GameDataManager.Instance.hp / GameDataManager.Instance.maxHp;
            if (GameDataManager.Instance.hp < 1.0f)
            {
                gameManager.GameOver();
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            GameDataManager.Instance.hp -= 1f;
            Hp.fillAmount = GameDataManager.Instance.hp / GameDataManager.Instance.maxHp;
            if (GameDataManager.Instance.hp < 1.0f)
            {
                gameManager.GameOver();
                Destroy(gameObject);
            }
        }
    }
    public void SetMaxHp()
    {
        GameDataManager.Instance.hp = GameDataManager.Instance.maxHp;
        Hp.fillAmount = GameDataManager.Instance.hp / GameDataManager.Instance.maxHp;
    }

    public void SetMaxFuel()
    {
        GameDataManager.Instance.fuel = GameDataManager.Instance.maxFule;
        Fuel.fillAmount = 1.0f;
    }

    public void SetMaxSkill()
    {

        GameDataManager.Instance.bomb = GameDataManager.Instance.maxBomb;
        BombNum.text = "x" + GameDataManager.Instance.bomb;
        GameDataManager.Instance.bombing = GameDataManager.Instance.bombTime;
        Bomb.fillAmount = GameDataManager.Instance.bombing / GameDataManager.Instance.bombTime;

        GameDataManager.Instance.fixing = GameDataManager.Instance.fixTime;
        Fix.fillAmount = GameDataManager.Instance.fixing / GameDataManager.Instance.fixTime;
    }
}


