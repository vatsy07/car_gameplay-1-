using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class car_movment : MonoBehaviour
{
    public GameObject spwaner;
    public AudioSource s1;
    public AudioClip car_moving;
    public AudioClip car_honking;
    public AudioClip car_crash;
    public Image i1;
    float maxSteerVelocity = 50.0f;
    float speed = 160.0f;
    public GameObject[] array;
    public Transform model;

    int cars_overtaken = 0;
    GameObject prev1;

    public Text t1;
    public Image i;
    
    List<GameObject> previously_object = new List<GameObject>();

    float x;
    float y;
    int n;
    int index_tobedelated = 1;

    Rigidbody rb;

    bool isMovingSoundPlaying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        n = array.Length;
    }

    void FixedUpdate()
    {
        float targetSpeed = speed * Mathf.Abs(x);
        float newZ;

        if (x != 0)
        {
            newZ = Mathf.Lerp(rb.velocity.z, targetSpeed, Time.deltaTime * 3f);
        }
        else
        {
            newZ = 0;
        }

        rb.velocity = new Vector3(
            maxSteerVelocity * y,
            rb.velocity.y,
            newZ
        );
    }

    void Update()
    {
        x = Input.GetAxisRaw("Vertical");
        y = Input.GetAxisRaw("Horizontal");
        
        t1.text = cars_overtaken.ToString();

        float tilt = -y * 10f;

        model.localRotation = Quaternion.Lerp(
            model.localRotation,
            Quaternion.Euler(0, 0, tilt),
            Time.deltaTime * 5f
        );

        HandleSound();
    }

    void HandleSound()
    {
        if (Mathf.Abs(x) > 0)
        {
            if (!isMovingSoundPlaying)
            {
                s1.clip = car_moving;
                s1.loop = true;
                s1.Play();
                isMovingSoundPlaying = true;
            }
        }
        else
        {
            if (isMovingSoundPlaying)
            {
                s1.Stop();
                isMovingSoundPlaying = false;

                s1.clip = car_honking;
                s1.loop = false;
                s1.Play();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collider"))
        {
            int index = UnityEngine.Random.Range(0, array.Length);
            prev1 = array[index];

            GameObject spawned = Instantiate(prev1, 
                new Vector3(-2.1776f, 5.99f, transform.position.z + 300f), 
                Quaternion.identity
            );

            previously_object.Add(spawned);
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            cars_overtaken = cars_overtaken + 1;
        }
        else if (other.gameObject.CompareTag("collider2"))
        {
            StartCoroutine(died());
        }
    }

    IEnumerator died()
    {
        yield return new WaitForSeconds(150f);

        if (previously_object.Count > 0 && index_tobedelated < previously_object.Count)
        {
            if (previously_object[index_tobedelated] != null)
            {
                previously_object[index_tobedelated].SetActive(false);
                index_tobedelated++;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            s1.Stop();
            s1.clip = car_crash;
            s1.loop = false;
            s1.Play();
            i.gameObject.SetActive(true);
            spwaner.SetActive(false);
        }
    }
}