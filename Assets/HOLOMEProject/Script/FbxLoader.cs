using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトの生成を行う。
/// </summary>
public class FbxLoader : MonoBehaviour
{
    private string gameObjectName;
    public GameObject Food;
    public GameObject Brush;

    // getter and setter
    public string GetGameObjectName()
    {
        return gameObjectName;
    }

    public void SetGameObjectName(string value)
    {
        gameObjectName = value;
    }


    /// <summary>
    /// オブジェクトを生成し、Hierarchyに追加する。
    /// </summary>
    public void GenerateObject()
    {
        // 指定したファイルへのパスからFBXファイルを読み込む。
        // Resources/Object/以下に配置すること。
        GameObject fbxObject = Resources.Load<GameObject>(gameObjectName);

        if (fbxObject != null )
        {
            // FBXをHierarchyに追加する
            GameObject generatedObject = Instantiate(fbxObject);
            generatedObject.name = gameObjectName;

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
            foreach (Transform child in generatedObject.transform)
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

            AddAnimatorController(generatedObject);


            CharacterModel characterModel = AddCharacterModel(generatedObject);
            SetInitSize(characterModel.GetGameObject());

            FoodCollisionDetection foodCollisionDetection = Food.GetComponent<FoodCollisionDetection>();
            foodCollisionDetection.SetCharacterModel(characterModel);

            BrushCollisionDetection brushCollisionDetection = Brush.GetComponent<BrushCollisionDetection>();
            brushCollisionDetection.SetCharacterModel(characterModel);

            generatedObject.AddComponent<HealthMonitor>();
            generatedObject.AddComponent<AnimationTimer>();
            generatedObject.AddComponent<NostalgicManager>();
        }
        else
        {
            Debug.LogError("FBXファイルが見つかりません: " + gameObjectName);
        }
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