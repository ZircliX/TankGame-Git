using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthManager))]
public class UI_Player : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private TMP_Text playerHealthText;

    private HealthManager hm;

    private void Awake()
    {
        hm = gameObject.GetComponent<HealthManager>();
    }

    public void Update()
    {
        playerHealthBar.fillAmount = hm.health / 100;
        playerHealthText.text = "Health : " + hm.health + "%";
    }
}