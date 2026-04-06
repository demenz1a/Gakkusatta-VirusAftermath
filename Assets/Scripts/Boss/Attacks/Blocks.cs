using TMPro;
using UnityEngine;
using System.Collections;

public class Blocks : MonoBehaviour
{
    [SerializeField] private GameObject _boardPrefab;
    [SerializeField] private GameObject _answerBlockPrefab;

    private int _value;
    private int _result;
    private BlocksManager _bossAttack;
    public Collider2D newcollider;

    private void Start()
    {
        StartCoroutine(EnableColliderDelayed(0.7f));
    }

    public void Initialize(int val, int result, BlocksManager attack)
    {
        _value = val;
        _result = result;
        _bossAttack = attack;
        GetComponentInChildren<TextMeshProUGUI>().text = val.ToString();

        if (result < 0)
        {
            Destroy(gameObject, 4f);
        }
        else if (result == 0)
        {
            Destroy(gameObject, 5f);
        }
    }

    public void OnHit() 
    {
        _bossAttack.OnAnswerSelected(_value,_result);
    }

    public IEnumerator EnableColliderDelayed(float delay)
    {
        newcollider.enabled = false;
        yield return new WaitForSeconds(delay);
        newcollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shot"))
        {
            OnHit();
        }
    }
}
