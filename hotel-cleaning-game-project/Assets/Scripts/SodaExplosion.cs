using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaExplosion : MonoBehaviour
{
    public float explosionForce;
    public float radius;
    public float maxDisplacement = 0.2f;
    public float upwardsModifier;
    public List<Rigidbody> sodaCans;

    private void Start()
    {
        ExplodeCans();
    }
    public void ExplodeCans()
    {
        float randomDisplaceX = Random.value * maxDisplacement;
        float randomDisplaceY = Random.value * maxDisplacement;
        float randomDisplaceZ = Random.value * maxDisplacement;
        Vector3 explosionPos = transform.position + new Vector3(randomDisplaceX, randomDisplaceY, randomDisplaceZ);

        foreach (Rigidbody soda in sodaCans)
        {
            soda.AddExplosionForce(explosionForce, explosionPos, radius, upwardsModifier);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
