using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{ 

    public string answer;
    public int yearID;
    public int dateID;
    public int questionID;
    public string userID;

    public Answer()
    {
    }

    public Answer(string answer, int yearID, int dateID, int questionID, string userID)
    {
        this.answer     = answer;
        this.yearID     = yearID;
        this.dateID     = dateID;
        this.questionID = questionID;
        this.userID     = userID;
    }
}