using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private FirstPersonController controller;
    //[SerializeField] private float _damageAmount;

    [SerializeField] private float _regenerationRate = 5f; // Amount of health to regenerate per second
    [SerializeField] private float _regenerationDelay = 5f; // Delay before regeneration starts
    private bool _isRegenerating = false; // Flag to check if regeneration is in progress

    [SerializeField] private TMP_Text healthText;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth <= 0)
        {
            controller.Die();
            _currentHealth = _maxHealth;
        }

        UpdateHealthBar();

        // Stop any ongoing regeneration and start the delay for regeneration
        StopCoroutine("RegenerateHealth");
        _isRegenerating = false;
        StartCoroutine(StartRegenerationDelay());
    }

    private void Update()
    {
        UpdateHealthText();
    }
    private void OnCollisionEnter(Collision other)
    {
        
    }

    private void UpdateHealthBar()
    {
        _healthBarFill.fillAmount = _currentHealth / _maxHealth;
    }

    public void DecreaseMaxHealth(float amount)
    {
        _maxHealth -= amount;
        _maxHealth = Mathf.Clamp(_maxHealth, 0, float.MaxValue);
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"HP: {_currentHealth} / {_maxHealth}";
    }

    private IEnumerator StartRegenerationDelay()
    {
        yield return new WaitForSeconds(_regenerationDelay);
        StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        _isRegenerating = true;
        while (_currentHealth < _maxHealth)
        {
            _currentHealth += _regenerationRate * Time.deltaTime;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            UpdateHealthBar();
            yield return null; // Wait for the next frame
        }
        _isRegenerating = false;
    }
}
