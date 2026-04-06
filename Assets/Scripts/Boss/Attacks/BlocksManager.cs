using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using System.Linq;
using System.Collections;

public class BlocksManager : MonoBehaviour
{
    [System.Serializable]
    public class Answer
    {
        public int value;
        public int result;
    }

    [System.Serializable]
    public class MathAttackData
    {
        public string expression;
        public List<Answer> answers;
    }
    public Transform spawnPoint;
    public CinemachineCamera cinemachineCamera;
    public Transform bossFocusPoint;
    public Transform playerTransform;    
    public bool IsAttackFinished { get; private set; }
    [SerializeField] private GameObject boardPrefab;
    [SerializeField] private GameObject answerBlockPrefab;
    [SerializeField] private AudioClip wrongA;
    [SerializeField] private AudioClip correctA;
    [SerializeField] private AudioClip okayA;
    [SerializeField] private AudioClip intro;
    private AudioSource _audioSource;
    private GameObject _currentBoard;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private List<MathAttackData> _examples = new List<MathAttackData>();
    private bool _isHitted = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartBlocks();
        }
    }
    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.volume = 1.0f;

        _examples = new List<MathAttackData>
        {
            new MathAttackData
            {
                expression = "X-7",
                answers = new List<Answer>
                {
                    new Answer { value = 7, result = 0 },    
                    new Answer { value = 20, result = 13 },    
                    new Answer { value = -9, result = -16 },   
                }
            },
            new MathAttackData
            {
                expression = "X+16",
                answers = new List<Answer>
                {
                    new Answer { value = 6, result = 22 },    
                    new Answer { value = -16, result = 0 },    
                    new Answer { value = -36, result = -20 },   
                }
            },
            new MathAttackData
            {
                expression = "X+5",
                answers = new List<Answer>
                {
                    new Answer { value = 8, result = 13 },    
                    new Answer { value = -5, result = 0 },    
                    new Answer { value = -20, result = -15 },   
                }
            },
            new MathAttackData
            {
                expression = "X*6",
                answers = new List<Answer>
                {
                    new Answer { value = -3, result = -16 },   
                    new Answer { value = 3, result = 18 },    
                    new Answer { value = 0, result = 0 },   
                }
            },
            new MathAttackData
            {
                expression = "X/2",
                answers = new List<Answer>
                {
                    new Answer { value = 0, result = 0 },    
                    new Answer { value = 32, result = 16 },    
                    new Answer { value = -30, result = -15 },  
                }
            },
            new MathAttackData
            {
                expression = "X-23",
                answers = new List<Answer>
                {
                    new Answer { value = 5, result = -18 },  
                    new Answer { value = 23, result = 0 },    
                    new Answer { value = 33, result = 10 },  
                }
            },
            new MathAttackData
            {
                expression = "X/3",
                answers = new List<Answer>
                {
                    new Answer { value = 33, result = 11 },   
                    new Answer { value = 0, result = 0 }, 
                    new Answer { value = -36, result = -12},   
                }
            },
            new MathAttackData
            {
                expression = "X*4",
                answers = new List<Answer>
                {
                    new Answer { value = 3, result = 12 },    
                    new Answer { value = 0, result = 0 },    
                    new Answer { value = -4, result = -16 },   
                }
            },
            new MathAttackData
            {
                expression = "X-21",
                answers = new List<Answer>
                {
                    new Answer { value = 32, result = 11 },
                    new Answer { value = 21, result = 0 },   
                    new Answer { value = 1, result = -21},  
                }
            },
            new MathAttackData
            {
                expression = "X*2",
                answers = new List<Answer>
                {
                    new Answer { value = 11, result = 22 },   
                    new Answer { value = 0, result = 0 },   
                    new Answer { value = -9, result = -18 },   
                }
            },
        };
    }

    public void StartBlocks()
    {
        if (_examples.Count == 0) return;

        _audioSource.PlayOneShot(intro);
        _isHitted = false;
        IsAttackFinished = false;

        cinemachineCamera.Follow = bossFocusPoint;
        cinemachineCamera.LookAt = bossFocusPoint;

        int index = Random.Range(0, _examples.Count);
        MathAttackData example = _examples[index];

        _currentBoard = Instantiate(boardPrefab, spawnPoint.position, Quaternion.identity);
        var boardComponent = _currentBoard.GetComponent<BlocksBoard>();
        if (boardComponent != null)
        {
            boardComponent.SetExpression(example.expression);
        }
        _spawnedObjects.Add(_currentBoard);

        float verticalOffset = -4f;
        float horizontalSpacing = 15f;
        List<Answer> shuffledAnswers = example.answers.OrderBy(a => Random.value).ToList();
        int answerCount = shuffledAnswers.Count;
        float totalWidth = (answerCount - 1) * horizontalSpacing;

        Vector3 boardCenter = _currentBoard.transform.position;

        for (int i = 0; i < answerCount; i++)
        {
            Answer answer = shuffledAnswers[i];
            float xOffset = -totalWidth / 3.2f + i * horizontalSpacing;
            Vector3 position = boardCenter + new Vector3(xOffset, verticalOffset, 0);

            GameObject block = Instantiate(answerBlockPrefab, position, Quaternion.identity);
            block.GetComponent<Blocks>().Initialize(answer.value, answer.result, this);
            _spawnedObjects.Add(block);
        }
    }
public int EvaluateExpression(string expression, int x)
    {
        expression = expression.Replace(" ", "");

        try
        {
            if (expression.StartsWith("X+"))
                return x + int.Parse(expression.Substring(2));
            else if (expression.StartsWith("X-"))
                return x - int.Parse(expression.Substring(2));
            else if (expression.StartsWith("X*"))
                return x * int.Parse(expression.Substring(2));
            else if (expression.StartsWith("X/"))
                return x / int.Parse(expression.Substring(2));
            else if (expression == "X")
                return x;
        }
        catch
        {
            Debug.LogWarning("Ошибка при разборе выражения: " + expression);
        }

        Debug.LogWarning("Неподдерживаемое выражение: " + expression);
        return x;
    }

    public void OnAnswerSelected(int value, int result)
    {
        if (_isHitted) return; 
        _isHitted = true;

        Debug.Log("/");

        if (result == 0)
        {
            Debug.Log("Атака отменена.");
            _audioSource.PlayOneShot(okayA);
        }
        else if (result > 0)
        {
            Debug.Log("Игрок получает урон!");
            Reiko.Instance.TakeDamage(result);
            _audioSource.PlayOneShot(wrongA);
        }
        else if (result < 0)
        {
            Debug.Log("Босс получает урон!");
            BossEntity.Instance.TakeDamageBossMinus(result);
            _audioSource.PlayOneShot(correctA);
        }

        Debug.Log(result);
        Debug.Log("/");

        ClearAllAttackObjects();

        StartCoroutine(AttackFinish());
    }

    private IEnumerator AttackFinish()
    {
        yield return new WaitForSeconds(2f);
        IsAttackFinished = true;
    }

    public void ClearAllAttackObjects()
    {
        foreach (GameObject obj in _spawnedObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        _spawnedObjects.Clear();
        _currentBoard = null;

        cinemachineCamera.Follow = playerTransform;
        cinemachineCamera.LookAt = playerTransform;
    }

    private bool OnHit()
    {
        return _isHitted;
    }
}

