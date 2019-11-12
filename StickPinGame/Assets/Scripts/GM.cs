using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    public bool isGameOver = false;

    public SendPin pin;
    public Rotater rr;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GameOver!");
            pin.enabled = false;
            rr.enabled = false;
            GetComponent<Animator>().SetTrigger("GameOver");
        }

    }

    public void RestarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
