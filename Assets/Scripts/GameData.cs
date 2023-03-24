using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // 아이디
    public string id;
    // 스테이지 플레이 시간
    public float stageTime;
    // 스테이지별 획득 점수
    public int stageScore;

    public void SetData(string _id, float _stageTime, int _stageScore)
    {
        id = _id;
        stageTime = _stageTime;
        stageScore = _stageScore;
    }
}
[Serializable]
public class GameDataGroup
{
    public List<GameData> rank;
}
