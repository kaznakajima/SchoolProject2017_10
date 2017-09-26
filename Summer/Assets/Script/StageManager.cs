using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ステージのサイズ
    const int StageTipSize = 10;

    int currentTipIndex;
    //次に生成するステージの番号
    int nextStageTip;

    public Transform character;
    public GameObject[] stageTips;
    public GameObject[] obstacleTips;
    public int startTipIndex;
    public int preInstantiate;
    public List<GameObject> generatedStageList = new List<GameObject>();
    public List<GameObject> generatedObstacleList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        currentTipIndex = startTipIndex - 1;

        //UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.hit == true)
            return;

        // キャラクターが存在するなら処理を行う
        if (character != null)
        {
            // キャラクターの位置から現在のステージチップのインデックスを計算
            int charaPositionIndex = (int)(character.transform.position.y / StageTipSize);

            // 次のステージチップに入ったらステージの更新処理を行う
            if (charaPositionIndex + preInstantiate > currentTipIndex)
            {
                UpdateStage(charaPositionIndex + preInstantiate);
            }
        }
    }

    // 指定のIndexまでのステージチップを生成して、管理下に置く
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        // 指定のステージチップまでを作成
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            GameObject obstacleObject = GenerateObstacle(i);

            // 生成したステージチップを管理リストに追加し
            generatedStageList.Add(stageObject);
            generatedObstacleList.Add(obstacleObject);
        }

        // ステージ保持上限内になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOlderStage();

        currentTipIndex = toTipIndex;
    }

    // ステージチップの選択(ランダム)
    GameObject GenerateStage(int tipIndex)
    {
        nextStageTip = Random.Range(0, stageTips.Length);

        GameObject stageObject = Instantiate(
            stageTips[nextStageTip],
            new Vector3(0, tipIndex * StageTipSize, 0),
            Quaternion.identity
            );



        return stageObject;
    }

    // ステージチップに対応した障害物の生成
    GameObject GenerateObstacle(int tipIndex)
    {
        int nextObstacleTip = nextStageTip;

        GameObject obstacleObject = Instantiate(
            obstacleTips[nextObstacleTip],
            new Vector3(0, tipIndex * StageTipSize, 0),
            Quaternion.identity);

        return obstacleObject;
    }

    // 1番古いステージを削除
    void DestroyOlderStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

    void DestroyOlderObstacle()
    {
        GameObject oldObstacle = generatedObstacleList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldObstacle);
    }
}
