using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Kinect = Windows.Kinect;

public class JointManager : MonoBehaviour
{

    public int send = 0;
    public int hold = 0;
    public int swing = 0;
    public int ss = 0;
    public Dictionary<Kinect.JointType, Vector3> joints = new Dictionary<Kinect.JointType, Vector3>();
    public Dictionary<int, GameObject> Strike = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> Ball = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> Out = new Dictionary<int, GameObject>();
    public Renderer rBall;
    public GameObject BodySourceManager;
    public GameObject ball;
    public GameObject MainCamera;
    public GameObject catcherCamera;
    public GameObject batterCamera;
    public GameObject Grobe;
    public GameObject StrikeLabelObject;
    public GameObject BallLabelObject;
    public GameObject DeadBallLabelObject;
    public GameObject FoulLabelObject;
    public GameObject StrikeOutLabelObject;
    public GameObject WalkLabelObject;
    public GameObject BASE1LabelObject;
    public GameObject BASE2LabelObject;
    public GameObject BASE3LabelObject;
    public GameObject HomerunLabelObject;
    public GameObject OutLabelObject;
    public GameObject DoublePlayLabelObject;
    //public GameObject RunnerLabelObject1;
    //public GameObject RunnerLabelObject2;
    //public GameObject RunnerLabelObject3;
    public GameObject BaseSpace;
    public GameObject batter;
    public GameObject strike1;
    public GameObject strike2;
    public GameObject strike3;
    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;
    public GameObject ball4;
    public GameObject out1;
    public GameObject out2;
    public GameObject out3;
    volatile public GameObject unitychan_Left_shoulder;
    volatile public GameObject unitychan_Left_arm;
    volatile public GameObject unitychan_Left_ForeArm;
    volatile public GameObject unitychan_Right_shoulder;
    volatile public GameObject unitychan_Right_arm;
    volatile public GameObject unitychan_Right_ForeArm;
    public DeadBallManager deadballmanager;
    public GroundManager groundmanager;
    public FoulManager foulmanager;
    public FoulManager foulmanager1;
    public FoulManager foulmanager2;
    //public BASE1_ZONE basezone1_1;
    //public BASE1_ZONE basezone1_2;
    //public BASE1_ZONE basezone1_3;
    //public BASE1_ZONE basezone1_4;
    //public BASE1_ZONE basezone1_5;
    //public BASE1_ZONE basezone1_6;
    //public BASE1_ZONE basezone1_7;
    //public BASE2_ZONE basezone2_2;
    //public BASE2_ZONE basezone2_3;
    //public BASE2_ZONE basezone2_1;
    //public BASE3_ZONE basezone3_2;
    //public BASE3_ZONE basezone3_3;
    public BASE1 base1_1;
    public BASE1 base1_2;
    public BASE1 base1_3;
    public BASE1 base1_4;
    public BASE1 base1_5;
    public BASE1 base1_6;
    public BASE1 base1_7;
    public BASE1 base1_8;
    public BASE2 base2_1;
    public BASE2 base2_2;
    public BASE2 base2_3;
    public BASE2 base2_4;
    public BASE2 base2_5;
    public BASE3 base3_1;
    public BASE3 base3_2;
    public BASE3 base3_3;
    public BASE3 base3_4;
    public BASE3 base3_5;
    public OUT out1p;
    public OUT out2p;
    public OUT out3p;
    public OUT out4p;
    public OUT out5p;
    public OUT out6p;
    public DoublePlay double1;
    public DoublePlay double2;
    public DoublePlay double3;
    public DoublePlay double4;
    public DoublePlay double5;
    public DoublePlay double6;
    public DoublePlay double7;
    public DoublePlay double8;
    public DoublePlay double9;
    public DoublePlay double10;
    public Homerun home1;
    public Homerun home2;
    public Homerun home3;
    public Homerun home4;
    public Homerun home5;
    public Camera Pcam;
    public Camera Ccam;
    public Camera Bcam;
    public Camera BTcam;
    //public UnityEngine.UI.Text P_Count;
    //public UnityEngine.UI.Text S_Count;
    //public UnityEngine.UI.Text Inning;

