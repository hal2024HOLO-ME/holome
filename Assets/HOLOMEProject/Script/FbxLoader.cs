using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UniRx;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// オブジェクトの生成を行う。
/// </summary>
public class FbxLoader : MonoBehaviourPunCallbacks
{
    private string gameObjectName;
    public GameObject Food;
    public GameObject Brush;
    public ParticleSystem Shower;
    public GameObject SignOutButton;
    private GameObject quadGif;
    public GameObject partnerTable;

    private void Awake()
    {
        GameObject parentPartnerTable = GameObject.Find("ParentPartnerTable");
        partnerTable = parentPartnerTable.transform.Find("ChildPartnerTable").gameObject;

        quadGif = GameObject.Find("Quad");


        Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => {
            quadGif.SetActive(false);

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
                Vector3 spawnPosition = cameraPosition + cameraForward * 0.8f;
                Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);

                Debug.Log(gameObjectName + "を生成します。");
                Debug.Log(gameObjectName.Equals("KitsuneVerGhost"));
                //GameObject gameObject = PhotonNetwork.Instantiate("KitsuneVerGhost", spawnPosition, rotation);
                // Resousesの配下のpfehabを取得して表示をさせる。
                GameObject fbxObject = Resources.Load<GameObject>(gameObjectName);

                partnerTable.transform.position = spawnPosition;
                partnerTable.SetActive(true);

                Debug.Log(gameObject);
                if (gameObject != null)
                {
                    // FBXをHierarchyに追加する
                    GameObject generatedObject = Instantiate(fbxObject);
                    generatedObject.name = gameObjectName;
                    generatedObject.transform.rotation = rotation;
                    generatedObject.transform.position = spawnPosition;

                    AddAnimatorController(generatedObject);

                    CharacterModel characterModel = AddCharacterModel(generatedObject);
                    SetInitSize(characterModel.GetGameObject());

                    FoodCollisionDetection foodCollisionDetection = Food.GetComponent<FoodCollisionDetection>();
                    foodCollisionDetection.SetCharacterModel(characterModel);

                    BrushCollisionDetection brushCollisionDetection = Brush.GetComponent<BrushCollisionDetection>();
                    brushCollisionDetection.SetCharacterModel(characterModel);

                    ShowerCollisionDetection showerCollisionDetection = Shower.GetComponent<ShowerCollisionDetection>();
                    showerCollisionDetection.SetCharacterModel(characterModel);

                    generatedObject.AddComponent<HealthMonitor>();
                    AnimationTimer animationTimer = generatedObject.AddComponent<AnimationTimer>();
                    animationTimer.SetCharacterModel(characterModel);
                    generatedObject.AddComponent<NostalgicManager>();

                    SignOut signOut = SignOutButton.GetComponent<SignOut>();
                    signOut.SetCharacterModel(characterModel);

                    MySpeechRecognizer mySpeechRecognizer = generatedObject.AddComponent<MySpeechRecognizer>();
                    mySpeechRecognizer.SetCharacterModel(characterModel);

                    var sessionObject = JsonUtility.FromJson<LoginResponse>(new Login().GetResponseSession());
                    Debug.Log(sessionObject);

                    if (!sessionObject.isCharacterExists) return;

                    string CHARACTER_JSON_DATA = new GetCharacterJson().GetResponseString();


                    var characterData = JsonUtility.FromJson<CharacterData>(CHARACTER_JSON_DATA);

                    // 懐き度のセット
                    characterModel.SetNostalgicLevel(characterData.nostalgic_level);

                    Renderer[] renderers = generatedObject.GetComponentsInChildren<Renderer>();

                    Dictionary<string, Color> colorDictionary = new()
                {
                    {"eye", characterData.color.eye},
                    {"ear", characterData.color.ear},
                    {"body", characterData.color.body},
                };

                    foreach (Renderer renderer in renderers)
                    {
                        if (characterData.customize.GetType().GetProperties().Any(p => p.Name == renderer.name))
                        {
                            renderer.enabled = true;
                        }

                        if (characterData.color.GetType().GetProperties().Any(p => p.Name == renderer.name))
                        {
                            renderer.material.color = colorDictionary[renderer.name];
                        }
                    }

                }
            }
        });

        
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