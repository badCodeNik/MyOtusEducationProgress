using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    public class TakeDamageAction : MonoBehaviour
    {
        [SerializeField] private LifeComponent _lifeComponent;

        public void TakeDamage(int damage)
        {
            _lifeComponent.TakeDamage(damage);
        }
    }

    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}