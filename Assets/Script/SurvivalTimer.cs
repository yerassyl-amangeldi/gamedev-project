using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    public float goal = 60f;
    public Animator gateAnimator;      
    public Spawner spawner;             
    public TextMeshProUGUI timerText;   

    float t;
    bool won;

    void Update()
    {
        if (won) return;

        t += Time.deltaTime;         

        if (timerText != null)
            timerText.text = "Survive: " + Mathf.CeilToInt(Mathf.Max(0f, goal - t));

        if (t >= goal)
        {
            won = true;
            Debug.Log("YOU WIN!");
            gateAnimator.SetBool("Open", true);
            if (spawner != null) spawner.StopSpawning();
            if (timerText != null) timerText.text = "Gate is open!";
        }
    }
}
