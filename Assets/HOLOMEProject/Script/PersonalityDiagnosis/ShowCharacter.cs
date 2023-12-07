using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacter : MonoBehaviour
{
    private string gameObjectName;

    /// <summary>
    /// 「会いに行く」ボタンをクリック時に発火
    /// Resorces/を対象にDBから取得したModelファイル名と一致するモデルを探し、生成する
    /// 性格診断結果表示パネルを隠す
    /// </summary>
    public void Start()
    {
        // HACK： Static変数をGetterで拾っている
        gameObjectName = new SendResult().GetResponseFileName();
        GameObject generateObject = GameObject.Find("GenerateObject");
        // fbxLoaderのコンストラクタを実行して、generateObjectにMiiを生成する。
        FbxLoader fbxLoader = generateObject.GetComponent<FbxLoader>();

        //fbxLoader.SetGameObjectName(gameObjectName);
        // NOTE: 発表用に固定値を設定
        fbxLoader.SetGameObjectName("MiiVerGhost");
        fbxLoader.GenerateObject();
    }
}
