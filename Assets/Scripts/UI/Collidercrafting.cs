using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collidercrafting : MonoBehaviour
{
    public enum UIType
    {
        canvus, button, slider
    }

    public UIType uiType = UIType.button;

    private float zfloat;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        switch(uiType)
        {
            case UIType.canvus:
                zfloat = 1;
                break;
            case UIType.button:
                zfloat = 5;
                break;
            case UIType.slider:
                zfloat = 5; 
                AddSliderCollider();
                break;

        }

        if (uiType != UIType.slider)
        {
            // UI 개체의 픽셀 크기를 가져옵니다.
            Vector3 pixelSize = new Vector3(rectTransform.rect.width, rectTransform.rect.height, zfloat);

            // UI 개체에 BoxCollider2D를 추가합니다.
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

            // Collider의 크기를 픽셀 크기로 설정합니다.
            boxCollider.size = pixelSize;
        }
    }
    void AddSliderCollider()
    {
        Slider slider = GetComponent<Slider>();

        if (slider != null)
        {
            RectTransform handleRect = slider.fillRect.GetComponent<RectTransform>();

            if (handleRect != null)
            {
                Vector3 handlePixelSize = new Vector3(handleRect.rect.width, handleRect.rect.height, zfloat);
                BoxCollider boxCollider = handleRect.gameObject.AddComponent<BoxCollider>();
                boxCollider.size = handlePixelSize;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
