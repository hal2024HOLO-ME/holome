using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public float duration = 5.0f;

    private float elapsedTime = 0.0f;
    private bool isContacted = false;

    void Update()
    {
        // オブジェクトBが接触していない場合にのみ更新
        if (!isContacted)
        {
            // 経過時間を更新
            elapsedTime += Time.deltaTime;

            // 移動の進捗度合い（0から1）を計算
            float progress = Mathf.Clamp01(elapsedTime / duration);

            // objectBをゆっくりとobjectAに近づける
            Vector3 targetPosition = new Vector3(objectA.position.x, objectB.position.y, objectA.position.z);
            objectB.position = Vector3.Lerp(objectB.position, targetPosition, Time.deltaTime / duration);

            // duration秒経過後に終了処理を行う
            if (elapsedTime >= duration)
            {
                // もし終了処理が必要な場合はここに追加
            }
        }
    }

    // オブジェクト同士の接触時に呼び出されるメソッド
    void OnTriggerEnter(Collider other)
    {
        // 接触したのがobjectAだった場合
        if (other.CompareTag("Sphere"))
        {
            // オブジェクトBの動きを止める
            isContacted = true;
            Debug.Log("接触した");
        }
    }
}
