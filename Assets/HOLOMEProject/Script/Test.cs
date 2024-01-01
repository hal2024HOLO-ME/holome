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
        // �I�u�W�F�N�gB���ڐG���Ă��Ȃ��ꍇ�ɂ̂ݍX�V
        if (!isContacted)
        {
            // �o�ߎ��Ԃ��X�V
            elapsedTime += Time.deltaTime;

            // �ړ��̐i���x�����i0����1�j���v�Z
            float progress = Mathf.Clamp01(elapsedTime / duration);

            // objectB����������objectA�ɋ߂Â���
            Vector3 targetPosition = new Vector3(objectA.position.x, objectB.position.y, objectA.position.z);
            objectB.position = Vector3.Lerp(objectB.position, targetPosition, Time.deltaTime / duration);

            // duration�b�o�ߌ�ɏI���������s��
            if (elapsedTime >= duration)
            {
                // �����I���������K�v�ȏꍇ�͂����ɒǉ�
            }
        }
    }

    // �I�u�W�F�N�g���m�̐ڐG���ɌĂяo����郁�\�b�h
    void OnTriggerEnter(Collider other)
    {
        // �ڐG�����̂�objectA�������ꍇ
        if (other.CompareTag("Sphere"))
        {
            // �I�u�W�F�N�gB�̓������~�߂�
            isContacted = true;
            Debug.Log("�ڐG����");
        }
    }
}
