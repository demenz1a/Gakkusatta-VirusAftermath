using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Answer
{
    public int value;  
    public int result;   
}

[Serializable]
public class MathAttackData
{
    public string expression;      
    public List<Answer> answers;     
}

[CreateAssetMenu(fileName = "MathAttackGroup", menuName = "BossAttack/Math Attack Group")]
public class MathAttackGroup : ScriptableObject
{
    public List<MathAttackData> examples; 
}

