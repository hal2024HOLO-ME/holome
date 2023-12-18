using UnityEngine;

public class ShowShower : MonoBehaviour
{
    public GameObject Shower;
    public void OnClickShowerButton()
    {
        // Showerがアクティブになっているときは、Brushを非アクティブにする。
        //ToDo:一度シャワー戻してもう一回ボタン押すと勝手に水流れるの修正する
        if (Shower.activeSelf)
        {
            ShowerCollisionDetection.isShowerUsed = true;
            Shower.SetActive(false);
            return;
        }
        Shower.SetActive(true);
    }
}
