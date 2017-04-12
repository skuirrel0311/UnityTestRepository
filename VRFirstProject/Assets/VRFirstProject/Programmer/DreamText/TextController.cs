using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    TextMesh textMesh;
    string endText;
    string currentText = "";
    bool isCalc = false;

    float currentProgress = 0.0f;

    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        endText = textMesh.text;
    }

    /// <summary>
    /// 表示する文字の量を変更します（０～１）
    /// </summary>
    /// <param name="isForce">強制的に変更するか</param>
    public void SetText(float progress,bool isForce = false)
    {
        //変更前の値の方が大きい場合はisForceがtrueじゃなければ変更しない
        if (currentProgress > progress && isForce == false) return;

        currentProgress = progress;
        currentText = endText.Substring(0, (int)(endText.Length * progress));
        isCalc = true;
    }
    
    void Update()
    {
        if (!isCalc) return;

        textMesh.text = currentText;

        isCalc = false;
    }
}
