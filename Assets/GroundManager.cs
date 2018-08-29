using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

    public int flag = 0;
    public GameObject ball;
    public GameObject grobe;
    public JointManager jointmanager;
    public GrobeManager grobemanager;
    //public BASE1_ZONE basezone1_1;
    //public BASE1_ZONE basezone1_2;
    //public BASE1_ZONE basezone1_3;
    //public BASE1_ZONE basezone1_4;
    //public BASE1_ZONE basezone1_5;
    //public BASE1_ZONE basezone1_6;
    //public BASE1_ZONE basezone1_7;
    //public BASE2_ZONE basezone2_1;
    //public BASE2_ZONE basezone2_2;
    //public BASE2_ZONE basezone2_3;
    //public BASE3_ZONE basezone3_2;
    //public BASE3_ZONE basezone3_3;

    private int sum = 0;

    // Use this for initialization
    void Start()
    {
        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //sum = basezone1_1.send + basezone1_2.send + basezone1_3.send + basezone1_4.send + basezone1_5.send + basezone1_6.send + basezone1_7.send + basezone2_2.send + basezone2_3.send + basezone2_1.send + basezone3_2.send + basezone3_3.send;

        //        Debug.Log(flag);
        if (/*sum > 120 &&*/ jointmanager.ss == 0 && flag > 0 && (ball.GetComponent<Rigidbody>().velocity.x) * (ball.GetComponent<Rigidbody>().velocity.x) + (ball.GetComponent<Rigidbody>().velocity.y) * (ball.GetComponent<Rigidbody>().velocity.y) + (ball.GetComponent<Rigidbody>().velocity.z) * (ball.GetComponent<Rigidbody>().velocity.z) < 2 && (5f + ball.transform.position.y) * (5f + ball.transform.position.y) < 1)
        {
            Debug.Log("StopReset");
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.SetActive(false);
            if (jointmanager.swing == 1)
            {
                jointmanager.OUT();
                jointmanager.count_reset();
            }
            jointmanager.detect();
            jointmanager.Wait();
            flag = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == ball && jointmanager.hold == -1 && flag >= 0)
        {
            Debug.Log("GroundTouch");
            flag++;
        }

    }

}
