using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public float speedy;
    public float directiony;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speedy * directiony * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            
        }
        if (other.gameObject.tag == "Muro")
        {
            Destroy(this.gameObject);
        }

    }
}
