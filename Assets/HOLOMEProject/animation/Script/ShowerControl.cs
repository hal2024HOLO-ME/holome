using UnityEngine;

public class ShowerControl : MonoBehaviour
{

    // Inspector
    [SerializeField] private ParticleSystem particle;

    /// <summary>
    /// 起動時に勝手にparticleが発火するのを防ぐ
    /// </summary>
    void Start()
    {
       particle.Stop();  
    }

}
