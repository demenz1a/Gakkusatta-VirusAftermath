using UnityEngine;

public class Xmark : MonoBehaviour
{
    public static Xmark Instance { get; private set; }
    public int _damageAmount = 20;
    [SerializeField] private float _delay = 30f;
    private float _timer;
    private bool _isActive = false;
    private bool _isPulse = false;
    [SerializeField] private AudioClip _pulseClip;
    [SerializeField] private AudioClip _explousionClip;
    private AudioSource audioSource;
    private bool pulseSoundStarted = false;
    private bool endSoundPlayed = false;
    private float pulseBaseVolume = 1f;
    private float pulseSpeed = 2f;

    void Start()
    {
        _timer = _delay;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.volume = pulseBaseVolume;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 2.5f && !pulseSoundStarted)
        {
            PlayPulseLoop();
            ActivatePulse();
            pulseSoundStarted = true;
        }

        if (pulseSoundStarted && !_isActive)
        {
            float volume = Mathf.PingPong(Time.time * pulseSpeed, 0.5f) + 0.3f; 
            audioSource.volume = volume * pulseBaseVolume;
        }

        if (Reiko._instance.IsDogde() && _timer <= 2f)
        {
            gameObject.SetActive(false);
        }

        if (!_isActive && _timer <= 0.9f)
        {
            if (!endSoundPlayed)
            {
                StopPulseLoop();
                PlayEndSound();
                endSoundPlayed = true;
            }

            ActivateMark();
        }

        if (_timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    public bool IsPulse()
    {
        return _isPulse;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    private void ActivatePulse()
    {
        _isPulse = true;
    }

    private void ActivateMark()
    {
        _isActive = true;
        Reiko.Instance.TakeDamage(_damageAmount);
    }

    private void PlayPulseLoop()
    {
        audioSource.clip = _pulseClip;
        audioSource.Play();
    }

    private void StopPulseLoop()
    {
        audioSource.Stop();
    }

    private void PlayEndSound()
    {
        float originalVolume = audioSource.volume;
        audioSource.volume = 3.0f;
        audioSource.PlayOneShot(_explousionClip);
        audioSource.volume = originalVolume;
    }




    private void OnEnable()
    {
        _timer = _delay;
        _isActive = false;
        _isPulse = false;
        pulseSoundStarted = false;
        endSoundPlayed = false;

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.volume = pulseBaseVolume;
        }
    }
}

