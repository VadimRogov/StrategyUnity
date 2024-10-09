using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для размещения зданий через кнопку
public class ButtonPlaceBuild : MonoBehaviour
{
    // Префаб здания
    public GameObject building;

    // Метод для размещения здания
    public void PlaceBuild()
    {
        // Создание экземпляра здания в начале координат
        Instantiate(building, Vector3.zero, Quaternion.identity);
    }
}

