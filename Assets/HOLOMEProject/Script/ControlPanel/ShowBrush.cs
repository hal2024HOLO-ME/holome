using UnityEngine;

public class ShowBrush : MonoBehaviour
{
    public GameObject Brush;

    public void OnClickBrushButton()
    {
        // Brush���A�N�e�B�u�ɂȂ��Ă���Ƃ��́ABrush���A�N�e�B�u�ɂ���B
        if (Brush.activeSelf)
        {
            BrushCollisionDetection.isBrushUsed = true;
            Brush.SetActive(false);
            return;
        }
        Brush.SetActive(true);
    }
}
