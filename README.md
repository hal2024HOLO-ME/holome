## セットアップ
1. cloneする
1. 議事録などが入ってる Google Drive にあがっている Font を拾ってくる
1. `\Holo-me\Assets\HOLOMEProject\Utilities\Fonts`になるように配置する
1. 同じくDriveに上がっているconfigファイル(metaファイルも忘れずに)を拾ってきて`\develop\holome\Assets\Resources\Config.json`になるように配置する

## アニメーションをテストする方々

1. `git clone https://github.com/hal2024-HOLO-ME/Holo-me.git`
1. MRTK XR Rig と MRTKInputSimulator をヒエラルキーに追加する
1. MRTK XR Rig の中のメインカメラの設定を skybox に変える
1. 対象の blender オブジェクトをいれる
1. `Assets/HOLOMEProject/Script/CollisionDetection`にあるコードを参考に作成する
