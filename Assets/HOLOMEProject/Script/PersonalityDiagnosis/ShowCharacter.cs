using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ShowCharacter : MonoBehaviour
{
    private string gameObjectName;
    private GameObject quadGif;

    /// <summary>
    /// 「会いに行く」ボタンをクリック時に発火
    /// Resources/を対象にDBから取得したModelファイル名と一致するモデルを探し、
    /// ローディング2秒後、生成する
    /// </summary>    
    public void Start()
    {
        // HACK： Static変数をGetterで拾っている
        // 初期化処理
        gameObjectName = new SendResult().GetResponseFileName();
        GameObject generateObject = GameObject.Find("GenerateObject");
        quadGif = GameObject.Find("Quad");

        FbxLoader fbxLoader = generateObject.GetComponent<FbxLoader>();
        fbxLoader.SetGameObjectName(gameObjectName);

        // 2秒ロードしてからモデルを生成する
        Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => {
            quadGif.SetActive(false);
            fbxLoader.GenerateObject();
         });
    }
}
