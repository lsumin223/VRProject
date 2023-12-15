using UnityEngine;

public class BeamRotate : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        // ��ü�� �־��� �ӵ��� Y�� ������ ȸ��
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
