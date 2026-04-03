using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwanner : MonoBehaviour
{
    public GameObject[] cars_spwanned;
    public GameObject target;

    private GameObject[] pool = new GameObject[20];
    int index = 0;

    void Start()
    {
        // create pool of 30 cars
        for (int i = 0; i < 20; i++)
        {
            pool[i] = Instantiate(cars_spwanned[index]);
            pool[i].SetActive(false);

            index++;

            if (index >= cars_spwanned.Length)
            {
                index = 0;
            }
        }

        StartCoroutine(car_spwannning());
    }

    void Update()
{
    for (int i = 0; i < pool.Length; i++)
    {
        if (pool[i].activeInHierarchy)
        {
            // check distance on Z axis
            if (pool[i].transform.position.z < target.transform.position.z - 30f)
            {
                pool[i].SetActive(false);
            }
        }
    }
}

    IEnumerator car_spwannning()
    {
        while (true)
        {
        
            SpawnCar();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnCar()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = target.transform.position
                    + target.transform.forward * 100f
                    + target.transform.right * Random.Range(0,20);

                pool[i].SetActive(true);
                break;
            }
        }
    }
}