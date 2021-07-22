using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditHistory
{
    List<LevelObject> history;
    int index;

    EditHistory() {
        index = -1;
        history = new List<LevelObject>();
    }

    public void AddLevel(LevelObject level) {
        while(history.Count > index + 1) {
            history.RemoveAt(history.Count - 1);
        }
        //history.AddLast(level);
        index++;
    }

    //public LevelObject Back() {
    //    if (index > 0) {
    //        index--;
    //        return history.;
    //    }
    //}
}
