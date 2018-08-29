using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASE1 : MonoBehaviour
{

    public GameObject ball;
    public JointManager jointmanager;
    public GroundManager groundmanager;
    public FoulManager foulmanager;
    public FoulManager foulmanager1;
    public FoulManager foulmanager2;

    public int flag = 0;

    void Update()
    {
        if (flag > 0 && foulmanager.flag == 0 && foulmanager1.flag == 0 && foulmanager2.flag == 0 && jointmanager.ss == 0)
        {
            jointmanager.ss = 1;
            jointmanager.detect();
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.SetActive(false);
            jointmanager.hit();
            jointmanager.count_reset();
            jointmanager.Wait();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            flag++;

        }
    }
}
