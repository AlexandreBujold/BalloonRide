using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace BalloonGame.Balloon.Health
{
    public class BalloonHealth : MonoBehaviour
    {
        public BalloonController m_balloonController;
        public int health = 3;
        public int maxHealth = 3;

        public UnityEvent OnDamage;
        public UnityEvent OnKill;

        public TextMeshProUGUI textObj;

        private void Update()
        {
            textObj.text = health.ToString();
        }

        public void Heal(int amount)
        {
            health = Mathf.Clamp(health + amount, 0, maxHealth);
        }

        public void Damage(int amount)
        {
            health = Mathf.Clamp(health - amount, 0, maxHealth);
            if (OnDamage != null)
            {
                OnDamage.Invoke();
            }
            if (health <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            Debug.Log(this.transform.parent.gameObject);
            if (null != m_balloonController)
            {
                if (OnKill != null)
                {
                    OnKill.Invoke();
                }
                Destroy(m_balloonController.gameObject);
            }
        }
    } 
}
