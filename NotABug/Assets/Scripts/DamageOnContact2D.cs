using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DamageOnContact2D : MonoBehaviour
{
    public enum ContactType
    {
        collision2d, trigger2d
    }
    [SerializeField] List<ContactType> contactTypes;
    [SerializeField] Flowchart hasDamageValue;
    [SerializeField] string damageValueName;
    [SerializeField] List<string> tagFilter;

    FloatVariable damageVar;
    float DamageValue { get { return damageVar.Value; } }

    void Awake()
    {
        damageVar = hasDamageValue.GetVariable(damageValueName) as FloatVariable;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!contactTypes.Contains(ContactType.trigger2d))
            return;

        DamageOtherObjectIfAppropriate(other.gameObject);
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if (!contactTypes.Contains(ContactType.collision2d))
            return;

        DamageOtherObjectIfAppropriate(other.gameObject);
    }

    void DamageOtherObjectIfAppropriate(GameObject otherObject)
    {
        Damageable toDamage = otherObject.GetComponent<Damageable>();

        if (toDamage != null)
            toDamage.TakeDamage(DamageValue);
    }
}
