using System.Collections.Generic;
using UnityEngine;

public class PainterTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject snowPainterVFX;

    [SerializeField]
    private ParticleSystem jetParticle;

    private List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        jetParticle.GetCollisionEvents(other, collisionEvents);

        snowPainterVFX.transform.position = collisionEvents[collisionEvents.Count - 1].intersection;
    }

    public void JetOn()
    {
        jetParticle.Play();
    }

    public void JetOff()
    {
        jetParticle.Stop();
    }
}