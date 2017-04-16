using UnityEngine;
using UnityEngine.UI;

public class BarGauge : BaseGauge
{
    Image pointImage;

    public override void Start()
    {
        pointImage = transform.GetChild(0).GetComponent<Image>();
        base.Start();
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            ChangeValue(1);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            ChangeValue(-1);
        }

        base.Update();
    }

    protected override void SetGaugeImage()
    {
        pointImage.fillAmount = (float)value / maxValue;
    }
}
