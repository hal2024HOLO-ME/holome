using Microsoft.CognitiveServices.Speech;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MySpeechRecognizer : MonoBehaviour
{
    private string recognizedString = "";
    private readonly object threadLocker = new();
    private SpeechRecognizer recognizer;
    private const string fromLanguage = "ja-JP";
    private const string region = "japaneast";
    private string subscriptionKey;

    public string[] actionString = { "前", "後ろ" };
    public bool action = false;
    public TextMeshPro textMeshPro;

    private void Awake()
    {
        // Resources.Loadを使用してファイルを読み込み
        TextAsset textAsset = Resources.Load<TextAsset>("subscriptionKey");

        if (textAsset != null)
        {
            // ファイルの内容を取得
            subscriptionKey = textAsset.text;
        }
        else
        {
            Debug.LogError("File not found: ");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BeginRecognizing();
    }

    void OnDestroy()
    {
        recognizer?.Dispose();
    }

    public async void BeginRecognizing()
    {
        CreateSpeechRecognizer();

        if (recognizer != null)
        {
            await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
            // recognizedString = "Say something...";
            Debug.Log("Say something...");
        }
    }

    /// <summary>
    /// 音声認識エンジン（SpeechRecognizer クラスのインスタンス）を作成します。
    /// recognizer がまだ作成されていない場合に限り、新しい SpeechRecognizer を作成します。
    /// また、作成した recognizer に各種イベントハンドラを登録します。
    /// </summary>
    void CreateSpeechRecognizer()
    {
        if (recognizer == null)
        {
            SpeechConfig config = SpeechConfig.FromSubscription(subscriptionKey, region);
            config.SpeechRecognitionLanguage = fromLanguage;
            recognizer = new SpeechRecognizer(config);
            if (recognizer != null)
            {
                recognizer.Recognizing += RecognizingHandler;
                recognizer.Recognized += RecognizedHandler;
                recognizer.SpeechStartDetected += SpeechStartDetected;
                recognizer.SpeechEndDetected += SpeechEndDetectedHandler;
                recognizer.Canceled += CancelHandler;
                recognizer.SessionStarted += SessionStartedHandler;
                recognizer.SessionStopped += SessionStoppedHandler;
            }
        }
    }

    /// <summary>
    /// SpeechRecognizer インスタンスがセッションを開始したときに呼び出されるイベントハンドラです。
    /// セッションが開始されるときに特定の処理を行いたい場合に使用します。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SessionStartedHandler(object sender, SessionEventArgs e)
    {
    }

    /// <summary>
    /// SpeechRecognizer インスタンスがセッションを終了したときに呼び出されるイベントハンドラです。
    /// セッションが終了したときにクリーンアップなどの処理を行う場合に使用します。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SessionStoppedHandler(object sender, SessionEventArgs e)
    {
        recognizer = null;
    }

    /// <summary>
    /// 認識中の音声データを受け取ったときに呼び出されるイベントハンドラです。認識中の音声に対する処理を行います。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RecognizingHandler(object sender, SpeechRecognitionEventArgs e)
    {
        if (e.Result.Reason == ResultReason.RecognizingSpeech)
        {
            lock (threadLocker)
            {
                recognizedString = $"{e.Result.Text}";
                Debug.Log(recognizedString);
            }
        }
    }

    /// <summary>
    /// 音声が正常に認識されたときに呼び出されるイベントハンドラです。
    /// 認識された文字列を recognizedString に設定し、デバッグログに表示しています。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RecognizedHandler(object sender, SpeechRecognitionEventArgs e)
    {
        if (e.Result.Reason == ResultReason.RecognizedSpeech)
        {
            lock (threadLocker)
            {
                recognizedString = $"{e.Result.Text}";
                Debug.Log(recognizedString);
                textMeshPro.text = recognizedString;
            }
        }
        else if (e.Result.Reason == ResultReason.NoMatch)
        {
        }
    }

    /// <summary>
    /// 話し始めが検出されたときに呼び出されるイベントハンドラです。
    /// 話し始めが検出されたときの処理を記述します。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SpeechStartDetected(object sender, RecognitionEventArgs e)
    {
    }

    /// <summary>
    /// 話し終わりが検出されたときに呼び出されるイベントハンドラです。
    /// 話し終わりが検出されたときの処理を記述します。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SpeechEndDetectedHandler(object sender, RecognitionEventArgs e)
    {
    }

    /// <summary>
    /// 認識がキャンセルされたときに呼び出されるイベントハンドラです。
    /// 認識がキャンセルされたときの処理を記述します。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelHandler(object sender, RecognitionEventArgs e)
    {
    }
}
