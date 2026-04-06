using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using Unity.Cinemachine;

public class ScheduleDanceManager : MonoBehaviour
{  
    public bool IsAttackFinished { get; private set; }
    public Animator animator;    
    public CinemachineCamera bossCam;
    public CinemachineCamera playerCam;
    [SerializeField] private GameObject[] damageZonePrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float danceDelay = 1.5f;
    [SerializeField] private float delayBeforeTelegraphPhase = 1f;
    [SerializeField] private float telegraphDuration = 0.5f; 
    [SerializeField] private float damageDuration = 1f;    
    [SerializeField] private AudioClip _danceClip;
    private List<int> currentPattern;
    private AudioSource audioSource;

   private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 1.0f;
    }

    private void Update()
    {
       
    }

    public void StartDanceAttack()
    {
        IsAttackFinished = false;
        currentPattern = GenerateRandomPattern();
        StartCoroutine(ExecuteDanceAttack());
    }

    List<int> GenerateRandomPattern()
    {
        List<int> available = new List<int> { 0, 1, 2, 3 , 4};
        return available.OrderBy(x => Random.value).Take(3).ToList();
    }

    IEnumerator ExecuteDanceAttack()
    {
        bossCam.Priority = 20;
        playerCam.Priority = 10;

        yield return new WaitForSeconds(1.8f);

        foreach (int index in currentPattern)
        {
            animator.SetTrigger($"Dance{index + 1}");
            audioSource.PlayOneShot(_danceClip);
            yield return new WaitForSeconds(danceDelay);
        }

        animator.SetTrigger("BackToAFK");

        bossCam.Priority = 10;
        playerCam.Priority = 20;

        yield return new WaitForSeconds(delayBeforeTelegraphPhase);

        foreach (int index in currentPattern)
        {
            GameObject zone = Instantiate(damageZonePrefabs[index], spawnPoint.position, Quaternion.identity);

            //Vector3 scale = zone.transform.localScale;
            //scale.x *= Random.value > 0.5f ? -1 : 1;
            //scale.y *= Random.value > 0.5f ? -1 : 1;
            //zone.transform.localScale = scale;

            zone.GetComponent<AttackZone>().StartZone();

            yield return new WaitForSeconds(2.5f);
        }

        yield return new WaitForSeconds(1f);

        IsAttackFinished = true;

    }
}



