using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    List<CharacterStateBehavior> stateList = new List<CharacterStateBehavior>();
    CharacterStateBehavior currentState;

    public void Start()
    {
        
    }

    public void Update()
    {
        if (currentState == null) return;
        
        currentState.Update();
    }

    public void ChangeState(int index, float waitTime = 0.0f)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        if(waitTime == 0.0f)
        {
            currentState = FindState(index);
            return;
        }

        StartCoroutine(KKUtilities.Delay(waitTime, () =>
        {
            currentState = FindState(index);
        }));
    }

    public void AddState(CharacterStateBehavior state)
    {
        stateList.Add(state);
    }

    public CharacterStateBehavior FindState(int index)
    {
        for(int i = 0; i < stateList.Count;i++)
        {
            if(stateList[i].index == index) return stateList[i];
        }

        return null;
    }
}
