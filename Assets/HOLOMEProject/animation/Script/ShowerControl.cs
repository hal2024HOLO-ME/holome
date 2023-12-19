using UnityEngine;

public class ShowerControl : MonoBehaviour
{

    // Inspector
    [SerializeField] private ParticleSystem particle;

    /// <summary>
    /// ‹N“®‚ÉŸè‚Éparticle‚ª”­‰Î‚·‚é‚Ì‚ğ–h‚®
    /// </summary>
    void Start()
    {
       particle.Stop();  
    }

}
