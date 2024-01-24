using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendResult : MonoBehaviour
{
    private int answerCount;
    private string responseCharacterName;

    // HACK: ここのstatic変数なんとかしたいけど非同期処理の関係でうまく直せない
    private static string responseFileName;
    public GameObject slate;
    public GameObject personalityDiagnosisResultSlate;
    public TextMeshPro ResultTitle;
    public TextMeshPro ResultContents;
    public TextMeshPro ResultBelowText;
    public SpriteRenderer characterImageRenderer;
    public SpriteRenderer characterNameSlateImageRenderer;

    private static Config config;

    /// <summary>
    ///  起動時にLoadConfig()でconfigファイルを読み込む
    /// </summary>
    void Awake()
    {
        if (config == null)
        {
            LoadConfig();
        }
    }

    /// <summary>
    /// ResourcesにあるConfigファイルをロードする
    /// </summary>
    private void LoadConfig()
    {
        TextAsset configFile = Resources.Load<TextAsset>("Config");
        if (configFile != null)
        {
            config = JsonUtility.FromJson<Config>(configFile.text);
        }
        else
        {
            Debug.LogError("Configファイルが見つかりません！");
        }
    }



    /// <summary>
    /// StartCoroutineでBEとの連携処理を非同期で行う
    /// </summary>
    public void SendResultToServer()
    {
        StartCoroutine(SendResultCoroutine());
    }

    /// <summary>
    /// キャラクターのタイプ（通常・妖怪）とキャラクターの名前をBEに送信する
    /// </summary>
    /// <returns></returns>
    IEnumerator SendResultCoroutine()
    {
        answerCount = PersonalityDiagnosisRadio.answerCount;
        string characterName = GetCharacterName(answerCount);

        WWWForm form = new();
        form.AddField("character_type", SelectCharacterType.characterType);
        form.AddField("character_name", characterName);

        WWW www = new(config.BASE_URL + "/diagnosis/select-character", form);
        yield return www;

        if (www.error == null)
        {
            ProcessResponse(www.text);
        }
        else
        {
            Debug.Log("接続に失敗しました");
        }
    }

    /// <summary>
    /// BEからレスポンスを受けとり、DiagnosisResponseの構造体に変換
    /// 表示したいUIに反映し性格診断のSlateを非表示、結果用のSlateを表示する
    /// </summary>
    /// <param name="responseText"></param>
    private void ProcessResponse(string responseText)
    {
        DiagnosisResponse response = JsonUtility.FromJson<DiagnosisResponse>(responseText);

        responseFileName = response.model_name;
        responseCharacterName = response.name.Replace(" ", "");

        ResultTitle.text = "あなたは「 " + responseCharacterName + "タイプ」です！";
        ResultContents.text = response.description;
        ResultBelowText.text = "そんなあなたのパートナーは" + responseCharacterName + "です";

        Sprite newSprite = Resources.Load<Sprite>("Images/" + responseFileName);
        characterImageRenderer.sprite = newSprite;
        characterNameSlateImageRenderer.sprite = newSprite;

        slate.SetActive(false);
        personalityDiagnosisResultSlate.SetActive(true);
    }

    /// <summary>
    /// 性格診断のYes/Noの数と対象のキャラクターを紐付ける
    /// NOTE: いぬの確率たかい・・・
    /// </summary>
    /// <param name="answerCount"></param>
    /// <returns></returns>
    private string GetCharacterName(int answerCount)
    {
        switch (answerCount)
        {
            case 0:
                return "ねこ";
            case 1:
            case 2:
                return "いぬ";
            case 3:
                return "たぬき";
            case 4:
                return "きつね";
            case 5:
                return "ミィ";
            default:
                return null;
        }
    }

    public string GetResponseFileName()
    {
        return responseFileName;
    }
}