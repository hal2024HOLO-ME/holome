using UnityEngine;

public class ShowShower : MonoBehaviour
{
    public GameObject Shower;
    public void OnClickShowerButton()
    {
        // Shower���A�N�e�B�u�ɂȂ��Ă���Ƃ��́AShower���A�N�e�B�u�ɂ���B
        if (Shower.activeSelf)
        {
            ShowerCollisionDetection.isShowerUsed = true;
            Shower.SetActive(false);
            return;
        }
        Shower.SetActive(true);
    }
}
