using PlayerFunctions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbParticles : MonoBehaviour
{
    private Transform player;
    public ParticleSystem ps;
    public float attractionSpeed = 5f;
    public bool attracted = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (attracted)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.main.maxParticles];
            int particleCount = ps.GetParticles(particles);

            for (int i = 0; i < particleCount; i++)
            {
                // Calculate direction toward the player
                Vector3 direction = (player.position - particles[i].position).normalized;
                // Move particle toward the player
                particles[i].velocity = direction * attractionSpeed;
            }

            // Apply changes to particle system
            ps.SetParticles(particles, particleCount);
        }
    }

    public void StartAttraction()
    {
        attracted = true;
    }
}
