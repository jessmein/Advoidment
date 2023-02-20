using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Advertisement : MonoBehaviour
{
    protected bool paused = false;
    protected bool completed = false;

    public abstract bool Paused { get; }
    public abstract bool Completed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseAd() {
        paused = true;
    }

    public void UnpauseAd() {
        paused = false;
    }

    public abstract void CreateAd();

    protected abstract IEnumerator waiter();
    protected abstract IEnumerator waiterDeath();
}
