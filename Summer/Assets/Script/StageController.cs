using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    // ステージの足場
    public GameObject Prefab;

	// Use this for initialization
	void Start ()
    {
        // プレファブを同ポジションに生成
        GameObject StageChip = (GameObject)Instantiate(
            Prefab,
            Vector3.zero,
            Quaternion.identity);

        // 一緒に削除されるように生成した足場を子に設定
        StageChip.transform.SetParent(transform, false);
	}

    // ギズモの表示
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.25f);

        // プレファブのアイコン表示
        if(Prefab != null)
        {
            Gizmos.DrawIcon(transform.position, Prefab.name, true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
