using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeZone : MonoBehaviour
{
    public GameObject ball;
    public GameObject Grobe;
    public JointManager jointmanager;
    public DeadBallManager deadballmanager;
    public FoulManager foulmanager;
    public FoulManager foulmanager1;
    public FoulManager foulmanager2;
    public GroundManager groundmanager;
    //    public GroundManager groundmanager;
    private int flag = 0;

    // Label disappear
    void Start()
    {
        jointmanager.labeling_reset();
    }

    // Update is called once per frame

    void Update()
    {

        if ((flag >= 0 && ball.transform.position.z - Grobe.transform.position.z < 0.01) && deadballmanager.flag == 0 && foulmanager.flag == 0 && foulmanager1.flag == 0 && foulmanager2.flag == 0)
        {

            if (flag == 0)
            {
                jointmanager.BALLlabeling();
                flag = -1;
            }
            else if (flag > 0)
            {
                jointmanager.STRIKElabeling();
                flag = -1;
            }
        }

        if (jointmanager.send == 1)
        {
            flag = 0;
            jointmanager.send = 0;
            jointmanager.labeling_reset();
        }
        // Debug.Log(flag);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball && deadballmanager.flag == 0 && foulmanager.flag == 0&&groundmanager.flag == 0)
        {
            flag++;
            Debug.Log("ZonePass");
        }

    }

}
