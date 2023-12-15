using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRInformation : MonoBehaviour
{
    [Header("#VR 헤드셋의 위치값")]
    public float yaw;
    public float pitch;
    public float roll;

    [Header("#VR 컨트롤러의 방향값")]
    public Vector3 Lhandvector;
    public Vector3 LhandVelocity;

    [Header("#회전값")]
    public Quaternion headsetRotation;


    void Update()
    {
        // 헤드셋의 회전 값을 얻기
        headsetRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        //Quaternion headsetRotation = Meta.XR.Headset.GetRotation();

        // 얻은 회전 값을 원하는 대로 활용
        // 예: 각도 출력
        ExtractEulerAngles(headsetRotation, out yaw, out pitch, out roll);
        yaw = (yaw > 180) ? yaw - 360 : yaw;
        pitch = (pitch > 180) ? pitch - 360 : pitch;
        roll = (roll > 180) ? roll - 360 : roll;
        //Debug.Log("Yaw: " + yaw + ", Pitch: " + pitch + ", Roll: " + roll);


        // LHand의 현재 위치 얻기
        Vector3 handPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);

        // LHand의 현재 속도 얻기
        LhandVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LHand);

        // LHand의 이전 프레임 위치 얻기
        Vector3 lastHandPosition = handPosition;

        // LHand의 이동 방향 벡터 계산
        Lhandvector = (handPosition - lastHandPosition).normalized;

    }

    // 쿼터니언에서 오일러 각도 추출
    void ExtractEulerAngles(Quaternion rotation, out float yaw, out float pitch, out float roll)
    {
        // ZYX 회전 순서를 사용하여 오일러 각도 추출
        Vector3 eulerAngles = rotation.eulerAngles;
        yaw = eulerAngles.y;
        pitch = eulerAngles.x;
        roll = eulerAngles.z;
    }
}
