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

    public string[] actionString = { "�O", "���" };
    public bool action = false;
    public TextMeshPro textMeshPro;

    private void Awake()
    {
        // Resources.Load���g�p���ăt�@�C����ǂݍ���
        TextAsset textAsset = Resources.Load<TextAsset>("subscriptionKey");

        if (textAsset != null)
        {
            // �t�@�C���̓��e���擾
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
    /// �����F���G���W���iSpeechRecognizer �N���X�̃C���X�^���X�j���쐬���܂��B
    /// recognizer ���܂��쐬����Ă��Ȃ��ꍇ�Ɍ���A�V���� SpeechRecognizer ���쐬���܂��B
    /// �܂��A�쐬���� recognizer �Ɋe��C�x���g�n���h����o�^���܂��B
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
    /// SpeechRecognizer �C���X�^���X���Z�b�V�������J�n�����Ƃ��ɌĂяo�����C�x���g�n���h���ł��B
    /// �Z�b�V�������J�n�����Ƃ��ɓ���̏������s�������ꍇ�Ɏg�p���܂��B
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SessionStartedHandler(object sender, SessionEventArgs e)
    {
    }

    /// <summary>
    /// SpeechRecognizer �C���X�^���X���Z�b�V�������I�������Ƃ��ɌĂяo�����C�x���g�n���h���ł��B
    /// �Z�b�V�������I�������Ƃ��ɃN���[���A�b�v�Ȃǂ̏������s���ꍇ�Ɏg�p���܂��B
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SessionStoppedHandler(object sender, SessionEventArgs e)
    {
        recognizer = null;
    }

    /// <summary>
    /// �F�����̉����f�[�^���󂯎�����Ƃ��ɌĂяo�����C�x���g�n���h���ł��B�F�����̉����ɑ΂��鏈�����s���܂��B
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
    /// ����������ɔF�����ꂽ�Ƃ��ɌĂяo�����C�x���g�n���h���ł��B
    /// �F�����ꂽ������� recognizedString �ɐݒ肵�A�f�o�b�O���O�ɕ\�����Ă��܂��B
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
    /// �b���n�߂����o���ꂽ�Ƃ��ɌĂяo�����C�x���g�n���h���ł��B
    /// �b���n�߂����o���ꂽ�Ƃ��̏������L�q���܂��B
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SpeechStartDetected(object sender, RecognitionEventArgs e)
    {
    }

    /// <summary>
    /// �b���I��肪���o���ꂽ�Ƃ��ɌĂяo�����C�x���g�n���h���ł��B
    /// �b���I��肪���o���ꂽ�Ƃ��̏������L�q���܂��B
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SpeechEndDetectedHandler(object sender, RecognitionEventArgs e)
    {
    }

    /// <summary>
    /// �F�����L�����Z�����ꂽ�Ƃ��ɌĂяo�����C�x���g�n���h���ł��B
    /// �F�����L�����Z�����ꂽ�Ƃ��̏������L�q���܂��B
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelHandler(object sender, RecognitionEventArgs e)
    {
    }
}
