using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesController : MonoBehaviour
{
    Camera mainCamera;
    Material pointerMat;

    //public float gaugeValue { get; private set; }
    public float gaugeValue;
    float maxGaugeValue = 2.0f;

    Ray ray;
    Vector3 centerPosition = new Vector3(0.5f, 0.5f, 0.0f);
    RaycastHit hit;

    [System.NonSerialized]
    public float radius = 0.2f;
    [System.NonSerialized]
    public float distance = 100.0f;
    [SerializeField]
    LayerMask mask = 0;

    public GameObject currentSeeObj { get; private set; }
    public GameObject oldSeeObj { get; private set; }

    float startInnerValue = 0.0f, startOuterValue = 0.08f;
    float endInnerValue = 0.3f, endOuterValue = 0.35f;

    void Start()
    {
        mainCamera = Camera.main;
        pointerMat = GetComponent<Renderer>().material;
        CreateReticleVertices();

        pointerMat.SetFloat("_InnerDiameter", 0.0f);
        pointerMat.SetFloat("_OuterDiameter", 0.08f);
        pointerMat.SetFloat("_DistanceInMeters", 10.0f);

        gaugeValue = maxGaugeValue;
    }

    void CreateReticleVertices()
    {
        Mesh mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = mesh;

        int segments_count = 20;
        int vertex_count = (segments_count + 1) * 2;

        #region Vertices

        Vector3[] vertices = new Vector3[vertex_count];

        const float kTwoPi = Mathf.PI * 2.0f;
        int vi = 0;
        for (int si = 0; si <= segments_count; ++si)
        {
            // Add two vertices for every circle segment: one at the beginning of the
            // prism, and one at the end of the prism.
            float angle = (float)si / (float)(segments_count) * kTwoPi;

            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);

            vertices[vi++] = new Vector3(x, y, 0.0f); // Outer vertex.
            vertices[vi++] = new Vector3(x, y, 1.0f); // Inner vertex.
        }
        #endregion

        #region Triangles
        int indices_count = (segments_count + 1) * 3 * 2;
        int[] indices = new int[indices_count];

        int vert = 0;
        int idx = 0;
        for (int si = 0; si < segments_count; ++si)
        {
            indices[idx++] = vert + 1;
            indices[idx++] = vert;
            indices[idx++] = vert + 2;

            indices[idx++] = vert + 1;
            indices[idx++] = vert + 2;
            indices[idx++] = vert + 3;

            vert += 2;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateBounds();
        ;
    }

    void Update()
    {
        currentSeeObj = GetSeeObject();

        PointerControl(currentSeeObj);
    }
    
    GameObject GetSeeObject()
    {
        GameObject obj;
        ray = mainCamera.ViewportPointToRay(centerPosition);

        if (Physics.SphereCast(ray, radius, out hit, distance, mask))
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
        if (currentSeeObj == null)
            gaugeValue += Time.deltaTime * 2.0f;
        else if (!currentSeeObj.Equals(oldSeeObj))
            gaugeValue += Time.deltaTime * 50.0f;
        else
            gaugeValue -= Time.deltaTime;

        gaugeValue = Mathf.Clamp(gaugeValue, 0.0f, maxGaugeValue);

        oldSeeObj = currentSeeObj;
        float temp = 1.0f - (gaugeValue / maxGaugeValue);

        SetPointerValue(temp);
    }

    void SetPointerValue(float value)
    {
        pointerMat.SetFloat("_InnerDiameter", Mathf.Lerp(startInnerValue, endInnerValue, value));
        pointerMat.SetFloat("_OuterDiameter", Mathf.Lerp(startOuterValue, endOuterValue, value));
    }
}
