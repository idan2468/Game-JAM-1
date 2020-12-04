using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    public PlayerController playerController;

    public void GetHit(float power)
    {
        playerController.GetHit(power);
    }
}
