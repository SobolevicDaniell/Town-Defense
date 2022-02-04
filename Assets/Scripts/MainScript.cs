using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Image WheatsImage;
    public Text WheatsText;





    public int Wheats = 5;
    public int Farmers = 1;
    public float WheatsTime = 5;




    float WheatsTimer;

    void Start()
    {
        WheatsTimer = WheatsTime;
    }

    void Update()
    {
        WheatsGenerator();
    }


    void WheatsGenerator()
    {
        WheatsTimer -= Time.deltaTime;
        WheatsImage.fillAmount = WheatsTimer / WheatsTime;
        if (WheatsTimer <= 0)
        {
            Wheats += Farmers;
            WheatsTimer = WheatsTime;
        }
        WheatsText.text = Mathf.Round(Wheats).ToString();
    }
}
