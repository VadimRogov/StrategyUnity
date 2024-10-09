using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для управления пулями
public class BulletController : MonoBehaviour
{
    // Позиция цели
    [NonSerialized]
    public Vector3 position;
    // Скорость пули
    public float speed = 20f;
    // Урон от пули
    public int damage = 20;

    // Метод, вызываемый каждый кадр
    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);

        if (transform.position == position)
            Destroy(gameObject);
    }

    // Метод для обработки столкновения пули с объектом
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            CarAttack attack = other.GetComponent<CarAttack>();
            if (attack != null)
            {
                attack.health -= damage;

                Transform healthBar = other.transform.GetChild(0).transform;
                healthBar.localScale = new Vector3(
                    healthBar.localScale.x - 0.3f,
                    healthBar.localScale.y,
                    healthBar.localScale.z);

                if (attack.health <= 0)
                    Destroy(other.gameObject);
            }
        }
    }
}