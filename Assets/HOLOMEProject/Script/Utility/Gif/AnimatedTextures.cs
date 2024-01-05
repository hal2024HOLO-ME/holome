using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimatedTextures : MonoBehaviour
{
    public string Filename;
    public float FrameDelay = 0.07f; // 新しいパブリック変数を追加します。

    private List<Texture2D> mFrames = new List<Texture2D>();
    private List<float> mFrameDelay = new List<float>();

    private int mCurFrame = 0;
    private float mTime = 0.0f;

    void Start()
    {
        /// TODO: エラー出るので一旦たぬきGhostで固定

        // this.Filename = new SendResult().GetResponseFileName() + ".gif";
        this.Filename = "MiiVerGhost.gif";


        if (string.IsNullOrWhiteSpace(Filename))
        {
            return;
        }

        var path = Path.Combine(Application.streamingAssetsPath, Filename);

        using (var decoder = new MG.GIF.Decoder(File.ReadAllBytes(path)))
        {
            var img = decoder.NextImage();

            while (img != null)
            {
                mFrames.Add(img.CreateTexture());
                mFrameDelay.Add(FrameDelay); // 全てのフレームで一定の遅延時間を設定します。
                img = decoder.NextImage();
            }
        }

        GetComponent<Renderer>().material.mainTexture = mFrames[0];
    }

    void Update()
    {
        if (mFrames == null)
        {
            return;
        }

        mTime += Time.deltaTime;

        if (mTime >= mFrameDelay[mCurFrame])
        {
            mCurFrame = (mCurFrame + 1) % mFrames.Count;
            mTime = 0.0f;

            GetComponent<Renderer>().material.mainTexture = mFrames[mCurFrame];
        }
    }
}
