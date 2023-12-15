using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRInformation : MonoBehaviour
{
    [Header("#VR ������ ��ġ��")]
    public float yaw;
    public float pitch;
    public float roll;

    [Header("#VR ��Ʈ�ѷ��� ���Ⱚ")]
    public Vector3 Lhandvector;
    public Vector3 LhandVelocity;

    [Header("#ȸ����")]
    public Quaternion headsetRotation;


    void Update()
    {
        // ������ ȸ�� ���� ���
        headsetRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        //Quaternion headsetRotation = Meta.XR.Headset.GetRotation();

        // ���� ȸ�� ���� ���ϴ� ��� Ȱ��
        // ��: ���� ���
        ExtractEulerAngles(headsetRotation, out yaw, out pitch, out roll);
        yaw = (yaw > 180) ? yaw - 360 : yaw;
        pitch = (pitch > 180) ? pitch - 360 : pitch;
        roll = (roll > 180) ? roll - 360 : roll;
        //Debug.Log("Yaw: " + yaw + ", Pitch: " + pitch + ", Roll: " + roll);


        // LHand�� ���� ��ġ ���
        Vector3 handPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);

        // LHand�� ���� �ӵ� ���
        LhandVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LHand);

        // LHand�� ���� ������ ��ġ ���
        Vector3 lastHandPosition = handPosition;

        // LHand�� �̵� ���� ���� ���
        Lhandvector = (handPosition - lastHandPosition).normalized;

    }

    // ���ʹϾ𿡼� ���Ϸ� ���� ����
    void ExtractEulerAngles(Quaternion rotation, out float yaw, out float pitch, out float roll)
    {
        // ZYX ȸ�� ������ ����Ͽ� ���Ϸ� ���� ����
        Vector3 eulerAngles = rotation.eulerAngles;
        yaw = eulerAngles.y;
        pitch = eulerAngles.x;
        roll = eulerAngles.z;
    }
}
