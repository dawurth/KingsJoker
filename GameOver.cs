using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // After 10 seconds transfer them to frontscreen
        StartCoroutine(TransferToFrontScreen());
    }

    IEnumerator TransferToFrontScreen()
    {
        yield return new WaitForSeconds(10);
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrontScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
