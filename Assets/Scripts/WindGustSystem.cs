using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerWindController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float moveForce = 20f;

    [Header("Wind Gusts")]
    public float minGustForce = 5f;
    public float maxGustForce = 20f;
    public float gustDuration = 1f;

    public float minGustInterval = 3f;
    public float maxGustInterval = 0.5f;
    public float minHeight = 0f;
    public float maxHeight = 100f;

    [Header("Audio")]
    public AudioSource audioSource;   // assign in Inspector
    public AudioClip VentoLagoDimon;      // wind sound effect

    private bool isGusting = false;
    private float nextGustTime;

    private Vector3 inputMove;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.angularDamping = 5f;

        // Auto-create AudioSource if not assigned
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        ScheduleNextGust();
    }

    void Update()
    {
        inputMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if (Time.time >= nextGustTime && !isGusting)
        {
            StartCoroutine(WindGust());
            ScheduleNextGust();
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(inputMove * moveForce, ForceMode.Acceleration);

        Vector3 correction = Vector3.Cross(transform.up, Vector3.up);
        rb.AddTorque(correction * 10f, ForceMode.Acceleration);

        Vector3 v = rb.linearVelocity;
        v.y = Mathf.Lerp(v.y, 0f, Time.fixedDeltaTime * 3f);
        rb.linearVelocity = v;
    }

    IEnumerator WindGust()
    {
        isGusting = true;

        // 🎧 PLAY WIND SOUND ON GUST START
        if (VentoLagoDimon != null)
        {
            audioSource.clip = VentoLagoDimon;
            audioSource.loop = false;
            audioSource.PlayOneShot(VentoLagoDimon);
        }

        Vector3 dir = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;

        float force = Random.Range(minGustForce, maxGustForce);

        float timer = 0f;

        while (timer < gustDuration)
        {
            rb.AddForce(dir * force, ForceMode.Force);

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isGusting = false;
    }

    void ScheduleNextGust()
    {
        float heightT = Mathf.InverseLerp(minHeight, maxHeight, transform.position.y);
        float interval = Mathf.Lerp(minGustInterval, maxGustInterval, heightT);

        nextGustTime = Time.time + interval;
    }
}