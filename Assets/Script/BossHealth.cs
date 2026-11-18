
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public BossStats stats;
    public TextMeshProUGUI bossHpText;

    private int currentHp;
    public int CurrentHp => currentHp;

    void Start()
    {
        currentHp = stats.maxHp;
        UpdateHPText();
    }

    void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();

        if (bullet != null)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHp <= 0) return;

        currentHp -= damage;
        currentHp = Mathf.Max(0, currentHp);

        UpdateHPText();

        Debug.Log("Boss HP: " + currentHp);


        if (currentHp <= 0)
        {
            Debug.Log("BOSS DEFEATED!");

            Time.timeScale = 1f;
            SaveSystem.CompleteLevel(SceneManager.GetActiveScene().buildIndex, 0);
            SceneManager.LoadScene("Win");
        }

    }

    void UpdateHPText()
    {
        if (bossHpText != null)
        {
            bossHpText.text = "BOSS HP: " + currentHp + "/" + stats.maxHp;
        }
    }
}
