using UnityEngine;

public interface ITakeableMoney
{
    public bool isActive();
    public void TakeMoney(Vector3 playerPos);
    
}

