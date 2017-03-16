using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesController : MonoBehaviour
{
    Image[] pointers;
    Camera mainCamera;
    RectTransform canvasRect;

    float gaugeValue = 2.0f;
    float maxGaugeValue = 2.0f;

    Ray ray;
    Vector3 centerPosition = new Vector3(0.5f, 0.5f, 0.0f);
    RaycastHit hit;
    [SerializeField]
    LayerMask mask;

    GameObject currentSeeObj = null;
    GameObject oldSeeObj = null;

    bool isTransition = false;

    void Start()
    {
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        pointers = canvasRect.transform.FindChild("Pointers").GetComponentsInChildren<Image>();
        
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        currentSeeObj = GetSeeObject();

        PointerControl(currentSeeObj);

        if (gaugeValue == 0.0f)
        {
            if (isTransition) return;
            isTransition = true;
            LoadSceneManager.I.LoadScene("main", true, 1.0f);
        }
    }

    GameObject GetSeeObject()
    {
        GameObject obj;
        ray = mainCamera.ViewportPointToRay(centerPosition);

        if (Physics.SphereCast(ray, 0.2f, out hit, 100.0f, mask))
        {
            obj = hit.transform.gameObject;
        }
        else
        {
            obj = null;
        }

        return obj;
    }

    void PointerControl(GameObject currentSeeObj)
    {
        pointers[0].rectTransform.anchoredPosition = new Vector2(canvasRect.sizeDelta.x * -0.25f, 0.0f);
        pointers[1].rectTransform.anchoredPosition = new Vector2(canvasRect.sizeDelta.x * 0.25f, 0.0f);

        if (currentSeeObj == null)
            gaugeValue += Time.deltaTime * 2.0f;
        else if (!currentSeeObj.Equals(oldSeeObj))
            gaugeValue += Time.deltaTime * 50.0f;
        else
            gaugeValue -= Time.deltaTime;

        gaugeValue = Mathf.Clamp(gaugeValue, 0.0f, maxGaugeValue);

        oldSeeObj = currentSeeObj;
        float temp = gaugeValue / maxGaugeValue;
        pointers[0].fillAmount = temp;
        pointers[1].fillAmount = temp;
    }
}
