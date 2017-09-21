using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaster : MonoBehaviour
{
    // 障害物
    public GameObject ObstacleObj;

	// Use this for initialization
	void Start ()
    {
        // プレファブを同ポジションに生成
        GameObject ObstacleChip = (GameObject)Instantiate(
            ObstacleObj,
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
            Gizmos.DrawIcon(transform.position, ObstacleObj.name, true);
        }
    }
}
