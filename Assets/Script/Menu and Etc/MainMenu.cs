using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLV1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void PlayLV2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
    }
    
    public void PlayLV3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +3);
    }

    public void PlayLV4()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +4);
    }

    public void PlayRandom()
    {
        int index = Random.Range(1,4);
        SceneManager.LoadScene(index);
    }

    public void PlayLV1again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
    }

    public void PlayLV2again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -3);
    }

    public void Quitgame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
