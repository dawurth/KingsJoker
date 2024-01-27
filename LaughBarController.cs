using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LaughBarController : MonoBehaviour
{

    public Slider laughBar;
    public const int MAX_LAUGHTER = 100;
    private float timeScale = 0;
    private float targetLaugh;
    private bool lerpingMood = false;

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
        //targetLaugh = targetLaugh+mood;

        //if (!lerpingMood)
        //    StartCoroutine(LerpMood);
    }

    /*
    private IEnumerator LerpMood
    {

        float speed = 1f;
        float startLaugh = laughBar.value;

        lerpingMood = true;

        while (timeScale < 1)
        {
            timeScale += Time.deltaTime * speed;
            laughBar.value = Mathf.Lerp(startLaugh, targetLaugh, timeScale);
        }
        lerpingMood = false;
        
    }*/
}
