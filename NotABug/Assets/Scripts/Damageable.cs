using UnityEngine;
using UnityEngine.Events;
using Fungus;

public class Damageable : MonoBehaviour
{
    public UnityAction Death = delegate{};

    [SerializeField] string healthValueName;
    [SerializeField] Flowchart hasHealthValue;

    FloatVariable healthVariable;
    protected float HealthValue
    {
        get { return healthVariable.Value; }
        set { healthVariable.Value = value; }
    }

    void Awake()
    {
        healthVariable = hasHealthValue.GetVariable(healthValueName) as FloatVariable;

    }

    public void TakeDamage(float damage)
    {
        if (HealthValue <= 0)
            return;
            
        HealthValue -= damage;
        if (HealthValue <= 0)
            Death.Invoke();
    }
}
