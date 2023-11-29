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
    public IEnumerator scene�ǂݍ���()
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
    public IEnumerator MiiGhostObject�����������()
    {
        GameObject generateObject = GameObject.Find("GenerateObject");
        // fbxLoader�̃R���X�g���N�^�����s���āAgenerateObject��MiiGhost�𐶐�����B
        FbxLoader fbxLoader = generateObject.AddComponent<FbxLoader>();

        fbxLoader.SetGameObjectName("MiiVerGhost");
        fbxLoader.GenerateObject();

        // Assert: �K�v�ȃA�T�[�V������ǉ�
        // MiiGhost��object����������Ă��邩�ǂ���
        GameObject miiObject = GameObject.Find("MiiVerGhost");
        Debug.Log(miiObject);
        Assert.IsNotNull(miiObject);

        // Animator���A�^�b�`����Ă��邩�ǂ���
        Animator animator = miiObject.GetComponent<Animator>();
        Assert.IsNotNull(animator);

        // CharacterModel���A�^�b�`����Ă��邩�ǂ���
        CharacterModel characterModel = miiObject.GetComponent<CharacterModel>();
        Assert.IsNotNull(characterModel);

        // HealthMonitor���A�^�b�`����Ă��邩�ǂ���
        HealthMonitor healthMonitor = miiObject.GetComponent<HealthMonitor>();
        Assert.IsNotNull(healthMonitor);

        // AnimationTimer���A�^�b�`����Ă��邩�ǂ���
        AnimationTimer animationTimer = miiObject.GetComponent<AnimationTimer>();
        Assert.IsNotNull(animationTimer);

        yield return null;
    }

    [UnityTest]
    public IEnumerator TanukiObject�����������()
    {

        GameObject generateObject = GameObject.Find("GenerateObject");
        // fbxLoader�̃R���X�g���N�^�����s���āAgenerateObject��MiiGhost�𐶐�����B
        FbxLoader fbxLoader = generateObject.AddComponent<FbxLoader>();

        fbxLoader.SetGameObjectName("TanukiVerNormal");
        fbxLoader.GenerateObject();

        // Assert: �K�v�ȃA�T�[�V������ǉ�
        // MiiGhost��object����������Ă��邩�ǂ���
        GameObject miiObject = GameObject.Find("TanukiVerNormal");
        Debug.Log(miiObject);
        Assert.IsNotNull(miiObject);

        // Animator���A�^�b�`����Ă��邩�ǂ���
        Animator animator = miiObject.GetComponent<Animator>();
        Assert.IsNotNull(animator);

        // CharacterModel���A�^�b�`����Ă��邩�ǂ���
        CharacterModel characterModel = miiObject.GetComponent<CharacterModel>();
        Assert.IsNotNull(characterModel);

        // HealthMonitor���A�^�b�`����Ă��邩�ǂ���
        HealthMonitor healthMonitor = miiObject.GetComponent<HealthMonitor>();
        Assert.IsNotNull(healthMonitor);

        // AnimationTimer���A�^�b�`����Ă��邩�ǂ���
        AnimationTimer animationTimer = miiObject.GetComponent<AnimationTimer>();
        Assert.IsNotNull(animationTimer);

        yield return null;
    }

}
