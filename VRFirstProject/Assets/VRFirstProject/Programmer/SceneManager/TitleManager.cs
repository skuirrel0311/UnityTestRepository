using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : BaseManager<TitleManager>
{
    [SerializeField]
    Light pointLight = null;
    [SerializeField]
    ParticleSystem particle = null;
    //パーティクルのパラメータはモジュールの値をいじらないといけない
    ParticleSystem.MainModule module;

    [SerializeField]
    float intervalTime = 5.0f;

    enum ColorName { White ,Red, Orange, Yellow, Green, Blue, Indigo, Violet }
    List<Color> colorList = new List<Color>();
    int currentIndex = 0;

    protected override void Start()
    {
        base.Start();

        SetColorList();
        StartCoroutine(SetRandomColor());
    }

    void SetColorList()
    {
        colorList.Add(Color.white);
        colorList.Add(Color.red);
        colorList.Add(new Color(1.0f, 0.517f, 0.0f));
        colorList.Add(Color.yellow);
        colorList.Add(Color.green);
        colorList.Add(Color.blue);
        colorList.Add(new Color(0.282f, 0.0f, 1.0f));
        colorList.Add(new Color(0.698f, 0.0f, 1.0f));
    }

    IEnumerator SetRandomColor()
    {
        Color startColor = pointLight.color;
        Color endColor;
        Color currentColor;
        module = particle.main;

        while(true)
        {
            startColor = pointLight.color;
            endColor = GetRandomColor();

            yield return StartCoroutine(KKUtilities.FloatLerp(intervalTime, (t) =>
            {
                currentColor = Color.Lerp(startColor, endColor, t);

                pointLight.color = currentColor;
                module.startColor = currentColor;
            }));
        }
    }

    Color GetRandomColor()
    {
        int temp;
        while(true)
        {
            temp = Random.Range(0, colorList.Count);
            if (temp != currentIndex) break;
        }
        currentIndex = temp;
        return colorList[currentIndex];
    }
}
