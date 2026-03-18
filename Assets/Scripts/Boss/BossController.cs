using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Health")]
    public int bossHp = 6; // Keeps track of boss health
    
    [Header("Phase Thresholds")]
    private int phase = 1; // Keeps track of phases
    
    [Header("Attack Timing")] // Time in between each attack
    private float attackTimer = 0f;
    private float nextAttackTime;
    
    [Header("Attack Scripts")] // List of scripts for all attack functionalities
    [SerializeField] private BabyTrollSpawner babyTrollSpawner;
    [SerializeField] private LivingRootSpawner livingRootSpawner;
    [SerializeField] private BoulderSpawner boulderSpawner;
    
    private int altarsActivated = 0;
    [SerializeField] private AltarController[] altars;

    void OnEnable()
    {
        AltarController.OnAltarActivated += HandleAltarActivated;
    }

    void OnDisable()
    {
        AltarController.OnAltarActivated -= HandleAltarActivated;
    }

    void Start()
    {
        SetNextAttackTime();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= nextAttackTime) // Trigger the next Attack
        {
            attackTimer = 0f;
            TriggerAttack();
            SetNextAttackTime();
        }
    }

    void SetNextAttackTime()
    {
        switch (phase)
        {
            case 1: nextAttackTime = Random.Range(20f, 25f); break;
            case 2: nextAttackTime = Random.Range(15f, 20f); break;
            case 3: nextAttackTime = Random.Range(12f, 15f); break;
        }
    }

    void TriggerAttack()
    {
        int attackChoice = Random.Range(0, phase);

        switch (attackChoice)
        {
            case 0: BabyTrollSpawner.Spawn(); break;
            case 1: LivingRootSpawner.Spawn(); break;
            case 2: boulderSpawner.Launch(); break;
        }
    }

    void UpdatePhase() // Switch between phases based on boss hp
    {
        switch (bossHp)
        {
            case 6:
            case 5:
                phase = 1; break;
            case 4:
            case 3:
                phase = 2; break;
            case 2:
            case 1:
                phase = 3; break;
            case 0:
                BossDead(); break;
        }
        attackTimer = 0f;
        SetNextAttackTime();
    }

    void HandleAltarActivated()
    {
        altarsActivated++;
        if (altarsActivated >= 3)
        {
            DamageBoss();
            ResetAltar();
        }
    }

    void DamageBoss()
    {
        bossHp--; // Decreases the health of the boss
        Debug.Log($"Boss HP: {bossHp} | Phase : {phase}");
        UpdatePhase();
    }

    void BossDead()
    {
        Debug.Log("Boss defeated");
        // Trigger death animation here.
    }

    void ResetAltar()
    {
        altarsActivated = 0; // Resets the count of activated altars
        foreach (var altar in altars)
            altar.Reset();
    }
}