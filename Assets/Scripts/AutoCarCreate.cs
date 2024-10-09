using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для автоматического создания машин
public class AutoCarCreate : MonoBehaviour
{
    // Флаг, указывающий, является ли машина вражеской
    [NonSerialized]
    public bool isEnemy = false;
    // Префаб машины
    public GameObject car;
    // Время между созданием машин
    public float time = 5f;

    // Метод, вызываемый при старте
    private void Start()
    {
        // Запуск корутины для создания машин
        StartCoroutine(SpawnCar());
    }

    // Корутина для создания машин
    IEnumerator SpawnCar()
    {
        for (int i = 1; i <= 3; i++)
        {
            yield return new WaitForSeconds(time);
            Vector3 pos = new Vector3(
                transform.GetChild(0).position.x + UnityEngine.Random.Range(3f, 7f),
                transform.GetChild(0).position.y,
                transform.GetChild(0).position.z + UnityEngine.Random.Range(3f, 7f));

            GameObject spawn = Instantiate(car, pos, Quaternion.identity);

            if (isEnemy)
                spawn.tag = "Enemy";
            else
                spawn.tag = "Player";
        }
    }
}