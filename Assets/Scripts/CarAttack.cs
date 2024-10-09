using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Класс для атаки машин
public class CarAttack : MonoBehaviour
{
    // Здоровье машины
    [NonSerialized]
    public int health = 100;
    // Радиус атаки
    public float radius = 70f;
    // Префаб пули
    public GameObject bullet;
    // Корутина атаки
    private Coroutine coroutine = null;

    // Метод, вызываемый каждый кадр
    private void Update()
    {
        DetectCollistion();
    }

    // Метод для обнаружения коллизий
    private void DetectCollistion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        if (hitColliders.Length == 0 && coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;

            if (gameObject.CompareTag("Enemy"))
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        }

        foreach (var e in hitColliders)
        {
            if ((gameObject.CompareTag("Player") && e.gameObject.CompareTag("Enemy")) ||
                    (gameObject.CompareTag("Enemy") && e.gameObject.CompareTag("Player")))
            {
                if (gameObject.CompareTag("Enemy"))
                    GetComponent<NavMeshAgent>().SetDestination(e.transform.position);

                if (coroutine == null)
                    coroutine = StartCoroutine(StartAttck(e));
            }
        }
    }

    // Корутина для начала атаки
    IEnumerator StartAttck(Collider enemyPos)
    {
        GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
        obj.GetComponent<BulletController>().position = enemyPos.transform.position;
        yield return new WaitForSeconds(1f);
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