    private Kinect.JointType shoulder;
    private Kinect.JointType hand;
    private float Hander = 0;
    private float vRan = 0;
    private float hRan = 0;
    private float yRan = 0;
    private float zRan = 0;
    private int num = 0;
    private int flag = 0;
    private int changer = -1;
    private int set = 0;
    private int strike_count = 0;
    private int ball_count = 0;
    private int out_count = 0;
    //private int p_count = 0;
    //private int s_count = 0;
    //private bool run1 = false;
    //private bool run2 = false;
    //private bool run3 = false;
    //private int inning_count = 1;
    private Vector3 sGrobe;        //Grobe start position
    private Vector3 CH;
    private Quaternion cCamera;        //Grobe start position
    private Quaternion bCamera;        //Grobe start position
    private Quaternion unitychan_Left_shoulder_s;
    private Quaternion unitychan_Left_arm_s;
    private Quaternion unitychan_Left_ForeArm_s;
    private Quaternion unitychan_Right_shoulder_s;
    private Quaternion unitychan_Right_arm_s;
    private Quaternion unitychan_Right_ForeArm_s;
    private BodySourceManager _BodyManager;
    private Dictionary<int, string> inning = new Dictionary<int, string>()
    {
        { 1, "1回表" },
        { 2, "1回裏" },
        { 3, "2回表" },
        { 4, "2回裏" },
        { 5, "3回表" },
        { 6, "3回裏" },
        { 7, "4回表" },
        { 8, "4回裏" },
        { 9, "5回表" },
        { 10, "5回裏" },
        { 11, "6回表" },
        { 12, "6回裏" },
        { 13, "7回表" },
        { 14, "7回裏" },
        { 15, "8回表" },
        { 16, "8回裏" },
        { 17, "9回表" },
        { 18, "9回裏" },
    };

    volatile private bool waitFlg = false;

    //pitching interval time 
    public void Wait()
    {
        StartCoroutine(timer());

    }
    private IEnumerator timer()
    {
        if (waitFlg == false)
        {
            waitFlg = true;
            hold = 0;
            flag = -3;
            if (swing == 1)
            {
                unitychan_Left_shoulder.transform.rotation = unitychan_Left_shoulder_s;
                unitychan_Left_arm.transform.rotation = unitychan_Left_arm_s;
                unitychan_Left_ForeArm.transform.rotation = unitychan_Left_ForeArm_s;
                unitychan_Right_shoulder.transform.rotation = unitychan_Right_shoulder_s;
                unitychan_Right_arm.transform.rotation = unitychan_Right_arm_s;
                unitychan_Right_ForeArm.transform.rotation = unitychan_Right_ForeArm_s;
            }
            swing = 0;
            yield return new WaitForSeconds(2.0f);
            //           Grobe.SetActive(false);
            Debug.Log("play!");
            changer = -1;
            send = 1;
            ball.SetActive(true);
            ball.transform.position = joints[hand];
            flag = -2;
            //            Grobe.SetActive(true);
            Grobe.transform.position = sGrobe;
            foulmanager.flag = 0;
            foulmanager1.flag = 0;
            foulmanager2.flag = 0;
            deadballmanager.flag = 0;
            catcherCamera.transform.rotation = cCamera;
            batterCamera.transform.rotation = bCamera;
            groundmanager.flag = 0;
            set = 0;
            ss = 0;
            waitFlg = false;
        }
    }


//labeling
public void STRIKElabeling()
    {
        Debug.Log("STRIKE!!");
        StrikeLabelObject.SetActive(true);
        Strike[strike_count].SetActive(true);
        strike_count++;
    }

    public void BALLlabeling()
    {
        Debug.Log("BALL!!");
        BallLabelObject.SetActive(true);
        Ball[ball_count].SetActive(true);
        ball_count++;
    }

    public void Foullabeling()
    {
        Debug.Log("FoulBall");
        FoulLabelObject.SetActive(true);
        if (strike_count < 2)
        {
            Strike[strike_count].SetActive(true);
            strike_count++;
        }
    }

    public void DeadBalllabeling()
    {
        Debug.Log("DeadBall");
        DeadBallLabelObject.SetActive(true);
        Wait();
        count_reset();

        //if (run1 == false)
        //{
        //    run1 = true;
        //    RunnerLabelObject1.SetActive(true);
        //}
        //else if (run2 == false)
        //{
        //    run2 = true;
        //    RunnerLabelObject2.SetActive(true);
        //}
        //else if (run3 == false)
        //{
        //    run3 = true;
        //    RunnerLabelObject3.SetActive(true);
        //}
        //else
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //        P_Count.text = "" + p_count;

        //    }
        //    else
        //    {
        //        s_count++;
        //        S_Count.text = "" + s_count;
        //    }

        //}
    }

