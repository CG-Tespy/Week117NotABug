using UnityEngine;
using UnityEngine.UI;
using Fungus;
public class Healthbar : MonoBehaviour
{
    [SerializeField] Image healthFill;
    [SerializeField] Flowchart hasHealthValues;
    [SerializeField] string healthValueName;
    [SerializeField] string maxHealthValueName;

    FloatVariable healthValueContainer, maxHealthValueContainer;
    float Health
    {
        get { return healthValueContainer.Value; }
    }
    float MaxHealth
    {
        get { return maxHealthValueContainer.Value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        GetHealthValueContainers();
    }

    void GetHealthValueContainers()
    {
        healthValueContainer = hasHealthValues.GetVariable(healthValueName) as FloatVariable;
        maxHealthValueContainer = hasHealthValues.GetVariable(maxHealthValueName) as FloatVariable;
    }

    void Update()
    {
        UpdateHealthFill();   
    }

    void UpdateHealthFill()
    {
        if (!GameIsPaused())
            healthFill.fillAmount = Health / MaxHealth;
    }

    bool GameIsPaused()
    {
        return Time.timeScale == 0;
    }
}

