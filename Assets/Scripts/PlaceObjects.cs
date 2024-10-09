using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для размещения объектов
public class PlaceObjects : MonoBehaviour
{
    // Слой, по которому можно размещать объекты
    public LayerMask layer;
    // Скорость вращения объекта
    public float rotateSpeed = 60f;

    // Метод, вызываемый при старте
    private void Start()
    {
        // Установка позиции объекта
        PositionObject();
    }

    // Метод, вызываемый каждый кадр
    private void Update()
    {
        // Установка позиции объекта
        PositionObject();

        // Размещение объекта по клику мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Включение скрипта AutoCarCreate
            gameObject.GetComponent<AutoCarCreate>().enabled = true;
            // Удаление скрипта PlaceObjects
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }

        // Вращение объекта, если зажата клавиша LeftShift
        if (Input.GetKey(KeyCode.LeftShift))
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    // Метод для установки позиции объекта
    private void PositionObject()
    {
        // Создание луча от камеры к позиции мыши
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Переменная для хранения результата попадания луча
        RaycastHit hit;

        // Проверка попадания луча и установка позиции объекта
        if (Physics.Raycast(ray, out hit, 1000f, layer))
            transform.position = hit.point;
    }
}
