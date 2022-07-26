using UnityEngine;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] ParticleSystem[] playOnEnable;
    
    private void OnEnable()
    {
        foreach (var particleSystem in playOnEnable)
        {
            particleSystem.Play();
        }
    }
}
