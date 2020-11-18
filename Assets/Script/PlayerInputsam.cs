using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightBuzz.Kinect4Azure;

public class PlayerInputsam : MonoBehaviour
{
    PlayerParamClass
    paramClass = PlayerParamClass.GetInstance();

    [SerializeField]
    private Transform
        bodyT;
    private Dictionary<JointType, JointType>
        parentJointMap = new Dictionary<JointType, JointType>() {
        { JointType.Pelvis, JointType.Head },
        { JointType.SpineNaval, JointType.Pelvis },
        { JointType.SpineChest, JointType.SpineNaval },
        { JointType.Neck, JointType.Head },
        { JointType.ClavicleLeft, JointType.SpineChest },
        { JointType.ShoulderLeft, JointType.ClavicleLeft },
        { JointType.ElbowLeft, JointType.ShoulderLeft },
        { JointType.WristLeft, JointType.ElbowLeft },
        { JointType.HandLeft, JointType.WristLeft },
        { JointType.HandtipLeft, JointType.HandLeft },
        { JointType.ThumbLeft, JointType.Head },
        { JointType.ClavicleRight, JointType.ClavicleRight },
        { JointType.ShoulderRight, JointType.ClavicleRight },
        { JointType.ElbowRight, JointType.ShoulderRight },
        { JointType.WristRight, JointType.ElbowRight },
        { JointType.HandRight, JointType.WristRight },
        { JointType.HandtipRight, JointType.HandRight },
        { JointType.ThumbRight, JointType.Head },
        { JointType.HipLeft, JointType.Pelvis },
        { JointType.KneeLeft, JointType.HipLeft },
        { JointType.AnkleLeft, JointType.KneeLeft },
        { JointType.FootLeft, JointType.AnkleLeft },
        { JointType.HipRight, JointType.Pelvis },
        { JointType.KneeRight, JointType.HipRight },
        { JointType.AnkleRight, JointType.KneeRight },
        { JointType.FootRight, JointType.AnkleRight },
        { JointType.Head, JointType.Head },
        { JointType.Nose, JointType.Head },
        { JointType.EyeLeft, JointType.Head },
        { JointType.EarLeft, JointType.Head },
        { JointType.EyeRight, JointType.Head },
        { JointType.EarRight, JointType.Head },
    };

    private KinectSensor sensor;
    private void Start()
    {
        sensor = KinectSensor.GetDefault();
        sensor?.Open();
    }

    private void FixedUpdate()
    {
        if (sensor == null || !sensor.IsOpen) return;

        Frame frame = sensor.Update();

        if (frame != null)
        {
            if (frame.BodyFrameSource != null)
            {
                UpdateAvatars(frame.BodyFrameSource.Bodies);
            }
        }
    }

    private void OnDestroy()
    {
        sensor?.Close();
    }

    /// <summary>
    /// Updates terrain position and rotation.
    /// Updates all joints positions and bones.
    /// </summary>
    /// <param name="bodies"></param>
    /// <param name="floor"></param>
    private void UpdateAvatars(List<Body> bodies)
    {
        if (bodies == null || bodies.Count == 0) return;

        // Show joints
        Body body = bodies.Closest();

        for (int i = 0; i <= (int)JointType.EyeRight; i++)
        {
            //関節の入力
            Vector3 jointPos = body.Joints[(JointType)i].Position;
            jointPos *= -1f;
            Quaternion jointRotation = body.Joints[(JointType)i].Orientation;

            //関節の出力
            //へその位置くらいでポジションを取る
            if ((JointType)i == JointType.SpineNaval)
                paramClass.SetPos(jointPos);

            Transform jointT = bodyT.GetChild(i);
            jointT.localPosition = jointPos;
            jointT.localRotation = jointRotation; Transform jointBoneT = jointT.GetChild(0);
            if (parentJointMap[(JointType)i] != JointType.Head)
            {
                // set bone
                Vector3 jointParentPos = body.Joints[parentJointMap[(JointType)i]].Position;
                jointParentPos *= -1f;
                Vector3 boneDirection = jointPos - jointParentPos;
                Vector3 boneDirectionLocalSpace = Quaternion.Inverse(jointT.rotation) * Vector3.Normalize(boneDirection);

                jointBoneT.position = jointPos - 0.5f * boneDirection;
                jointBoneT.localRotation = Quaternion.FromToRotation(Vector3.up, boneDirectionLocalSpace);
                jointBoneT.localScale = new Vector3(1f, 20f * 0.5f * boneDirection.magnitude, 1f);
            }
            else
            {
                jointBoneT.gameObject.SetActive(false);
            }
        }
    }
}
