using UnityEngine;

public class BaseGauge : MonoBehaviour
{
    [SerializeField]
    protected int maxValue = 10;
    protected int value;

    bool isCalc = false;

    public virtual void Start()
    {
        value = maxValue;
    }

    public virtual void Update()
    {
        if (!isCalc) return;
        isCalc = false;

        SetGaugeImage();
    }

    public void ChangeValue(int point)
    {
        isCalc = true;

        value += point;
        value = Mathf.Clamp(value, 0, maxValue);
    }

    protected virtual void SetGaugeImage()
    {

    }
}
