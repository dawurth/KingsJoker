using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScoreController : MonoBehaviour
{
    public GameObject currentScoreGO;
    public GameObject highScoreGO;

    // Start is called before the first frame update
    void Start()
    {
        float currentScore = PlayerPrefs.GetFloat("currentScore");
        float highScore = PlayerPrefs.GetFloat("highScore");

        if(currentScore > highScore)
        {
            currentScoreGO.GetComponent<TextMeshProUGUI>().text = "Score: " + currentScore.ToString("#0.00") + " New Record!";
        } else
        {
            currentScoreGO.GetComponent<TextMeshProUGUI>().text = "Score: " + currentScore.ToString("#0.00");
        }
        
        highScoreGO.GetComponent<TextMeshProUGUI>().text = "Highest Score: " + highScore.ToString("#0.00");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
