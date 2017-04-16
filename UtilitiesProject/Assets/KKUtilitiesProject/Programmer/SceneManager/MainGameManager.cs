using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : BaseManager<MainGameManager>
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ParticleManager.I.Play("ExplosionParticle", Vector3.left * 2.0f);
            AudioManager.I.PlayOneShot("Explosion", new Vector3(-2.0f, 0.0f, 0.0f));

            KKUtilities.Delay(0.5f, () =>
            {
                AudioManager.I.PlayOneShot("Explosion", new Vector3(2.0f, 0.0f, 0.0f));
                ParticleManager.I.Play("ExplosionParticle", Vector3.right * 2.0f);
            }, this);

            KKUtilities.Delay(1.0f, () =>
            {
                AudioManager.I.PlayOneShot("Explosion");
                ParticleManager.I.Play("ExplosionParticle", Vector3.zero);
            }, this);
        }
    }
}
