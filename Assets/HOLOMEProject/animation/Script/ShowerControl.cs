using UnityEngine;

public class ShowerControl : MonoBehaviour
{

    // Inspector
    [SerializeField] private ParticleSystem particle;

    /// <summary>
    /// オブジェクトセット時に勝手にparticleが発火するのを防ぐ
    /// </summary>
    void OnEnable() {
         particle.Stop();
    }

}
