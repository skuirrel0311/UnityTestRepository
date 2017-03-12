using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesController : MonoBehaviour
{
    Image pointer;
    Camera mainCamera;

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
        pointer = GameObject.Find("Canvas").transform.FindChild("Pointer").GetComponent<Image>();
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        ray = mainCamera.ViewportPointToRay(centerPosition);

        if (Physics.SphereCast(ray, 0.2f, out hit, 100.0f, mask))
        {
            currentSeeObj = hit.transform.gameObject;
        }
        else
        {
            currentSeeObj = null;
        }

        PointerControl(currentSeeObj);

        if (gaugeValue == 0.0f)
        {
            if (isTransition) return;
            isTransition = true;
            LoadSceneManager.I.LoadScene("main", true, 1.0f);
        }
    }

    void PointerControl(GameObject currentSeeObj)
    {
        if (currentSeeObj == null)
            gaugeValue += Time.deltaTime * 2.0f;
        else if (!currentSeeObj.Equals(oldSeeObj))
            gaugeValue += Time.deltaTime * 50.0f;
        else
            gaugeValue -= Time.deltaTime;

        gaugeValue = Mathf.Clamp(gaugeValue, 0.0f, maxGaugeValue);

        oldSeeObj = currentSeeObj;
        pointer.fillAmount = gaugeValue / maxGaugeValue;
    }
}
