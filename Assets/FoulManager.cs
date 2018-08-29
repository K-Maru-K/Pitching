using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulManager : MonoBehaviour
{

    public GameObject ball;
    public JointManager jointmanager;
    public DeadBallManager deadballmanager;
    public int flag = 0;
    // Use this for initialization
    //   void Start () {

    //}

    // Update is called once per frame
    //void Update () {

    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball && deadballmanager.flag == 0&&jointmanager.swing==1)
        {
            flag = 1;
            jointmanager.Foullabeling();
            ball.SetActive(false);
            jointmanager.Wait();
        }
    }
}
