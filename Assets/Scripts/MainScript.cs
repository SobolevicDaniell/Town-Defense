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
    public Button FarmersButton;

    public Image WarriorsImage;
    public Text WarriorsText;
    public Button WarriorsButton;

    public Image WaveImage;
    public Text WaveText;
    public Text EnemiesText;
    public Text WaveNumberText;
    public Image ConsumptionImage;


    // Начальные настройки
    public int Wheats = 5;
    public int Farmers = 1;
    public int FarmerConsumption = 1;
    public int Warriors = 0;
    public int WarriorConsumption = 2;
    public int WarriorDmg = 3;
    public int Enemies = 1;
    public int EnemyDmg = 3;
    public int EnemyHP = 3;
    public int MaxQueue = 5;

    // Цены
    public int FarmerCost = 2;
    public int WarriorsCost = 5;

    // Профиты
    public int FarmersProfit = 1;
    
    // Здоровье
    public int FarmerHP = 1;
    public int WarriorHP = 3;
    public int EnemysHP = 3;

    // Время
    public float WheatsTime = 5;
    public float FarmersTime = 2;
    public float WarriorsTime = 10;
    public float WaveTime = 180;
    public float ConsumptionTime = 30;

    // Инты
    int WaveNumber = 1;
    int FarmersQueue;
    int WarriorsQueue;
    int WheatsToConsumption;
    int EnemiesTotalDmg;
    int EnemiesTotalHp;
    int WarriorsTotalDmg;
    int WarriorsTotalHP;
    int FarmersTotalHP;

    // Флоаты
    float WheatsTimer;
    float FarmersTimer;
    float WarriorsTimer;
    float WaveTimer;
    float ConsumptionTimer;

    void Start()
    {
        WheatsTimer = WheatsTime;
        WaveTimer = WaveTime;
        ConsumptionTimer = ConsumptionTime;

        FarmersTimer = FarmersTime;
        WarriorsTimer = WarriorsTime;

        FarmersText.text = Mathf.Round(Farmers).ToString();
        WarriorsText.text = Mathf.Round(Warriors).ToString();
        WaveNumberText.text = $"Wave №{WaveNumber}";
        EnemiesText.text = $"Enemies next round: {Enemies}";
    }

    void Update()
    {
        WheatsGenerator();
        Consumption();
        FarmersGenerator();
        WarriorsGenerator();
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

    public void FarmersAddButton() { Wheats -= FarmerCost; FarmersQueue++; }
    public void FarmersGenerator()
    {
        if (FarmersQueue > 0)
        {
            FarmersTimer -= Time.deltaTime;
            FarmersImage.fillAmount = FarmersTimer / FarmersTime;
            if (FarmersTimer <= 0)
            {
                FarmersQueue--;
                Farmers++;
                FarmersTimer = FarmersTime;
                FarmersText.text = Mathf.Round(Farmers).ToString();
            }
        }

        if (Wheats < FarmerCost || FarmersQueue > MaxQueue) // Проверка на взможность покупки фермеров
        { 
            FarmersButton.interactable = false; 
        } else { 
            FarmersButton.interactable = true; 
        }
    }

    public void WarriorsAddButton() { Wheats -= WarriorsCost; WarriorsQueue++; }
    public void WarriorsGenerator()
    {
        if (WarriorsQueue > 0)
        {
            WarriorsTimer -= Time.deltaTime;
            WarriorsImage.fillAmount = WarriorsTimer / WarriorsTime;
            if (WarriorsTimer <= 0)
            {
                WarriorsQueue--;
                Warriors++;
                WarriorsTimer = WarriorsTime;
                WarriorsText.text = Mathf.Round(Warriors).ToString();
            }
        }

        if (Wheats < WarriorsCost || WarriorsQueue > MaxQueue) // Проверка на взможность покупки воинов
        {
            WarriorsButton.interactable = false;
        }
        else
        {
            WarriorsButton.interactable = true;
        }
    }

    void Wave()
    {
        WaveTimer -= Time.deltaTime;
        WaveImage.fillAmount = WaveTimer / WaveTime;
        WaveText.text = Mathf.Round(WaveTimer).ToString();
        if (WaveTimer <= 0)
        {
            // Шаг 1
            EnemiesTotalDmg = Enemies * EnemyDmg;
            EnemiesTotalHp = Enemies * EnemyHP;
            WarriorsTotalDmg = Warriors * WarriorDmg;
            WarriorsTotalHP = Warriors * WarriorHP;
            FarmersTotalHP = Farmers * FarmerHP;

            // Шаг 2
            EnemiesTotalHp -= WarriorsTotalDmg;
            WarriorsTotalHP -= EnemiesTotalDmg;

            // Шаг 3
            

            if (WarriorsTotalHP <= 0)
            {
                FarmersTotalHP -= EnemiesTotalHp / EnemyHP * EnemyDmg;

                if (FarmersTotalHP <= 0) { /* Loosing script */ }
                else { /* Else script */ }
            } else {
                // 123123
            }

            Warriors = WarriorsTotalHP / WarriorHP;

            WaveNumber++;
            Enemies++;
            WarriorsText.text = Mathf.Round(Warriors).ToString();
            FarmersText.text = Mathf.Round(Farmers).ToString();
            WaveNumberText.text = $"Wave №{WaveNumber}";
            EnemiesText.text = $"Enemies: {Enemies}";
            
            WaveTimer = WaveTime;
        }
    }
    
    void Consumption()
    {
        ConsumptionTimer -= Time.deltaTime;
        ConsumptionImage.fillAmount = ConsumptionTimer / ConsumptionTime;

        if (ConsumptionTimer <= 0)
        {
            WheatsToConsumption = Warriors * WarriorConsumption + Farmers * FarmerConsumption;
            while (WheatsToConsumption > 0)
            {
                if (Wheats > 0) { Wheats--; WheatsToConsumption -= 1; }
                else if (Warriors > 0) { WheatsToConsumption -= WarriorConsumption; Warriors--; }
                else if (Farmers > 0) { WheatsToConsumption -= FarmerConsumption; Farmers--; }
                else { /* Loosing script */ }
            }
            ConsumptionTimer = ConsumptionTime;
        }
    }
}