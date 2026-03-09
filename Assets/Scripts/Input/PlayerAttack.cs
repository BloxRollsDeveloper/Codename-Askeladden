using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;

    public void UpdateAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            print("Playing the attack animation!");
        }
    }
}
