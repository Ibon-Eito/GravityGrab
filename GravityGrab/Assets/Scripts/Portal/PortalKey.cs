using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public enum PortalKeyState
{
    PATHING,
    WANDERING,
    OPENING
}

[Serializable]
class SteeringInfo
{
    public float speed;
    public float slowdownDistance;
    public float distanceToStop;
    public float rotationSpeed;
}  

public class PortalKey : MonoBehaviour
{
    [SerializeField] private Portal portal;
    [SerializeField] private PortalKeyState state; 

    [Header("PATHING")]
    [SerializeField] private SteeringInfo pathingInfo;
    [SerializeField] private List<Transform> transforms;
    [SerializeField] private bool loops;
    [SerializeField] private int repetitions = 0;

    [Header("WANDERING")]
    [SerializeField] private SteeringInfo wanderingInfo;
    [SerializeField] private float maxWanderRange;

    [Header("OPENING")]
    [SerializeField] private SteeringInfo openingInfo;
    [SerializeField] private Vector3 portalPosition;
    [SerializeField] private ParticleSystem explosion;
    


    private int nextPosition = 0; 
    private List<Vector2> positions = new List<Vector2>();
    private Vector2 velocity = Vector2.zero;
    private Vector2 nextWanderPosition;
    private bool reachedPortal = false;
    private bool stop = false;

    void Start()
    {
        portalPosition = portal.transform.position;

        foreach (Transform t in transforms)
        {
            positions.Add(new Vector2(t.position.x, t.position.y));
        }
    }

    void Update()
    {
        if(stop) return;
        switch (state)
        {
            case PortalKeyState.PATHING:
                KeyPath();
                break;
            case PortalKeyState.WANDERING:
                KeyWander();
                break;
            case PortalKeyState.OPENING:
                KeyOpen();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && state != PortalKeyState.OPENING)
        {
            collider.gameObject.GetComponent<VFXPlayer>().PlayKeySFX();
            state = PortalKeyState.OPENING;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.GetComponent<TrailRenderer>().startColor = Color.white;
            gameObject.GetComponent<TrailRenderer>().endColor = Color.white;

            try
            {
                TutorialManager tut = FindObjectOfType<TutorialManager>();
                if (tut != null)
                {
                    if (tut.currentStep == 3)
                        StartCoroutine(tut.GoToNextStep());
                }
            }
            catch
            {
                Debug.LogWarning("You are not in tutorial");
            }
        }    
    }

    private void KeyPath()
    {
        Vector2 targetPosition = positions[nextPosition];
        SteeringBehaviour(targetPosition, pathingInfo);

        if(Vector2.Distance(transform.position, targetPosition) <= pathingInfo.distanceToStop)
        {
            nextPosition = (nextPosition + 1) % (positions.Count);
            if(nextPosition == 0 && !loops)
            {
                repetitions--;
                if (repetitions < 0)
                {
                    nextWanderPosition = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * maxWanderRange;
                    Debug.DrawRay((Vector3)nextWanderPosition, transform.position, Color.green, 2);
                    state = PortalKeyState.WANDERING;
                }
            }
        }
    }

    private void KeyWander()
    {
        SteeringBehaviour(nextWanderPosition, wanderingInfo);

        if (Vector2.Distance(transform.position, nextWanderPosition) <= wanderingInfo.distanceToStop)
        {
            nextWanderPosition = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * maxWanderRange;
            Debug.DrawLine((Vector3)nextWanderPosition, transform.position, Color.green, 2);
        }
    }

    private void KeyOpen()
    {
        SteeringBehaviour(portalPosition, openingInfo);

        if (!reachedPortal && Vector2.Distance(transform.position, portalPosition) <= openingInfo.distanceToStop)
        {
            FindObjectOfType<VFXPlayer>().PlayKeySFX();
            reachedPortal = true;
            portal.OpenPortal();
            explosion.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            UnityEngine.Object.Destroy(gameObject,3);
        }
    }

    private void SteeringBehaviour(Vector2 targetPosition, SteeringInfo info)
    {
        Vector2 keyDistance = (targetPosition - (Vector2)transform.position);
        Vector2 desiredVelocity = keyDistance.normalized * info.speed;
        Vector2 steering = desiredVelocity - velocity;

        velocity += steering * Time.deltaTime;

        float slowdownFactor = Mathf.Clamp01(keyDistance.magnitude / info.slowdownDistance);
        velocity *= slowdownFactor;

        transform.position += (Vector3)velocity * Time.deltaTime;

        if (velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * info.rotationSpeed);
        }
    }

    public void CanMove(bool can)
    {
        stop = !can;
    }
}
