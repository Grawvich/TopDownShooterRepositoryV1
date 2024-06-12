using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void Awake(PlayerController player);

    public abstract void Start(PlayerController player);

    public abstract void EnterState(PlayerController player);

    public abstract void Update(PlayerController player);

    public abstract void OnCollisionEnter(PlayerController player);
}
