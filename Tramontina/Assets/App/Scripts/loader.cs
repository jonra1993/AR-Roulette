using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loader : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log("Active scene is '" + scene.name + "'.");
        StartCoroutine(Delay());
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        playGame();
    }

    public void playGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
