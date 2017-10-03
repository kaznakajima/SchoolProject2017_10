using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // プレイヤーの位置
    public GameObject _Player;

    // カメラの参照
    public GameObject _Camera;

    // カメラの位置
    Vector3 CameraPos;

    // プレイヤーとの距離
    private Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        CameraPos = new Vector3(_Camera.transform.position.x, _Camera.transform.position.y, 0);

        // プレイヤーとカメラの距離を測る
        offset = _Camera.transform.position - CameraPos;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Vector3 newPosition = _Camera.transform.position;

        // プレイヤーが上に行き過ぎないようにする
        if (_Player.transform.position.y > newPosition.y)
            // カメラ移動
            newPosition.y = _Player.transform.position.y + offset.y;

        // 移動実行
        _Camera.transform.position = Vector3.Lerp(_Camera.transform.position, newPosition, 10.0f * Time.deltaTime);
	}
}
