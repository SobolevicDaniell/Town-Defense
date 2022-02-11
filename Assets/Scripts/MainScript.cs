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
    public Image ConsumptionImage;

    public GameObject PopUp;
    public Text PopUpText;
    public LeanTweenType easeType;

    // Начальные настройки
    public int Wheats = 5;
    public int Farmers = 1;
    public int FarmerConsumption = 1;
    public int Warriors = 0;
    public int WarriorConsumption = 2;
    public int Enemies = 1;
    public int MaxQueue = 5;

    // Цены
    public int FarmerCost = 2;
    public int WarriorsCost = 5;

    // Профиты
    public int FarmersProfit = 1;
    
    // Здоровье
    public int FarmerHP = 1;
    public int WarriorHP = 3;
    public int EnemyHP = 3;

    // Дамаг
    public int FarmerDmg = 1;
    public int EnemyDmg = 3;
    public int WarriorDmg = 3;

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
    int EnemiesTotalHP;
    int WarriorsTotalDmg;
    int WarriorsTotalHP;
    int FarmersTotalHP;
    int FarmersTotalDmg;

    // Флоаты
    float WheatsTimer;
    float FarmersTimer;
    float WarriorsTimer;
    float WaveTimer;
    float ConsumptionTimer;

    bool IsMultiply;

    void Start()
    {
        WheatsTimer = WheatsTime;
        WaveTimer = WaveTime;
        ConsumptionTimer = ConsumptionTime;

        FarmersTimer = FarmersTime;
        WarriorsTimer = WarriorsTime;

        IsMultiply = true;

        FarmersText.text = Mathf.Round(Farmers).ToString();
        WarriorsText.text = Mathf.Round(Warriors).ToString();
        EnemiesText.text = $"Врагов на следующей волне: {Enemies}\nВолна: №{WaveNumber}";
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
        if (WaveNumber == 31) { TheEnd(0); }
        WaveTimer -= Time.deltaTime;
        WaveImage.fillAmount = WaveTimer / WaveTime;
        WaveText.text = Mathf.Round(WaveTimer).ToString();
        if (WaveTimer <= 0)
        {
            EnemiesTotalHP = Enemies * EnemyHP;
            WarriorsTotalHP = Warriors * WarriorHP;
            FarmersTotalHP = Farmers * FarmerHP;
            while (true)
            {
                if (WarriorsTotalHP < 0) { WarriorsTotalHP = 0; }

                WarriorsTotalDmg = WarriorsTotalHP / WarriorHP * WarriorDmg;
                FarmersTotalDmg = FarmersTotalHP / FarmerHP * FarmerDmg;

                EnemiesTotalHP -= WarriorsTotalDmg;

                if (EnemiesTotalHP <= 0) { break; } // Если мы убиваем сразу всех

                if (EnemiesTotalHP % EnemyHP > 0) { EnemiesTotalHP += EnemiesTotalHP % EnemyHP; } // Округляем в бОльшую сторону.
                EnemiesTotalDmg = EnemiesTotalHP / EnemyHP * EnemyDmg;

                WarriorsTotalHP -= EnemiesTotalDmg;

                if (WarriorsTotalHP <= 0)
                {
                    WarriorsTotalHP = 0;
                    EnemiesTotalHP -= FarmersTotalDmg;

                    if (EnemiesTotalHP <= 0) { break; } // Если мы убиваем сразу всех

                    if (EnemiesTotalHP % EnemyHP > 0) { EnemiesTotalHP += EnemiesTotalHP % EnemyHP; } // Округляем в бОльшую сторону.
                    FarmersTotalHP -= EnemiesTotalHP / EnemyHP * EnemyDmg;

                    if (FarmersTotalHP <= 0) { FarmersTotalHP = 0; TheEnd(1); break; }
                    if (FarmersTotalHP % FarmerHP > 0) { FarmersTotalHP += FarmersTotalHP % FarmerHP; } // Округляем в бОльшую сторону.
                }
            }

            Farmers = FarmersTotalHP / FarmerHP;
            Warriors = WarriorsTotalHP / WarriorHP;

            WaveNumber++;

            if (IsMultiply) { Enemies *= 2; }
            else { Enemies += 2; }
            IsMultiply = !IsMultiply; 

            FarmersText.text = Mathf.Round(Farmers).ToString();
            WarriorsText.text = Mathf.Round(Warriors).ToString();
            EnemiesText.text = $"Врагов на следующей волне: {Enemies}\nВолна: №{WaveNumber}";

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
                else { TheEnd(1); }
            }
            ConsumptionTimer = ConsumptionTime;
        }
    }

    void TheEnd(int a)
    {
        if (a == 1) { PopUpText.text = "You lost!"; }

        PopUp.SetActive(true);
        PopUp.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(PopUp, new Vector3(1, 1, 1), 0.5f).setEase(easeType).setOnComplete(TimeScale);
    }

    void TimeScale() { Time.timeScale = 0; }
}