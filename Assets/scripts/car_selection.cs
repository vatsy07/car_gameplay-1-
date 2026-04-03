using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class car_selection : MonoBehaviour
{
    public GameObject[] cars;
    int index = 0;

    void Start()
    {
        ShowCar();
    }

    public void Next()
    {
        cars[index].SetActive(false);

        index = (index + 1) % cars.Length;

        ShowCar();
    }

    public void Previous()
    {
        cars[index].SetActive(false);

        index--;
        if (index < 0)
            index = cars.Length - 1;

        ShowCar();
    }

    void ShowCar()
    {
        cars[index].SetActive(true);
    }

    public void StartGame()
{
    PlayerPrefs.SetInt("SelectedCarIndex", index);
    SceneManager.LoadScene(1); 
}
}