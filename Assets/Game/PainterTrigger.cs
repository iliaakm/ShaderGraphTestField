using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PainterTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject snowPainterVFX;
    [SerializeField]
    ParticleSystem jetParticle;

    List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        jetParticle.GetCollisionEvents(other, collisionEvents);

        snowPainterVFX.transform.position = collisionEvents[collisionEvents.Count - 1].intersection;
    }
}
