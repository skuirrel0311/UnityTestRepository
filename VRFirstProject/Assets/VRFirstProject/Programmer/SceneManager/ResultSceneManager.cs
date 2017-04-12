using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : BaseManager<ResultSceneManager>
{
    [SerializeField]
    TextMesh textMesh;
    ScoreManager.Rank clearRank;

    ParticleSystem.MainModule particleModule;
    int particleNum = 0;

    float time = 0.0f;
    float intervalTime = 5.0f;

    protected override void Start()
    {
        base.Start();
        clearRank = ScoreManager.ClearRank;
        textMesh.text = "Score : " + ScoreManager.score.ToString() + "\nRank : " + clearRank.ToString();

        particleModule = ParticleManager.I.GetParticle("ExplosionParticle").main;

        particleNum = GetParticleNum(clearRank);

        particleModule.startColor = GetRankColor(clearRank);

        PlayParticle();
    }

    protected override void Update()
    {
        base.Update();

        time += Time.deltaTime;

        if (time > intervalTime)
        {
            time = 0.0f;
            PlayParticle();
        }
    }

    void PlayParticle()
    {
        if (clearRank != ScoreManager.Rank.S)
        {
            KKUtilities.Delay(Random.Range(0.2f, 1.0f), () =>
            {
                Vector3 temp = GetParticlePosition();
                AudioManager.I.PlayOneShot("Explosion", temp);
                ParticleManager.I.Play("ExplosionParticle", temp);
            }, this);
        }
        else
        {
            for (int i = 0; i < particleNum; i++)
            {
                KKUtilities.Delay(Random.Range(0.2f, 1.0f), () =>
                 {
                     Vector3 temp = GetParticlePosition();
                     particleModule.startColor = KKUtilities.GetRandomColor();
                     AudioManager.I.PlayOneShot("Explosion", temp);
                     ParticleManager.I.Play("ExplosionParticle", temp);
                 },this);
            }
        }
    }

    Vector3 GetParticlePosition()
    {
        Vector3 temp;
        temp.x = Random.Range(-8.0f, 8.0f);
        temp.y = Random.Range(2.0f, 10.0f);
        temp.z = Random.Range(15.0f, 25.0f);
        return temp;
    }

    Color GetRankColor(ScoreManager.Rank rank)
    {
        if (rank == ScoreManager.Rank.A) return Color.yellow;
        if (rank == ScoreManager.Rank.B) return Color.blue;
        return Color.white;
    }

    int GetParticleNum(ScoreManager.Rank rank)
    {
        if (rank == ScoreManager.Rank.S) return 5;
        if (rank == ScoreManager.Rank.A) return 3;
        if (rank == ScoreManager.Rank.B) return 2;
        return 1;
    }
}
