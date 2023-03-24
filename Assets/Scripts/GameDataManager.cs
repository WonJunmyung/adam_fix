using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
// 둘중에 하나 적용
//using Unity.Plastic.Newtonsoft.Json;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;


public class GameDataManager : Singleton<GameDataManager>
{
    public int isMusic = 0;
    public int isSound = 0;
    public float gameTime = 0;
    public int gameScore;
    public string curId;
    public GameDataGroup gameDataGroup = new GameDataGroup();

    // 플레이어에 대한 정보
    public float bombTime = 2.0f;
    public float bombing = 0f;
    public bool isBomb = false;
    public float NoBombSpeed = 1.0f;

    public float fixTime = 60.0f;
    public float fixing = 0f;
    public bool isFix = false;
    public float NoFixSpeed = 1.0f;

    public float hp = 5f;
    public float maxHp = 5f;
    public float fuel = 10f;
    public float maxFule = 10f;
    public float fuelTime = 5.0f;
    public float fuelTimer = 0f;


    public int upgrade = 0;
    public int maxUpgrade = 3;

    public int bomb = 0;
    public int maxBomb = 3;


    public void SetInitPlayer()
    {
        bombTime = 2.0f;
        bombing = 0f;
        isBomb = false;
        NoBombSpeed = 1.0f;

        fixTime = 60.0f;
        fixing = 0f;
        isFix = false;
        NoFixSpeed = 1.0f;

        hp = 5f;
        maxHp = 5f;
        fuel = 10f;
        maxFule = 10f;
        fuelTime = 5.0f;
        fuelTimer = 0f;

        upgrade = 0;
        maxUpgrade = 3;

        bomb = 0;
        maxBomb = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadData()
    {

        if (PlayerPrefs.HasKey("GameData"))
        {
            GameDataGroup load = JsonConvert.DeserializeObject<GameDataGroup>(PlayerPrefs.GetString("GameData"));
            gameDataGroup.rank = load.rank.OrderBy(x => x.stageScore).Reverse().ToList();
        }
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }

        isMusic = PlayerPrefs.GetInt("Music");
        isSound = PlayerPrefs.GetInt("Sound");
    }

    public void SaveData(string _id, float _stageTime, int _stageScore)
    {
        GameData gameData = new GameData();
        gameData.SetData(_id, _stageTime, _stageScore);
        gameDataGroup.rank.Add(gameData);
        gameDataGroup.rank = gameDataGroup.rank.OrderBy(x => x.stageScore).Reverse().ToList();
        PlayerPrefs.SetString("GameData", JsonConvert.SerializeObject(gameDataGroup));
    }



    public void SetID(string _id)
    {
        curId = _id;
    }
}