    public void count_reset()
    {
        for (int i = 0; i < 3; i++)
        {
            Strike[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            Ball[i].SetActive(false);
        }
        strike_count = 0;
        ball_count = 0;
    }

    public void out_count_reset()
    {
        for (int i = 0; i < 3; i++)
        {
            Out[i].SetActive(false);
        }
        //RunnerLabelObject1.SetActive(false);
        //RunnerLabelObject2.SetActive(false);
        //RunnerLabelObject3.SetActive(false);
        //run1 = false;
        //run2 = false;
        //run3 = false;
        //inning_count++;
        //Inning.text = inning[inning_count];
        out_count = 0;
    }

    public void labeling_reset()
    {
        Debug.Log("RESET!!");
        BallLabelObject.SetActive(false);
        StrikeLabelObject.SetActive(false);
        FoulLabelObject.SetActive(false);
        DeadBallLabelObject.SetActive(false);
        StrikeOutLabelObject.SetActive(false);
        WalkLabelObject.SetActive(false);
        BASE1LabelObject.SetActive(false);
        BASE2LabelObject.SetActive(false);
        BASE3LabelObject.SetActive(false);
        HomerunLabelObject.SetActive(false);
        OutLabelObject.SetActive(false);
        DoublePlayLabelObject.SetActive(false);
    }

    public void hit()
    {
        //if (run3 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //        P_Count.text = "" + p_count;
        //    }
        //    else
        //    {
        //        s_count++;
        //        S_Count.text = "" + s_count;
        //    }
        //}
        //run3 = run2;
        //run2 = run1;
        //run1 = true;
        //RunnerLabelObject3.SetActive(run3);
        //RunnerLabelObject2.SetActive(run2);
        //RunnerLabelObject1.SetActive(run1);
        BASE1LabelObject.SetActive(true);
    }

    public void hit2()
    {
        //if (run3 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //if (run2 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //run3 = run1;
        //run2 = true;
        //run1 = false;
        //P_Count.text = "" + p_count;
        //S_Count.text = "" + s_count;
        //RunnerLabelObject3.SetActive(run3);
        //RunnerLabelObject2.SetActive(run2);
        //RunnerLabelObject1.SetActive(run1);
        BASE2LabelObject.SetActive(true);
    }

    public void hit3()
    {
        //if (run3 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //if (run2 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //if (run1 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //run3 = true;
        //run2 = false;
        //run1 = false;
        //P_Count.text = "" + p_count;
        //S_Count.text = "" + s_count;
        //RunnerLabelObject3.SetActive(true);
        //RunnerLabelObject2.SetActive(false);
        //RunnerLabelObject1.SetActive(false);
        BASE3LabelObject.SetActive(true);
    }

    public void homerun()
    {
        //if (inning_count % 2 == 1)
        //{
        //    p_count++;
        //}
        //else
        //{
        //    s_count++;
        //}
        //if (run3 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //if (run2 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //if (run1 == true)
        //{
        //    if (inning_count % 2 == 1)
        //    {
        //        p_count++;
        //    }
        //    else
        //    {
        //        s_count++;
        //    }
        //}
        //P_Count.text = "" + p_count;
        //S_Count.text = "" + s_count;
        //run3 = false;
        //run2 = false;
        //run1 = false;
        //RunnerLabelObject3.SetActive(false);
        //RunnerLabelObject2.SetActive(false);
        //RunnerLabelObject1.SetActive(false);
        HomerunLabelObject.SetActive(true);
    }

    public void DoublePlay()
    {
        Out[out_count].SetActive(true);
        out_count++;
        if (out_count < 3)
        {
            //if (run1 == true)
            //{
            //    run1 = false;
            //    Out[out_count].SetActive(true);
            out_count++;
            //    RunnerLabelObject1.SetActive(false);
            DoublePlayLabelObject.SetActive(true);
            //}
            //else if (run2 == true)
            //{
            //    run2 = false;
            //    Out[out_count].SetActive(true);
            //    out_count++;
            //    RunnerLabelObject2.SetActive(false);
            //    DoublePlayLabelObject.SetActive(true);
            //}
            //else if (run3 == true)
            //{
            //    run3 = false;
            //    Out[out_count].SetActive(true);
            //    out_count++;
            //    RunnerLabelObject3.SetActive(false);
            //    DoublePlayLabelObject.SetActive(true);
            //}
            //else
            //{
            //    OutLabelObject.SetActive(true);
            //}
        }
        else
        {
            OutLabelObject.SetActive(true);
        }

        if (out_count >= 3)
        {
            out_count_reset();
        }

    }

    public void OUT()
    {
        Out[out_count].SetActive(true);
        out_count++;
        OutLabelObject.SetActive(true);
        if (out_count >= 3)
        {
            out_count_reset();
        }
    }

    public void detect()
    {
        //basezone1_1.flag = 0;
        //basezone1_2.flag = 0;
        //basezone1_3.flag = 0;
        //basezone1_4.flag = 0;
        //basezone1_5.flag = 0;
        //basezone1_6.flag = 0;
        //basezone1_7.flag = 0;
        //basezone2_2.flag = 0;
        //basezone2_3.flag = 0;
        //basezone2_1.flag = 0;
        //basezone3_2.flag = 0;
        //basezone3_3.flag = 0;
        base1_1.flag = 0;
        base1_2.flag = 0;
        base1_3.flag = 0;
        base1_4.flag = 0;
        base1_5.flag = 0;
        base1_6.flag = 0;
        base1_7.flag = 0;
        base1_8.flag = 0;
        base2_1.flag = 0;
        base2_2.flag = 0;
        base2_3.flag = 0;
        base2_4.flag = 0;
        base2_5.flag = 0;
        base3_1.flag = 0;
        base3_2.flag = 0;
        base3_3.flag = 0;
        base3_4.flag = 0;
        base3_5.flag = 0;
        out1p.flag = 0;
        out2p.flag = 0;
        out3p.flag = 0;
        out4p.flag = 0;
        out5p.flag = 0;
        out6p.flag = 0;
        double1.flag = 0;
        double2.flag = 0;
        double3.flag = 0;
        double4.flag = 0;
        double5.flag = 0;
        double6.flag = 0;
        double7.flag = 0;
        double8.flag = 0;
        double9.flag = 0;
        double10.flag = 0;
        home1.flag = 0;
        home2.flag = 0;
        home3.flag = 0;
        home4.flag = 0;
        home5.flag = 0;

        //basezone1_1.send=0;
        //basezone1_2.send = 0;
        //basezone1_3.send = 0;
        //basezone1_4.send = 0;
        //basezone1_5.send = 0;
        //basezone1_6.send = 0;
        //basezone1_7.send = 0;
        //basezone2_2.send = 0;
        //basezone2_3.send = 0;
        //basezone2_1.send = 0;
        //basezone3_2.send = 0;
        //basezone3_3.send = 0;

    }

    //Use this for initialization
    void Start()
    {
        ball.SetActive(false);
        //Grobe start position get
        sGrobe = Grobe.transform.position;
        shoulder = Kinect.JointType.ShoulderRight;
        hand = Kinect.JointType.HandRight;
        Hander = 1;
        //get camera position
        cCamera = catcherCamera.transform.rotation;
        bCamera = batterCamera.transform.rotation;
        //count board setting
        Strike[0] = strike1;
        Strike[1] = strike2;
        Strike[2] = strike3;
        Ball[0] = ball1;
        Ball[1] = ball2;
        Ball[2] = ball3;
        Ball[3] = ball4;
        Out[0] = out1;
        Out[1] = out2;
        Out[2] = out3;
        count_reset();
        out_count_reset();
        labeling_reset();
        //RunnerLabelObject1.SetActive(false);
        //RunnerLabelObject2.SetActive(false);
        //RunnerLabelObject3.SetActive(false);
        //run1 = false;
        //run2 = false;
        //run3 = false;

        //p_count = 0;
        //s_count = 0;
        //inning_count = 1;
        //Inning.text = inning[inning_count];
        //P_Count.text = "" + p_count;
        //S_Count.text = "" + s_count;
        // 各参照の初期化
        unitychan_Left_shoulder_s = unitychan_Left_shoulder.transform.rotation;
        unitychan_Left_arm_s = unitychan_Left_arm.transform.rotation;
        unitychan_Left_ForeArm_s = unitychan_Left_ForeArm.transform.rotation;
        unitychan_Right_shoulder_s = unitychan_Right_shoulder.transform.rotation;
        unitychan_Right_arm_s = unitychan_Right_arm.transform.rotation;
        unitychan_Right_ForeArm_s = unitychan_Right_ForeArm.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (BodySourceManager == null)
        {
            Debug.Log("ERROR!!(1)");
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            Debug.Log("ERROR!!(2)");
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            Debug.Log("ERROR!!(3)");
            return;
        }

        //player capture
        int i = 0;
        foreach (var body in data)
        {
            if (body.IsTracked == true)
            {
                num = i;
                ball.SetActive(true);
                break;
            }
            i++;
        }

        //get player position
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = data[num].Joints[jt];
            CH = GetVector3FromJoint(sourceJoint);
            joints[jt] = new Vector3(-CH.x, CH.y, CH.z);
        }

        //camera set
        MainCamera.transform.position = joints[Kinect.JointType.Head];

        //key catch config
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("RightArrow");
            changer = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("LeftArrow");
            changer = 2;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow");
            changer = 3;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow");
            changer = 4;
        }
        else if (Input.GetKey(KeyCode.N))
        {
            Debug.Log("Nukkle");
            changer = 5;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Curve");
            changer = 6;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            changer = 7;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Strate");
            changer = -1;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("RightHand");
            Hander = 1;
            shoulder = Kinect.JointType.ShoulderRight;
            hand = Kinect.JointType.HandRight;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            Debug.Log("LeftHand");
            Hander = -1;
            shoulder = Kinect.JointType.ShoulderLeft;
            hand = Kinect.JointType.HandLeft;
        }
        else if (Input.GetKey(KeyCode.P))
        {
            Pcam.depth = 1;
            Ccam.depth = 0;
            Bcam.depth = 0;
            BTcam.depth = 0;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            Pcam.depth = 0;
            Ccam.depth = 1;
            Bcam.depth = 0;
            BTcam.depth = 0;
        }
        else if (Input.GetKey(KeyCode.B))
        {
            Pcam.depth = 0;
            Ccam.depth = 0;
            Bcam.depth = 1;
            BTcam.depth = 0;
        }
        else if (Input.GetKey(KeyCode.T))
        {
            Pcam.depth = 0;
            Ccam.depth = 0;
            Bcam.depth = 0;
            BTcam.depth = 1;
        }


