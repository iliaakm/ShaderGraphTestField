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
        int numCollisionEvents = jetParticle.GetCollisionEvents(other, collisionEvents);

        int i = 0;
        //while (i < numCollisionEvents)
        {
            snowPainterVFX.transform.position = collisionEvents[collisionEvents.Count-1].intersection;
            i++;
        }
    }
}
