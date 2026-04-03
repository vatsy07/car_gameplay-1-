using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class enemy_script : MonoBehaviour
{
    public GameObject target;
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
{
    if(gameObject.transform.position.z - target.transform.position.z > 20.0f)
    {
        rb.AddForce(new Vector3(0,0,30));
    }  
    else
    {
        float xDir = target.transform.position.x - transform.position.x;
        float sideSpeed = Mathf.Clamp(xDir, -1f, 1f); 
        rb.velocity = new Vector3(sideSpeed * 10f, 0f, 20f);        
    }  
}
    


    
    
    void OnCollisionEnter(Collision collision)
{
    if(collision.gameObject.CompareTag("enemy"))
    {
        Debug.Log("COLLIDED WITH ENEMY");
        gameObject.SetActive(false);
    }
}
}
