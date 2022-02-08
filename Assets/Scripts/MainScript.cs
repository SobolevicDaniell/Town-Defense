using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Image WheatsImage;
    public Text WheatsText;

    public Image FarmersImage;
    public Text FarmersText;

    public Image WarriorsImage;
    public Text WarriorsText;

    public Image WaveImage;
    public Text WaveText;
    public Text EnemiesText;
    public Text WaveNumberText;



    public int Wheats = 5;
    public int Farmers = 1;
    public int Warriors = 0;
    public int Enemies = 1;

    public int FarmerCost = 2;
    public int WarriorsCost = 5;

    public int FarmersProfit = 1;

    public int FarmersLives = 1;
    public int WarriorsLives = 3;
    public int EnemiesLives = 3;

    public float WheatsTime = 5;
    public float FarmersTime = 5;
    public float WarriorsTime = 10;
    public float WaveTime = 60;



    int WaveNumber = 1;


    float WheatsTimer;
    float FarmersTimer;
    float WarriorsTimer;
    float WaveTimer;

    float TotalDamage;

    void Start()
    {
        WheatsTimer = WheatsTime;
        WaveTimer = WaveTime;

        FarmersText.text = Mathf.Round(Farmers).ToString();
        WarriorsText.text = Mathf.Round(Warriors).ToString();
        WaveNumberText.text = $"Wave №{WaveNumber}";
        EnemiesText.text = $"Enemies: {Enemies}";
    }

    void Update()
    {
        WheatsGenerator();
        Wave();
    }


    void WheatsGenerator()
    {
        WheatsTimer -= Time.deltaTime;
        WheatsImage.fillAmount = WheatsTimer / WheatsTime;
        if (WheatsTimer <= 0)
        {
            Wheats += Farmers * FarmersProfit;
            WheatsTimer = WheatsTime;
        }
        WheatsText.text = Mathf.Round(Wheats).ToString();
    }

    public void FarmersGenerator()
    {
        if (Wheats >= FarmerCost)
        {
            Wheats = Wheats - FarmerCost;
            FarmersText.text = Mathf.Round(Farmers).ToString();

            // ошибка тут ->
            FarmersTimer = FarmersTime;
            while (FarmersTimer > 0)
            {
                FarmersTimer -= Time.deltaTime;
                FarmersImage.fillAmount = FarmersTimer / FarmersTime;
            }
            Farmers++;
            FarmersText.text = Mathf.Round(Farmers).ToString();
        }
    }


    public void WarriorsGenerator()
    {
        if (Wheats >= WarriorsCost)
        {
            Wheats = Wheats - WarriorsCost;
            WarriorsText.text = Mathf.Round(Warriors).ToString();

            // ошибка тут ->
            WarriorsTimer = WarriorsTime;
            while (WarriorsTimer > 0)
            {
                WarriorsTimer -= Time.deltaTime;
                WarriorsImage.fillAmount = WarriorsTimer / WarriorsTime;
            }
            Warriors++;
            WarriorsText.text = Mathf.Round(Warriors).ToString();
        }
    }
    void Wave()
    {
        WaveTimer -= Time.deltaTime;
        WaveImage.fillAmount = WaveTimer / WaveTime;
        WaveText.text = Mathf.Round(WaveTimer).ToString();
        if (WaveTimer <= 0)
        {

            // ошибка тут ->
            TotalDamage = Enemies * EnemiesLives;
            while (TotalDamage == 0)
            {
                TotalDamage -= WarriorsLives;
                Warriors--;
                
                if (Warriors == 0)
                {
                    TotalDamage -= FarmersLives;
                    Farmers--;
                    
                    if (Farmers == 0)
                    {
                        SceneManager.LoadScene(2);
                    }
                }
            }

            WaveNumber++;
            Enemies++;
            WarriorsText.text = Mathf.Round(Warriors).ToString();
            FarmersText.text = Mathf.Round(Farmers).ToString();
            WaveNumberText.text = $"Wave №{WaveNumber}";
            EnemiesText.text = $"Enemies: {Enemies}";
            
            WaveTimer = WaveTime;
        }
        
    }

}