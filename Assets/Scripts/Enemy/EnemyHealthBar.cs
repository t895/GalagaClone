using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;
    public Slider slider;
    public Image fill;
    public float healthBarOffset;
    public float timeBeforeDisable;
    private float pastHealth = 100;
    private IEnumerator coroutine;

    void Start()
    {
        coroutine = WaitForFadeOut();
    }

    void Update()
    {
        if(enemy.health != pastHealth)
        {
            coroutine = WaitForFadeOut();
            StartCoroutine(coroutine);
            slider.value = enemy.health / 100;
        }
        pastHealth = enemy.health;

        transform.position = enemy.transform.position + new Vector3(0f, healthBarOffset, 0f);
    }

    private IEnumerator WaitForFadeOut()
    {
        fill.enabled = true;
        yield return new WaitForSeconds(timeBeforeDisable);
        fill.enabled = false;
    }
}
