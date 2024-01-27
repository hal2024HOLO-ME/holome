using UnityEngine;

public class FloorHidden : MonoBehaviour
{
    public GameObject caracter;
    public GameObject objectB;
    public float hideDistance = 2.2f; // オブジェクトAとオブジェクトBの非表示距離

    void Update()
    {
        // オブジェクトAとオブジェクトBの距離を計算
        float distance = Vector3.Distance(caracter.transform.position, objectB.transform.position);

        Debug.Log(distance);

        // 距離が一定の範囲よりも離れた場合にオブジェクトBを非表示にする
        if (distance > hideDistance)
        {
            Debug.Log("床を非表示にした");
            objectB.SetActive(false); // オブジェクトBを非表示にする
        }
    }
}
