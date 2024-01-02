using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UniRx;

/// <summary>
/// オブジェクトの生成を行う。
/// </summary>
public class FbxLoader : MonoBehaviourPunCallbacks
{
    private string gameObjectName;
    public GameObject Food;
    public GameObject Brush;
    public ParticleSystem Shower;
    private GameObject quadGif;
    private GameObject parentPartnerTable;
    public GameObject partnerTable;

    private void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();

        GameObject parentPartnerTable = GameObject.Find("ParentPartnerTable");
        partnerTable = parentPartnerTable.transform.Find("ChildPartnerTable").gameObject;
    }

    /// <summary>
    ///vマスターサーバーへの接続が成功した時に呼ばれるコールバック 
    /// </summary>
    public override void OnConnectedToMaster()
    {
        quadGif = GameObject.Find("Quad");

        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => {
            quadGif.SetActive(false);
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        });
        Debug.Log("サーバーに接続しました。。");
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        gameObjectName = new SendResult().GetResponseFileName();

        // Main Cameraを検索して取得
        Camera mainCamera = Camera.main;


        if (mainCamera != null)
        {
            // Main Cameraが向いている方向を取得
            Vector3 cameraForward = mainCamera.transform.forward;
            // Main Cameraの座標を取得
            Vector3 cameraPosition = mainCamera.transform.position;
            // Main Cameraの方向に0.5だけ進んだ位置を計算
            Vector3 spawnPosition = cameraPosition + cameraForward * 0.5f;
            Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);

            Debug.Log(gameObjectName + "を生成します。");
            GameObject gameObject = PhotonNetwork.Instantiate(gameObjectName, spawnPosition, rotation);
            
            partnerTable.transform.position = spawnPosition;
            partnerTable.SetActive(true);

            if (gameObject != null)
            {
                gameObject.name = gameObjectName;

                AddAnimatorController(gameObject);

                CharacterModel characterModel = AddCharacterModel(gameObject);
                SetInitSize(characterModel.GetGameObject());

                FoodCollisionDetection foodCollisionDetection = Food.GetComponent<FoodCollisionDetection>();
                foodCollisionDetection.SetCharacterModel(characterModel);

                BrushCollisionDetection brushCollisionDetection = Brush.GetComponent<BrushCollisionDetection>();
                brushCollisionDetection.SetCharacterModel(characterModel);

                ShowerCollisionDetection showerCollisionDetection = Shower.GetComponent<ShowerCollisionDetection>();
                showerCollisionDetection.SetCharacterModel(characterModel);

                gameObject.AddComponent<HealthMonitor>();
                AnimationTimer animationTimer = gameObject.AddComponent<AnimationTimer>();
                animationTimer.SetCharacterModel(characterModel);
                gameObject.AddComponent<NostalgicManager>();
            }
            else
            {
                Debug.LogError("FBXファイルが見つかりません: " + gameObjectName);
            }
        }
        else
        {
            Debug.LogError("メインカメラがありません");
        }
    }

    public void SetGameObjectName(string value)
    {
        gameObjectName = value;
    }

    /// <summary>
    /// 生成したObjectに対して、Animatorを追加する。
    /// </summary>
    private void AddAnimatorController(GameObject gameObject)    
    {
        Animator animator = gameObject.GetComponent<Animator>();
        RuntimeAnimatorController controller = Resources.Load("animation/"+ gameObjectName
            +" Animator Controller") as RuntimeAnimatorController;
        animator.runtimeAnimatorController = controller;
    }

    /// <summary>
    /// 生成したObjectに対して、CharacterModelを追加する。
    /// </summary>
    /// <returns>モデルのクラス</returns>
    private CharacterModel AddCharacterModel(GameObject childObject)
    {
        CharacterModel characterModel = childObject.AddComponent<CharacterModel>();
        characterModel.SetGameObject(childObject);
        return characterModel;
    }

    /// <summary>
    /// 生成した初期Objectに対して、initialPosition, Rotate, Scaleを設定する。
    /// </summary>
    private void SetInitSize(GameObject childObject)
    {
        // 初期位置
        //childObject.transform.position = new Vector3(-1, 0, 10);
        childObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        // 初期サイズ
        childObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}