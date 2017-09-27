using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaster : MonoBehaviour
{
    // 障害物
    public GameObject[] ObstacleObj;

    int Objnum = 0;

    // Use this for initialization
    void Start ()
    {

        Camera _mainCamera = Camera.main;

        if(_mainCamera.transform.position.y >= 0)
        {
            Objnum = 0;
        }
        if(_mainCamera.transform.position.y >= 100)
        {
            Objnum = 1;
        }

        // プレファブを同ポジションに生成
        GameObject ObstacleChip = (GameObject)Instantiate(
            ObstacleObj[Objnum],
            Vector3.zero,
            Quaternion.identity);

        // 一緒に削除されるように生成した足場を子に設定
        ObstacleChip.transform.SetParent(transform, false);
    }

    // ギズモの表示
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.25f);

        // プレファブのアイコン表示
        if (ObstacleObj != null)
        {
            Gizmos.DrawIcon(transform.position, ObstacleObj[Objnum].name, true);
        }
    }
}
