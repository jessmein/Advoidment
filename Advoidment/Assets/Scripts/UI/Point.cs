using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to delete the points
public class Point : MonoBehaviour
{
    public ParticleSystem particlePrefab;
    public int poolSize = 10;

    public Material plusOne;
    public Material plusTwo;

    private List<ParticleSystem> particlePool;
    private int numInactive = 0;
    private bool beenReset = true;
    private bool addingCount = false;

    private void Start()
    {
        particlePool = new List<ParticleSystem>();

        AddToPool();
    }

    private void Update()
    {
        for (int i = 0; i < particlePool.Count; i++)
        {
            if (!particlePool[i].isPlaying)
            {
                particlePool[i].gameObject.SetActive(false);
                numInactive++;
            }
        }

        if (numInactive > poolSize && !beenReset)
        {
            ResetPool();
        }

    }

    public ParticleSystem GetParticle()
    {
        // Find inactive particle in pool
        ParticleSystem particle = particlePool.Find(p => !p.gameObject.activeInHierarchy);

        // if there's no inactive particle found, add more to the pool
       if (particle == null && !addingCount){

            particle = Instantiate(particlePrefab);
            particlePool.Add(particle);

            poolSize += 5;
            AddToPool();           
       }

        numInactive--;
        beenReset = false;

        // Activate the particle and return it
        particle.gameObject.SetActive(true);
        return particle;
    }

    public void SetMaterial(bool doublePoints)
    {
        Material pointMaterial; // used to determine which point material to assign

        if (doublePoints)
        {
            pointMaterial = plusTwo;
        }
        else
        {
            pointMaterial = plusOne;
        }

        for (int i = 0; i < particlePool.Count; i++)
        {
            particlePool[i].GetComponent<ParticleSystemRenderer>().material = pointMaterial;
        }
    }

    private void AddToPool()
    {
        addingCount = true;
        // edit the pool to contain more
        for (int i = 0; i < 4; i++)
        {
            ParticleSystem particle = Instantiate(particlePrefab);
            particle.gameObject.SetActive(false);
            particlePool.Add(particle);
        }
        addingCount = false;
    }

    private void ResetPool()
    {
        Debug.Log("I'm reseting");
        poolSize = 10;

        beenReset = true;

        for (int i = particlePool.Count - 1; i > poolSize; i--)
        {
            Destroy(particlePool[i].gameObject);
            particlePool.RemoveAt(i);
        }
    }
}