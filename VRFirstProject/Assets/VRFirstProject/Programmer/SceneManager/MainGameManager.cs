using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : BaseManager<MainGameManager>
{
    [SerializeField]
    StarController shootingStar = null;

    [SerializeField]
    EyesController eye = null;

    protected override void Start()
    {
        eye.distance = 10000.0f;
        StartCoroutine(SetShootingStar());
        base.Start();
    }

    protected override void Update()
    {
    }

    IEnumerator SetShootingStar()
    {
        Coroutine coroutine;
        while(true)
        {
            shootingStar.limitTime = Random.Range(4.0f, 7.0f);
            coroutine = StartCoroutine(shootingStar.Shot());

            yield return coroutine;
        }
    }
}
