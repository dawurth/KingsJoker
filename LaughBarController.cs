using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LaughBarController : MonoBehaviour
{

    public Slider laughBar;
    public const int MAX_LAUGHTER = 100;

    // Start is called before the first frame update
    void Start()
    {
        laughBar = GetComponent<Slider>();
        laughBar.maxValue = MAX_LAUGHTER;
        laughBar.value = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMood(float mood)
    {
        laughBar.value += mood;
    }

}
