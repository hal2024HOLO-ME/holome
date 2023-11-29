using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeContents : MonoBehaviour
{
    // 兄弟オブジェクトのリスト
    private List<GameObject> siblingObjects = new List<GameObject>();

    // 現在表示している兄弟オブジェクトのインデックス
    private int currentSiblingIndex = 0;

    void Start()
    {
        // すべての兄弟オブジェクトをリストに追加
        for (int i = 2; i < transform.parent.childCount - 1; i++)
        {
            siblingObjects.Add(transform.parent.GetChild(i).gameObject);
        }

        // 最初の兄弟オブジェクトだけを表示する
        if (siblingObjects.Count > 0)
        {
            siblingObjects[0].SetActive(true);
        }
    }

    // 次へボタンが押されたときに呼び出されるメソッド
    public void OnNextButtonClicked()
    {
        // もし最後まで表示していたらシーンを切り替える
        if (currentSiblingIndex == siblingObjects.Count - 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PersonalityDiagnosis");
            return;
        }

        // 現在表示している兄弟オブジェクトを非表示にする
        siblingObjects[currentSiblingIndex].SetActive(false);

        // インデックスを更新する
        currentSiblingIndex = (currentSiblingIndex + 1) % siblingObjects.Count;

        // 新しい兄弟オブジェクトを表示する
        siblingObjects[currentSiblingIndex].SetActive(true);
    }
}
