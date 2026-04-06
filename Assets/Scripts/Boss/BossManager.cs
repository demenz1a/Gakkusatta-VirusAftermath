using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour
{
    public BlocksManager blocksAttack;
    public ScheduleDanceManager scheduleDanceAttack;
    public FigureManager figuresAttack;
    public SegmentsManager segmentsAttack;
    public XMarkManager xmarkAttack;

    public Figures figures;
    public AttackZone schedule;
    public Xmark xmark;
    public Segments segments;


    public int damageIncrementPerCycle = 5;
    private int currentCycle = 0;
    private bool firstAttackWasSegments = false;

    private bool wasBlocksExplained = false;

    private void Start()
    {
        StartCoroutine(AttackCycleLoop());
    }

    private IEnumerator AttackCycleLoop()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            currentCycle++;

            Debug.Log("=== Начинается новый цикл атак: " + currentCycle + " ===");

            IncreaseAttackDamage();

            int firstAttack = Random.Range(2, 5);

            int secondAttack;
            do
            {
                secondAttack = Random.Range(2, 5);
            } while (secondAttack == firstAttack);

            yield return StartCoroutine(StartAttack(firstAttack));

            yield return StartCoroutine(StartAttack(secondAttack));

            yield return StartCoroutine(StartAttack(1));

            yield return new WaitForSeconds(2f);
        }
    }



    private IEnumerator StartAttack(int attackNumber)
    {
        Debug.Log("Атака #" + attackNumber);

        //if (currentCycle >= 3 && attackNumber != 1)
        //{
          //  xmarkAttack.StartXMark();
        //}

        switch (attackNumber)
        {
            case 1:
                if (!wasBlocksExplained)
                {
                    wasBlocksExplained = true;
                    yield return StartCoroutine(ShowBlockExplanationDialogue());
                }

                blocksAttack.StartBlocks();
                yield return new WaitUntil(() => blocksAttack.IsAttackFinished);
                break;
            case 2:
                scheduleDanceAttack.StartDanceAttack();
                yield return new WaitUntil(() => scheduleDanceAttack.IsAttackFinished);
                break;
            case 3:
                figuresAttack.StartFigures();
                yield return new WaitUntil(() => figuresAttack.IsAttackFinished);
                break;
            case 4:
                segmentsAttack.StartSegments();
                yield return new WaitUntil(() => segmentsAttack.IsAttackFinished);
                break;
        }
    }


    private void IncreaseAttackDamage()
    {
        int bonusDamage = (currentCycle - 1) * damageIncrementPerCycle;

        schedule._damageAmount += bonusDamage;
        figures._damageAmount += bonusDamage;
        segments._damageAmount += bonusDamage;
        //xmark._damageAmount += bonusDamage;
    }

    public DialogueManager blockDialogue;

    private IEnumerator ShowBlockExplanationDialogue()
    {
        blockDialogue.gameObject.SetActive(true);
        blockDialogue.StartDialogueFromIndex(15);

        yield return new WaitUntil(() => !blockDialogue.gameObject.activeSelf);
    }
}

