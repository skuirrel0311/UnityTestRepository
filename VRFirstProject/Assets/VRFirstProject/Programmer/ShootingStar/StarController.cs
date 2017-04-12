using System.Collections;
using UnityEngine;

public class StarController : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;
    public float speed = 100.0f;
    public float limitTime = 5.0f;

    [SerializeField]
    float distance = 100.0f;
    [SerializeField]
    float maxLongitude = 360.0f;
    [SerializeField]
    float minLongitude = 0.0f;
    [SerializeField]
    float maxLatitude = 20.0f;
    [SerializeField]
    float minLatitude = 80.0f;
    
    GameObject particle;

    void Awake()
    {
        particle = transform.GetChild(0).gameObject;
    }

    //流れ星
    public IEnumerator Shot()
    {
        particle.SetActive(false);
        yield return null;
        StartUp();

        float t = 0.0f;
        while (true)
        {
            t += Time.deltaTime;
            transform.Translate(velocity * Time.deltaTime);

            if (t > limitTime) break;
            yield return null;
        }
    }

    //流れ星を初期化する
    void StartUp()
    {
        particle.SetActive(true);
        float longitude = Random.Range(minLongitude, maxLongitude);
        float latitude = Random.Range(minLatitude, maxLatitude);

        Vector3 startPosition = KKUtilities.SphereCoordinate(longitude, latitude, distance);
        Vector3 temp = KKUtilities.SphereCoordinate(longitude + 180.0f, latitude, distance);
        transform.position = startPosition;

        velocity = Vector3.Cross(startPosition, temp).normalized * speed;

        AudioSource shotAudio = AudioManager.I.PlayOneShot("ShootingStar", startPosition);
        shotAudio.maxDistance = 1000;
    }
}
