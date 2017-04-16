using UnityEngine;

public class CharacterStateBehavior
{
    protected GameObject owner;

    //検索用
    public int index { get; private set; }

    public CharacterStateBehavior(GameObject owner, int index)
    {
        this.owner = owner;
        this.index = index;
    }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void Exit() { }
}
