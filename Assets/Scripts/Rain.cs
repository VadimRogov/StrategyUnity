using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Класс для управления дождем
public class Rain : MonoBehaviour
{
    // Направленный свет
    public Light dirLight;
    // Система частиц для дождя
    private ParticleSystem ps;
    // Флаг, указывающий, идет ли дождь
    private bool isRain = false;

    // Метод, вызываемый при старте
    private void Start()
    {
        // Получение компонента ParticleSystem
        ps = GetComponent<ParticleSystem>();
        // Запуск корутины для изменения погоды
        StartCoroutine(Weather());
    }

    // Метод, вызываемый каждый кадр
    private void Update()
    {
        // Изменение интенсивности света в зависимости от погоды
        if (isRain && dirLight.intensity > 0.4f)
            LightIntensity(-1);
        else if (!isRain && dirLight.intensity < 0.7f)
            LightIntensity(1);
    }

    // Метод для изменения интенсивности света
    private void LightIntensity(int mult)
    {
        dirLight.intensity += 0.1f * Time.deltaTime * mult;
    }

    // Корутина для изменения погоды
    IEnumerator Weather()
    {
        while (true)
        {
            // Ожидание от 10 до 15 секунд
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f));

            // Включение или выключение дождя
            if (isRain)
                ps.Stop();
            else
                ps.Play();

            // Инвертирование флага дождя
            isRain = !isRain;
        }
    }
}