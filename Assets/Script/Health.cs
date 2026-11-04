using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public int hp = 3;
    public int maxHp = 5;
    public TextMeshProUGUI hpText;     
    public AudioClip hurtClip;
    public AudioClip healClip;

    bool invincible;
    Renderer rend;
    AudioSource src;

    void Start()
    {
        rend = GetComponent<Renderer>();
        src = GetComponent<AudioSource>();
        UpdateUI();
    }

    public void TakeDamage(int dmg)
    {
        if (invincible) return;          

        hp -= dmg;
        Debug.Log("HP: " + hp);
        UpdateUI();
        Play(hurtClip);

        if (hp <= 0)
        {
            GameOver();
            return;
        }
        StartCoroutine(Invincibility());
    }

    public void Heal(int amount)
    {
        hp = Mathf.Min(hp + amount, maxHp);
        Debug.Log("HP: " + hp);
        UpdateUI();
        Play(healClip);
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        for (int i = 0; i < 10; i++)
        {
            rend.enabled = !rend.enabled;              
            yield return new WaitForSeconds(0.2f);    
        }
        rend.enabled = true;
        invincible = false;
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        if (hpText != null) hpText.text = "HP: " + hp;
    }

    void Play(AudioClip clip)
    {
        if (src != null && clip != null) src.PlayOneShot(clip);
    }
}
