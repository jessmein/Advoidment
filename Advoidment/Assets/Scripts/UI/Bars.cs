using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bars : MonoBehaviour
{
    private Slider bar;
    public bool isScore;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.GetComponent<Slider>();

        if (isScore)
        {
            bar.value = 0.0f;
        }
    }

    public void SetMax(float max)
    {
        bar.maxValue = max;

        if (!isScore)
        {
            bar.value = max;
        }
    }

    public void SetTimeMeter(float val)
    {
        if (val >= bar.maxValue)
        {
            bar.maxValue = val;
        }

        bar.value = val;
    }

    public void IncreaseScoreMeter(float val)
    {
        //if (val >= bar.maxValue)
        //{
        //    bar.maxValue += 2 * val;
        //}
        bar.value = val % bar.maxValue;
    }
}
