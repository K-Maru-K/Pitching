using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASE3_ZONE : MonoBehaviour
{

    // Use this for initialization
    public GameObject ball;
    public JointManager jointmanager;
    public GroundManager groundmanager;
    public FoulManager foulmanager;
    public FoulManager foulmanager1;
    public FoulManager foulmanager2;

    public int flag = 0;
    public int send = 0;

    void Update()
    {
        if ((ball.GetComponent<Rigidbody>().velocity.x) * (ball.GetComponent<Rigidbody>().velocity.x) + (ball.GetComponent<Rigidbody>().velocity.y) * (ball.GetComponent<Rigidbody>().velocity.y) + (ball.GetComponent<Rigidbody>().velocity.z) * (ball.GetComponent<Rigidbody>().velocity.z) < 3)
        {
            if (flag > 0 && foulmanager.flag == 0 && foulmanager1.flag == 0 && foulmanager2.flag == 0 && jointmanager.ss == 0)
            {
                jointmanager.ss = 1;
                jointmanager.detect();
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                ball.SetActive(false);
                jointmanager.hit3();
                jointmanager.count_reset();
                jointmanager.Wait();
            }
            else
            {
                send ++;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball && (ball.GetComponent<Rigidbody>().velocity.x) * (ball.GetComponent<Rigidbody>().velocity.x) + (ball.GetComponent<Rigidbody>().velocity.y) * (ball.GetComponent<Rigidbody>().velocity.y) + (ball.GetComponent<Rigidbody>().velocity.z) * (ball.GetComponent<Rigidbody>().velocity.z) < 3)
        {
            flag++;

        }
    }

}
