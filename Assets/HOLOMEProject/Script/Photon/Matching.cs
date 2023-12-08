using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        Debug.Log("サーバーに接続しました。。");
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // HoloLensのカメラの位置を取得
        Transform mainCameraTransform = Camera.main.transform;


        if (mainCameraTransform != null)
        {
            // Main Camera の位置を取得
            Vector3 cameraPosition = mainCameraTransform.position;

            // 1メートル以内のランダムな位置を生成
            Vector3 randomOffset = Random.onUnitSphere * 1f;

            // 生成する位置を計算
            Vector3 position = cameraPosition + randomOffset;

            // オブジェクトを生成
            //TODO: バックエンドできるまでキャラ名を固定値ぶち込む
            Quaternion rotation = Quaternion.Euler(0, 270, 0);
            PhotonNetwork.Instantiate("MiiVerGhost", position, rotation);
            GameObject.Find("MiiVerGhost(Clone)").transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            Debug.LogError("メインカメラがありません");
        }

        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        //var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        
        Debug.Log("ルームに参加しました。");
    }
}