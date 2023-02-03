using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float MaxLifeTime =  10;
        [SerializeField] GameObject[] destoryOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;
        [SerializeField] UnityEvent onHit;

        Health target = null;
        Vector3 targetPoint;
        float damage = 0f;
        GameObject instigator = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (target != null && isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        public void SetTarget(Health target,GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, target);
        }

        public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage,null , targetPoint);
        }

        public void SetTarget(GameObject instigator,float damage,Health target=null,Vector3 targetPoint = default)
        {
            this.target = target;
            this.targetPoint = targetPoint;
            this.instigator = instigator;
            this.damage = damage;

            Destroy(gameObject, MaxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            if(target == null)
            {
                return targetPoint;
            }
            CapsuleCollider targetCapsule =target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>();
            if (target != null && health != target) return;
            if (health == null || health.IsDead()) return;
            if (other.gameObject == instigator) return;
            health.TakeDamage(instigator, damage);

            speed = 0;

            onHit.Invoke();
            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            foreach(GameObject toDestory in destoryOnHit)
            {
                Destroy(toDestory, lifeAfterImpact);
            }

            Destroy(gameObject);
        }

    }
}