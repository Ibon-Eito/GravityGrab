using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject orbVisuals;
    [SerializeField] private AbsorbParticles absorbParticles;

    public IEnumerator DestroyOrb()
    {
        orbVisuals.SetActive(false);
        ps.Play();
        Object.Destroy(this.gameObject, 5);
        yield return new WaitForSeconds(0.4f);
        absorbParticles.StartAttraction();
    }
}