using UnityEngine;

public class ShowerControl : MonoBehaviour
{

    // Inspector
    [SerializeField] private ParticleSystem particle;

    /// <summary>
    /// �I�u�W�F�N�g�Z�b�g���ɏ����particle�����΂���̂�h��
    /// </summary>
    void OnEnable() {
         particle.Stop();
    }

}
