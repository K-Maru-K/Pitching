using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrobeManager : MonoBehaviour
{
    public GameObject ball;
    public JointManager jointmanager;
    public GroundManager groundmanager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            Debug.Log("BallCatchReset");
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.transform.position = gameObject.transform.position;
            ball.SetActive(false);
            jointmanager.Wait();
        }
    }
}
