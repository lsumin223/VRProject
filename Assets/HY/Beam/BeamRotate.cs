using UnityEngine;

public class BeamRotate : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        // 물체를 주어진 속도로 Y축 주위로 회전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
