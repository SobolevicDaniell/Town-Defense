using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
    public Button buttonStart;
    public Button buttonBack;

    public void StartGame()
    {
        buttonStart.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        SceneManager.LoadScene(1);
    }

    public void ModesMenu()
    {
        // под вопросом будет ли вообще
    }

    public void BackToMain()
    {
        buttonBack.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        SceneManager.LoadScene(0);
    }
}
