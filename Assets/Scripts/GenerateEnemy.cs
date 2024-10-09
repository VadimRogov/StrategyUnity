using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для генерации врагов
public class GenerateEnemy : MonoBehaviour
{
    // Массив точек для генерации
    public Transform[] points;
    // Префаб фабрики
    public GameObject factory;

    // Метод, вызываемый при старте
    private void Start()
    {
        // Запуск корутины для создания фабрик
        StartCoroutine(SpawnFactory());
    }

    // Корутина для создания фабрик
    IEnumerator SpawnFactory()
    {
        for (int i = 0; i < points.Length; i++)
        {
            yield return new WaitForSeconds(10f);
            GameObject spawn = Instantiate(factory);
            Destroy(spawn.GetComponent<PlaceObjects>());
            spawn.transform.position = points[i].position;
            spawn.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
            spawn.GetComponent<AutoCarCreate>().enabled = true;
            spawn.GetComponent<AutoCarCreate>().isEnemy = true;
        }
    }
}
