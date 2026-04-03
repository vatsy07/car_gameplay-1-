using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] cars;
    public Transform spawnPoint; // 👈 assign this in Inspector

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }

        cars[index].SetActive(true);
        cars[index].transform.position = spawnPoint.position; // 👈 move to spawn
        Debug.Log("Activated: " + cars[index].name);
    }
}