        if (flag == -2 && ((joints[shoulder].z - joints[hand].z) < 3 || joints[shoulder].y > joints[hand].y + 1))
        {
            flag = 0;
        }

        //set position
        if ((joints[shoulder].z - joints[hand].z) <= -1 || joints[shoulder].z - joints[hand].z <= -1)
        {
            set = 1;
        }

        //set ball & throw config
        if (set == 1 && flag == 0 && (joints[shoulder].z - joints[hand].z) > 3 && joints[shoulder].y <= joints[hand].y)
        {
            flag = changer;
            //            Debug.Log(flag);
            hold = -1;
            if (changer == 5)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 3f, -180f);
                ball.GetComponent<Rigidbody>().AddForce(0, 0, -8f * 2);
            }
            else if (changer == 6)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(-20f * Hander, 20f, -150f);
                ball.GetComponent<Rigidbody>().AddForce(0, 0, -7f * 2);
            }
            else
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, -2f, -250f);
                ball.GetComponent<Rigidbody>().AddForce(0, 0, -100f);
            }
        }
        if (set == 1 && flag == 0 && (joints[shoulder].z - joints[hand].z) > 3 && joints[shoulder].y < joints[hand].y + 2)
        {
            flag = changer;
            //            Debug.Log(flag);
            hold = -1;
            if (changer == 5)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 20f, -180f);
                ball.GetComponent<Rigidbody>().AddForce(0, 0, -8f * 2);
            }
            else if (changer == 6)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(-20f * Hander, 25f, -150f);
                ball.GetComponent<Rigidbody>().AddForce(0, 0, -7f * 2);
            }
            else
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 14f, -250f);
                ball.GetComponent<Rigidbody>().AddForce(0, 0, -100f);
            }
        }
        if (set == 1 && flag == 0 && (joints[shoulder].z - joints[hand].z) > 3 && joints[shoulder].y < joints[hand].y + 4)
        {
            flag = changer;
            //            Debug.Log(flag);
            hold = -1;
            if (changer == 5)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 15f, -180f);
                ball.GetComponent<Rigidbody>().AddForce(0, -20f, -8f * 2);
            }
            else if (changer == 6)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(-20f * Hander, 30f, -150f);
                ball.GetComponent<Rigidbody>().AddForce(0, -20f, -7f * 2);
            }
            else
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 20f, -250f);
                ball.GetComponent<Rigidbody>().AddForce(0, -30f, -100f);
            }
        }

        else if (flag == 0)
        {
            if (joints[hand] == null)
            {
                Debug.Log("ERROR!!(4)");
                flag = 0;
                changer = -1;
            }
            else
            {
                hold = 0;
                ball.transform.position = joints[hand];
                Debug.Log("Hold");
            }
        }

        //changing ball config
        if (flag == 5)
        {
            ball.GetComponent<Rigidbody>().AddForce(Random.value * 500f - 250f, 0, 0);
        }
        else if (joints[Kinect.JointType.Head].z - ball.transform.position.z > 30 && flag == 7)
        {
            rBall.enabled = false;
            flag = -1;
        }
        else if (flag == 6 && joints[Kinect.JointType.Head].z - ball.transform.position.z > 5)
        {
            ball.GetComponent<Rigidbody>().AddForce(150f * Hander, -50f, 0);
        }
        else if (flag > 0 && flag < 5 && joints[Kinect.JointType.Head].z - ball.transform.position.z > 60)
        {
            if (flag == 1)
            {
                ball.GetComponent<Rigidbody>().AddForce(-1000f, 0, 0);
            }
            else if (flag == 2)
            {
                ball.GetComponent<Rigidbody>().AddForce(1000f, 0, 0);
            }
            else if (flag == 3)
            {
                ball.GetComponent<Rigidbody>().AddForce(0, -600f, 0);
            }
            else if (flag == 4)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 5f, -100f);
            }

            flag = -1;
        }


        //Grobe move to estimated ball position
        if (flag == -1 || flag > 4)
        {
            Grobe.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, Grobe.transform.position.z);

        }
        //vanishing config
        if ((Grobe.transform.position.z - ball.transform.position.z) * (Grobe.transform.position.z - ball.transform.position.z) < 0.01)
        {
            rBall.enabled = true;
        }
        //hitting config
        if ((flag == -1 || flag > 4) && (BaseSpace.transform.position.z - ball.transform.position.z) * (BaseSpace.transform.position.z - ball.transform.position.z) < 20)
        {
            Debug.Log("HIT!!");
            vRan = Random.Range(0f, 3f);
            hRan = Random.Range(0f, 3f);
            zRan = Random.Range(0f, 180f);
            yRan = Random.Range(-200f, 200f);

            if (vRan < 1)
            {
                vRan = -3f / 4;
            }
            else if (vRan > 2)
            {
                vRan = 3f / 4;
            }
            else
            {
                vRan = 0;
            }

            if (hRan < 1)
            {
                hRan = -3f / 4;
            }
            else if (hRan > 2)
            {
                hRan = 3f / 4;
            }
            else
            {
                hRan = 0;
            }
            Debug.Log(hRan);
            Debug.Log(vRan);
            if (ball.transform.position.x < BaseSpace.transform.position.x + 1f + 1f && ball.transform.position.x > BaseSpace.transform.position.x - 1f - 1f && ball.transform.position.y < BaseSpace.transform.position.y + 1f + 1f && ball.transform.position.y > BaseSpace.transform.position.y - 1f - 1f)
            //if (ball.transform.position.x < BaseSpace.transform.position.x + hRan + 1f && ball.transform.position.x > BaseSpace.transform.position.x + hRan - 1f && ball.transform.position.y < BaseSpace.transform.position.y + vRan + 1f && ball.transform.position.y > BaseSpace.transform.position.y + vRan - 1f)
            {
                flag = -3;
                unitychan_Left_shoulder.transform.Rotate(new Vector3(0.01f, 0, 0));
                unitychan_Left_arm.transform.Rotate(new Vector3(35.4f, 65f, -30.97f));
                unitychan_Left_ForeArm.transform.Rotate(new Vector3(170.2f, 155.9f, -85.9f));
                unitychan_Right_shoulder.transform.Rotate(new Vector3(1.01f, -2.41f, 13.409f));
                unitychan_Right_arm.transform.Rotate(new Vector3(12.271f, -28.9f, -3.46f));
                unitychan_Right_ForeArm.transform.Rotate(new Vector3(36.81f, 144.88f, 13.67f));
                swing = 1;
                rBall.enabled = true;
                ball.GetComponent<Rigidbody>().velocity = new Vector3((ball.transform.position.z - batter.transform.position.z) / (batter.transform.position.x - ball.transform.position.x) * (50f + zRan), 50f + yRan, 50f + zRan);
                ball.GetComponent<Rigidbody>().AddForce((ball.transform.position.z - batter.transform.position.z) / (batter.transform.position.x - ball.transform.position.x) * (350f + zRan / 2) / 2, (350f + yRan) / 2, (350f + zRan / 2) / 2);
                //ball.GetComponent<Rigidbody>().velocity = new Vector3((ball.transform.position.z - batter.transform.position.z) / (batter.transform.position.x - ball.transform.position.x) * (30f + 200f), 50f + 150f, 30f + 200f);
                //ball.GetComponent<Rigidbody>().AddForce((ball.transform.position.z - batter.transform.position.z) / (batter.transform.position.x - ball.transform.position.x) * (350f + 150f / 2) / 2, (350f + 100f) / 2, (350f + 150f / 2) / 2);
                unitychan_Left_arm.transform.Rotate(new Vector3(144.2f, 85.9f, 100.36f));
                unitychan_Left_ForeArm.transform.Rotate(new Vector3(-31.9f, 29.6f, -61f));
                unitychan_Right_arm.transform.Rotate(new Vector3(36.429f, 90.8f, 58.6f));
                unitychan_Right_ForeArm.transform.Rotate(new Vector3(-21.47f, 145.52f, 5.23f));
            }
        }

        if (flag == -3 || flag == -1)
        {
            ball.transform.Rotate(new Vector3(Mathf.Sqrt(ball.GetComponent<Rigidbody>().velocity.x * ball.GetComponent<Rigidbody>().velocity.x + ball.GetComponent<Rigidbody>().velocity.y * ball.GetComponent<Rigidbody>().velocity.y + ball.GetComponent<Rigidbody>().velocity.z * ball.GetComponent<Rigidbody>().velocity.z), 0));
        }

        if ((Grobe.transform.position.z - ball.transform.position.z) * (Grobe.transform.position.z - ball.transform.position.z) > 50)
        {
            batterCamera.transform.LookAt(ball.transform.position, Vector3.up);
        }

        if (flag == -3 && (Grobe.transform.position.z - ball.transform.position.z) * (Grobe.transform.position.z - ball.transform.position.z) > 10)
        {
            catcherCamera.transform.LookAt(ball.transform.position, Vector3.up);
        }

        if (strike_count >= 3)
        {
            StrikeLabelObject.SetActive(false);
            StrikeOutLabelObject.SetActive(true);
            count_reset();
            Out[out_count].SetActive(true);
            out_count++;
            if (out_count >= 3)
            {
                out_count_reset();
            }
        }
        else if (ball_count >= 4)
        {
            BallLabelObject.SetActive(false);
            WalkLabelObject.SetActive(true);
            count_reset();
            //if (run1 == false)
            //{
            //    run1 = true;
            //    RunnerLabelObject1.SetActive(true);
            //    Debug.Log("run0->1");
            //}
            //else if (run2 == false)
            //{
            //    run2 = true;
            //    RunnerLabelObject2.SetActive(true);
            //    Debug.Log("run1->2");
            //}
            //else if (run3 == false)
            //{
            //    run3 = true;
            //    RunnerLabelObject3.SetActive(true);
            //    Debug.Log("run2->3");
            //}
            //else
            //{
            //    Debug.Log("run3->home");
            //    if (inning_count % 2 == 1)
            //    {
            //        p_count++;
            //        P_Count.text = "" + p_count;
            //    }
            //    else
            //    {
            //        s_count++;
            //        S_Count.text = "" + s_count;
            //    }
            //}

        }
    }

    //position transformer
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

}
