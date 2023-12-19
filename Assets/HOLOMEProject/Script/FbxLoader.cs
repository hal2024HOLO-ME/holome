using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// オブジェクトの生成を行う。
/// </summary>
public class FbxLoader : MonoBehaviourPunCallbacks
{
    private string gameObjectName;
    public GameObject Food;
    public GameObject Brush;
    public ParticleSystem Shower;

    private void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    ///vマスターサーバーへの接続が成功した時に呼ばれるコールバック 
    /// </summary>
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
            // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
            Vector3 position = new Vector3(0f, 0.9f, -9.6f);
            Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);

            GameObject gameObject = PhotonNetwork.Instantiate(gameObjectName, position, rotation);

            if (gameObject != null)
            {
                gameObject.name = gameObjectName;
                Dictionary<string, (string[] addRigidBodyAndBoxCollider, string[] initDisplayOject)> gameObjectList = new()
                {
                    { "MiiVerNormal", (new string[] {"body", "head"} , new String[]{
                        "body",
                        "head",
                        "eye",
                        "ear",
                        "obj1",
                        "アーマチュア"
                    })},
                    { "MiiVerGhost", (new string[] {"body", "head"} , new String[]{
                        "body",
                        "head",
                        "eye",
                        "ear",
                        "obj1",
                        "アーマチュア"
                    })},
                    { "TanukiVerNormal", (new string[] { "body", "head" }, new String[]{
                        "body",
                        "head",
                        "ear",
                        "eye",
                        "obj1.001",
                        "obj2",
                        "obj5.001",
                        "obj5.002",
                        "obj5.003",
                        "obj_body",
                        "obj_head",
                        "アーマチュア",
                    })},
                    { "TanukiVerGhost", (new string[] { "body", "head" }, new String[]{
                        "body",
                        "head",
                        "ear",
                        "eye",
                        "obj1.001",
                        "obj2",
                        "obj5.001",
                        "obj5.002",
                        "obj5.003",
                        "obj_body",
                        "obj_head",
                        "アーマチュア",
                    })},
                    { "CatVerNormal", (new string[] { "body", "head" }, new String[]{
                        "body",
                        "head",
                        "ear",
                        "eye",
                        "obj1",
                        "obj4",
                        "アーマチュア.001",
                    })},
                    { "CatVerGhost", (new string[] { "body", "head" }, new String[]{
                        "body",
                        "body1",
                        "head",
                        "ear",
                        "eye",
                        "obj1",
                        "obj4",
                        "アーマチュア.001",
                    })},
                };
                foreach (Transform child in gameObject.transform)
                {
                    // childのnameがgameObjectNameList[gameObjectName]に含まれているかどうか。
                    if (Array.Exists(gameObjectList[gameObjectName].addRigidBodyAndBoxCollider, element => element == child.name))
                    {
                        child.gameObject.AddComponent<BoxCollider>();
                        AddRigidBody(child.gameObject);
                    }

                    if (!Array.Exists(gameObjectList[gameObjectName].initDisplayOject, element => element == child.name))
                    {
                        child.gameObject.SetActive(false);
                    }
                }

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
                gameObject.AddComponent<AnimationTimer>();
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
    /// 生成したObjectに対して、RigidBodyを追加する。
    /// </summary>
    private void AddRigidBody(GameObject childObject)
    {
        Rigidbody rigidbody = childObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
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
        childObject.transform.position = new Vector3(-1, 0, 10);
        childObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        // 初期サイズ
        childObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}