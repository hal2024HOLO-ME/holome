using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UniRx;
using UnityEngine.Windows;
using System.Linq;

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
        GameObject generateObject = GameObject.Find("GenerateObject");


        var sessionObject = JsonUtility.FromJson<LoginResponse>(new Login().GetResponseSession());

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

                if (!sessionObject.isCharacterExists) return;

                // characterModel.SetNostalgicLevel(character_data.nostalgic_level);
                const string jsonData = @"
                        {
                            ""character_data"": {
                                ""color"": {
                                    ""eye"": ""1.000, 0.000, 0.000, 0.00"",
                                    ""ear"": ""1.000, 0.000, 0.000, 0.000"",
                                    ""body"": ""1.000, 0.000, 0.000, 0.000""
                                },
                                ""customize"": {
                                    ""neck"": ""bell"",
                                    ""head"": ""hat"",
                                    ""face"": ""glasses""
                                }
                            }
                        }";
                var characterData = JsonUtility.FromJson<CharacterData>(jsonData);
                Renderer[] eyeRenderers = gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in eyeRenderers)
                {
                    if (characterData.color.GetType().GetProperties().Any(p => p.Name == renderer.name))
                    {
                        string colorString = null;
                        switch (renderer.name)
                        {
                            case "eye":
                                colorString = characterData.color.eye.ToString();
                                break;
                            case "ear":
                                colorString = characterData.color.ear.ToString();
                                break;
                            case "body":
                                colorString = characterData.color.body.ToString();
                                break;
                        }
                        // 文字列をfloatに変換してからコンマで分割
                        string[] values = colorString.Split(',');
                        // 分割された値をfloat型に変換してColorに代入
                        float r = float.Parse(values[0]);
                        float g = float.Parse(values[1]);
                        float b = float.Parse(values[2]);
                        float a = float.Parse(values[3]);

                        Color color = new(r, g, b, a);
                        renderer.material.color = color;
                    }

                    if(characterData.customize.GetType().GetProperties().Any(p => p.Name == renderer.name))
                    {
                        renderer.enabled = true;
                    }
                }
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
        //childObject.transform.position = new Vector3(-1, 0, 10);
        childObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        // 初期サイズ
        childObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}