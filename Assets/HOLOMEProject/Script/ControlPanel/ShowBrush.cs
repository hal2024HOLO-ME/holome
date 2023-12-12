using UnityEngine;

public class ShowBrush : MonoBehaviour
{
    public GameObject Brush;

    public void OnClickBrushButton()
    {
        // Brushがアクティブになっているときは、Brushを非アクティブにする。
        if (Brush.activeSelf)
        {
            BrushCollisionDetection.isBrushUsed = true;
            Brush.SetActive(false);
            return;
        }
        Brush.SetActive(true);
    }
}
