using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class button_manager : MonoBehaviour
{
    public void restart_push()
    {
        Debug.Log("UTTON PUSHED :-");
        SceneManager.LoadScene("car_gameplay");
    }
}
