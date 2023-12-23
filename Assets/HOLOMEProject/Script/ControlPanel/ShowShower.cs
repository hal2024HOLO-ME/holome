using UnityEngine;

public class ShowShower : MonoBehaviour
{
    public GameObject Shower;
    public void OnClickShowerButton()
    {
        // Showerがアクティブになっているときは、Showerを非アクティブにする。
        if (Shower.activeSelf)
        {
            ShowerCollisionDetection.isShowerUsed = true;
            Shower.SetActive(false);
            return;
        }
        Shower.SetActive(true);
    }
}
