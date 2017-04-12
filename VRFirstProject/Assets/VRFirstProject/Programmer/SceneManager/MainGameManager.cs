using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : BaseManager<MainGameManager>
{
    [SerializeField]
    StarController shootingStar = null;

    [SerializeField]
    EyesController eye = null;
    float oldProgress = 0.0f;

    [SerializeField]
    TextController dreamText = null;

    GameObject cameraObj;

    protected override void Start()
    {
        eye.distance = 10000.0f;
        cameraObj = Camera.main.gameObject;
        dreamText.SetText(0.0f, true);
        GameStart();
        base.Start();
    }

    protected override void Update()
    {
        if (eye.progress > 0.0f)
        {
            dreamText.SetText(eye.progress);
        }

        //このフレームで進捗が０になった
        if (eye.progress == 0.0f && oldProgress > 0.0f)
        {
            dreamText.SetText(0.0f, true);
        }

        if (eye.progress == 1.0f)
        {
            ScoreManager.AddScore(1);
            eye.ResetValue();
            dreamText.SetText(0.0f, true);
            ParticleManager.I.Play("ExplosionParticle", dreamText.transform.position);
        }

        oldProgress = eye.progress;
    }

    IEnumerator SetShootingStar()
    {
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        Coroutine coroutine;
        while (true)
        {
            shootingStar.limitTime = Random.Range(5.0f, 10.0f);
            coroutine = StartCoroutine(shootingStar.Shot());

            yield return coroutine;
            dreamText.SetText(0.0f, true);
        }
    }

    public void GameStart()
    {
        StartCoroutine(SetShootingStar());
        KKUtilities.Delay(100.0f, () =>
        {
            GameEnd(true);
        },this);
    }

    public void GameEnd(bool gameOver = false)
    {
        if(gameOver)
        {
            LoadSceneManager.I.LoadScene("result", true, 1.0f);
        }
    }
}
