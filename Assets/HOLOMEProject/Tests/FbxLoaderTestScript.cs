using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FbxLoaderTestScript
{
    private UnityEngine.SceneManagement.Scene scene;

    // A Test behaves as an ordinary method
    [Test]
    public void FbxLoaderTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }


    [UnitySetUp]
    public IEnumerator scene読み込み()
    {
        scene = EditorSceneManager.LoadSceneInPlayMode(
            "Assets/Scenes/ObjectCheck.unity",
            new LoadSceneParameters(LoadSceneMode.Additive)
        );
        yield return null;
        SceneManager.SetActiveScene(scene);

        yield return null;
    }


    [UnityTest]
    public IEnumerator MiiGhostObjectが生成される()
    {
        GameObject generateObject = GameObject.Find("GenerateObject");
        // fbxLoaderのコンストラクタを実行して、generateObjectにMiiGhostを生成する。
        FbxLoader fbxLoader = generateObject.AddComponent<FbxLoader>();

        fbxLoader.SetGameObjectName("MiiVerGhost");
        fbxLoader.GenerateObject();

        // Assert: 必要なアサーションを追加
        // MiiGhostのobjectが生成されているかどうか
        GameObject miiObject = GameObject.Find("MiiVerGhost");
        Debug.Log(miiObject);
        Assert.IsNotNull(miiObject);

        // Animatorがアタッチされているかどうか
        Animator animator = miiObject.GetComponent<Animator>();
        Assert.IsNotNull(animator);

        // CharacterModelがアタッチされているかどうか
        CharacterModel characterModel = miiObject.GetComponent<CharacterModel>();
        Assert.IsNotNull(characterModel);

        // HealthMonitorがアタッチされているかどうか
        HealthMonitor healthMonitor = miiObject.GetComponent<HealthMonitor>();
        Assert.IsNotNull(healthMonitor);

        // AnimationTimerがアタッチされているかどうか
        AnimationTimer animationTimer = miiObject.GetComponent<AnimationTimer>();
        Assert.IsNotNull(animationTimer);

        yield return null;
    }

    [UnityTest]
    public IEnumerator TanukiObjectが生成される()
    {

        GameObject generateObject = GameObject.Find("GenerateObject");
        // fbxLoaderのコンストラクタを実行して、generateObjectにMiiGhostを生成する。
        FbxLoader fbxLoader = generateObject.AddComponent<FbxLoader>();

        fbxLoader.SetGameObjectName("TanukiVerNormal");
        fbxLoader.GenerateObject();

        // Assert: 必要なアサーションを追加
        // MiiGhostのobjectが生成されているかどうか
        GameObject miiObject = GameObject.Find("TanukiVerNormal");
        Debug.Log(miiObject);
        Assert.IsNotNull(miiObject);

        // Animatorがアタッチされているかどうか
        Animator animator = miiObject.GetComponent<Animator>();
        Assert.IsNotNull(animator);

        // CharacterModelがアタッチされているかどうか
        CharacterModel characterModel = miiObject.GetComponent<CharacterModel>();
        Assert.IsNotNull(characterModel);

        // HealthMonitorがアタッチされているかどうか
        HealthMonitor healthMonitor = miiObject.GetComponent<HealthMonitor>();
        Assert.IsNotNull(healthMonitor);

        // AnimationTimerがアタッチされているかどうか
        AnimationTimer animationTimer = miiObject.GetComponent<AnimationTimer>();
        Assert.IsNotNull(animationTimer);

        yield return null;
    }

}
