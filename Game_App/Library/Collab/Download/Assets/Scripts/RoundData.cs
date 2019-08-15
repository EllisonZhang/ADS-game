using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData 
{
   [SerializeField] private string name;
   [SerializeField] public int timeLimitInSeconds = 10;
   public int pointAddedForCorrectAnswer;
   [SerializeField] public QuestionData[] questions;

}
