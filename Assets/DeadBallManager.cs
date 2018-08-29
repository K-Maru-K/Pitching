using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBallManager : MonoBehaviour
{

    public GameObject ball;
    public JointManager jointmanager;
    public int flag = 0;


    // Use this for initialization
    void Start () {
        flag = 0;
   }

    // Update is called once per frame
    //void Update () {

    //}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == ball)
        {
            flag = 1;
            jointmanager.DeadBalllabeling();
        }

    }

}
