using System;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject targetObject1; // 色を変えたい対象のゲームオブジェクト
    public GameObject targetObject2; // 色を変えたい対象のゲームオブジェクト
    public Color newColor; // 変更後の色
    public static Color eyeColor;
    public static Color earColor;
    public static Color bodyColor;

    // public string Target;
    public string[] change = new string[2];
     

    // ボタンがクリックされたときに呼び出す関数
    public void ChangeObjectColor()
    {
        change = new ChangeTarget().GetTarget();
        targetObject1 = GameObject.Find(change[0]);
        targetObject2 = GameObject.Find(change[1]);
        // String target = new ChangeTarget().GetTarget();
        //  targetObject1 = GameObject.Find(new ChangeTarget().GetTarget());
       
           
        
        if (targetObject1 != null && targetObject2 != null)
        {
            Debug.Log(new ChangeTarget().GetTarget1());
            Debug.Log("targetObject1" + targetObject1);
            Debug.Log("targetObject2" + targetObject2);


            if (new ChangeTarget().GetTarget1() == "eye")
            {
                eyeColor = newColor;
                Debug.Log("eyeColor:" + eyeColor);
            }
            else if (new ChangeTarget().GetTarget1() == "ear")
            {
                earColor = newColor;
                Debug.Log("earColor:" + earColor);
            }

            // ゲームオブジェクトのRendererコンポーネントを取得
            Renderer[] renderers1 = targetObject1.GetComponentsInChildren<Renderer>();
            Renderer[] renderers2 = targetObject2.GetComponentsInChildren<Renderer>();

            if (renderers1 != null && renderers1.Length > 0 && renderers2 != null && renderers2.Length > 0)
            {
                //脳筋してるので後で修正する
                // if(renderers2 != null && renderers2.Length > 0){
                    // 各RendererのMaterialの色を変更
                    foreach (Renderer renderer in renderers1)
                    {
                        foreach (Material material in renderer.materials)
                        {
                            material.color = newColor;
                        }
                    }
                     foreach (Renderer renderer in renderers2)
                    {
                        foreach (Material material in renderer.materials)
                        {
                            material.color = newColor;
                        }
                    // }
                    }
                
            }
            else
            {
                Debug.LogWarning("Rendererコンポーネントが見つかりません。対象のゲームオブジェクトにRendererが必要です。");
            }
        }
        else if(targetObject1 != null && targetObject2 == null){
            if (targetObject1.name == "eye")
            {
                eyeColor = newColor;
                Debug.Log("bodyColor:" + bodyColor);
            }
            else if (targetObject1.name == "ear")
            {
                earColor = newColor;
                Debug.Log("bodyColor:" + bodyColor);
            }
            else if (targetObject1.name == "body")
            {
                bodyColor = newColor;
                Debug.Log("earColor:" + earColor);
            }

            Renderer[] renderers1 = targetObject1.GetComponentsInChildren<Renderer>();
            if (renderers1 != null && renderers1.Length > 0)
            {
                 if (renderers1 != null && renderers1.Length > 0){
                     foreach (Renderer renderer in renderers1)
                    {
                        foreach (Material material in renderer.materials)
                        {
                            material.color = newColor;
                        }
                    }
                 }
                
            }
            else
            {
                Debug.LogWarning("Rendererコンポーネントが見つかりません。対象のゲームオブジェクトにRendererが必要です。");
            }
        }
        else
        {
            Debug.LogWarning("対象のゲームオブジェクトが設定されていません。");
        }
    }
}