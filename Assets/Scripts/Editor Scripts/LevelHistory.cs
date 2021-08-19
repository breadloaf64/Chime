using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHistory
{
    List<LevelObject> history;
    int index;

    public LevelHistory() {
        index = -1;
        history = new List<LevelObject>();
    }

    public void Add(LevelObject level) {
        debug();
        while(history.Count > index + 1) {
            history.RemoveAt(history.Count - 1);
        }
        
        history.Add(level.DeepCopy());
        index = history.Count - 1;
        debug();
    } 

    public LevelObject Back() {
        index--;
        if (index < 0) index = 0;
        debug();
        return history[index].DeepCopy();
    }

    public LevelObject Forward() {
        index++;
        if (index >= history.Count) index = history.Count - 1;
        debug();
        return history[index].DeepCopy();
    }

    public int GetIndex() {
        return index;
    }

    public int GetSize() {
        return history.Count;
    }

    void debug() {
        //Debug.Log((index + 1) + " | " + history.Count);
    }
}
