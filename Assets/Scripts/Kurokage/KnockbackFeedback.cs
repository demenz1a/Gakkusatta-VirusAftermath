using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] float strength = 16, delay = 0.15f;
    public UnityEvent OnBegin, OnDone;

    //public static KnockbackFeedback instance;

    private void Awake()
    {
        //if (instance == null)
        //{
          //  instance = this;
        //}
    }
    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();

        if (rigidbody == null) return;

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null) agent.enabled = false;

        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rigidbody.AddForce(direction * strength, ForceMode2D.Impulse);

        StartCoroutine(Reset(agent));
    }

    private IEnumerator Reset(UnityEngine.AI.NavMeshAgent agent)
    {
        yield return new WaitForSeconds(delay);
        rigidbody.linearVelocity = Vector2.zero;

        if (agent != null) agent.enabled = true;

        OnDone?.Invoke();
    }
}
