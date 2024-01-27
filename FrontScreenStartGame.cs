using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontScreenStartGame : MonoBehaviour
{
    public Button startGame;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = startGame.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On click move to game scene
    void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

}
