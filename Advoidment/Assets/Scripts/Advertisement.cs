using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdDifficulty {
    Easy        = 2,
    Medium      = 4,
    Hard        = 6
}

public enum WindowType
{
    square,
    longVert,
    wide
}

public abstract class Advertisement : MonoBehaviour
{
    protected bool paused = false;
    protected bool completed = false;
    protected AdDifficulty difficulty;
    protected WindowType winType;

    public abstract bool Paused { get; }
    public abstract bool Completed { get; set; }
    public AdDifficulty Difficulty { get { return difficulty; } set { difficulty = value; } }
    public WindowType WinType { get { return winType; } set { winType = value; } }
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

    // gets the window obj animator to activate
    protected Animator GetWindowObj()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;

            Debug.Log("child name: " + child.name);

            Debug.Log("Contains Window? " + child.name.Contains("Window"));

            if (child.name.Contains("Window"))
            {
                return child.GetComponent<Animator>();
            }
        }

        return null;
    }

    public abstract void CreateAd();

    protected abstract IEnumerator waiter();
    protected abstract IEnumerator waiterDeath();
}
