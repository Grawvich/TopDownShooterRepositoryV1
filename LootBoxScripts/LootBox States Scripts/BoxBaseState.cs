using UnityEngine;
public abstract class BoxBaseState
{
    public abstract void Awake(LootboxController box);

    public abstract void Start(LootboxController box);

    public abstract void EnterState(LootboxController box);

    public abstract void Update(LootboxController box);

    public abstract void OnTriggerEnter(Collider col);
}
