using Microsoft.CognitiveServices.Speech;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MySpeechRecognizer : MonoBehaviour
{
    private string recognizedString = "";
    private readonly object threadLocker = new();
    private SpeechRecognizer recognizer;

    /// <summary>
    /// メインスレッドで実行するためのコンテキスト
    /// </summary>
    private SynchronizationContext context;
    public bool action = false;
    private CharacterModel characterModel;
    public GameObject characterObject;

    private void Awake()
    {
        context = SynchronizationContext.Current;
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
        if (recognizer != null) return;
        // Resources.Loadを使用してファイルを読み込み
        TextAsset textAsset = Resources.Load<TextAsset>("subscriptionKey");
        string subscriptionKey = "";
        if (textAsset != null)
        {
            // ファイルの内容を取得
            subscriptionKey = textAsset.text;
        }
        else
        {
            // 例外を発生させる
            throw new System.Exception("File not found: ");
        }
        const string region = "japaneast";
        const string fromLanguage = "en-US";
        SpeechConfig config = SpeechConfig.FromSubscription(subscriptionKey, region);
        config.SpeechRecognitionLanguage = fromLanguage;
        recognizer = new SpeechRecognizer(config);

        if (recognizer == null) return;
        
        recognizer.Recognizing += RecognizingHandler;
        recognizer.Recognized += RecognizedHandler;
        recognizer.SpeechStartDetected += SpeechStartDetected;
        recognizer.SpeechEndDetected += SpeechEndDetectedHandler;
        recognizer.Canceled += CancelHandler;
        recognizer.SessionStarted += SessionStartedHandler;
        recognizer.SessionStopped += SessionStoppedHandler;
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
                /*                if (recognizedString.Contains(SendCharacterName.partnerName))
                                {*/
                // mianスレッドで実行
                context.Post(async _ =>
                {
                    float distanceFromCamera = 2f;
                    float moveSpeed = 5f;
                    // カメラのTransformを取得
                    Transform cameraTransform = Camera.main.transform;

                    // カメラの正面方向を取得
                    Vector3 cameraForward = cameraTransform.forward;

                    // 移動先の座標を計算
                    Vector3 targetPosition = cameraTransform.position + cameraForward * distanceFromCamera;

                    if( SendCharacterName.partnerName.Contains(recognizedString) )
                    {
                        GameObject characterObject = characterModel.GetGameObject();
                        while (characterObject.transform.position != targetPosition)
                        {
                            // 現在の位置から目標位置まで Lerp を使用して滑らかに移動
                            characterObject.transform.position = Vector3.Lerp(characterObject.transform.position, targetPosition, Time.deltaTime * moveSpeed);
                        }
                        characterObject.transform.LookAt(cameraTransform);
                        // オブジェクトをカメラの正面方向に移動させる
                        transform.position = Vector3.Lerp(characterObject.transform.position, targetPosition, Time.deltaTime * moveSpeed);
                    }
                    action = true;
                }, null);
            }
        }
    }

    /// <summary>
    /// 
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

    public void SetCharacterModel(CharacterModel characterModel)
    {
        // mianスレッドで実行
        context.Post(_ =>
        {
            this.characterModel = characterModel;
        }, null);
    }
}
