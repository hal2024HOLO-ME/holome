using UnityEngine;

public class FloorHidden : MonoBehaviour
{
    public GameObject caracter;
    public GameObject objectB;
    public float hideDistance = 2.2f; // �I�u�W�F�N�gA�ƃI�u�W�F�N�gB�̔�\������

    void Update()
    {
        // �I�u�W�F�N�gA�ƃI�u�W�F�N�gB�̋������v�Z
        float distance = Vector3.Distance(caracter.transform.position, objectB.transform.position);

        Debug.Log(distance);

        // ���������͈̔͂������ꂽ�ꍇ�ɃI�u�W�F�N�gB���\���ɂ���
        if (distance > hideDistance)
        {
            Debug.Log("�����\���ɂ���");
            objectB.SetActive(false); // �I�u�W�F�N�gB���\���ɂ���
        }
    }
}
