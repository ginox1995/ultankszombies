using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mausoleum : MonoBehaviour
{
    public int healthPoints;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints==0)
        {
            MausoleumDestroy();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Bullet")
        {
            healthPoints--;
        }
    }
    private void MausoleumDestroy()
    {
        Destroy(gameObject);
    }
}
