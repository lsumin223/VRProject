using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SpinEnemy : MonoBehaviour
{
    [Header("���ڰ� �������� �����.")]
    public float rotationSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative();
    }

}
