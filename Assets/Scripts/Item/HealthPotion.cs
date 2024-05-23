using Unity.VisualScripting;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int healAamount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어와 충돌했을 때
        {          
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem.CurrentHealth < healthSystem.MaxHealth)
            {
                healthSystem.ChangeHealth(healAamount);
                SoundManager.Sound.Play("DrinkPotion.mp3", SoundType.Effect);
                gameObject.SetActive(false);
            }
        }
    }
}
