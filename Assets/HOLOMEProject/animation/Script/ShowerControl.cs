using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerControl : MonoBehaviour
{

    // Inspector
    [SerializeField] private ParticleSystem particle;

    /// <summary>
    /// �N�����ɏ����particle�����΂���̂�h��
    /// </summary>
    void Start()
    {
       particle.Stop();  
    }

}
