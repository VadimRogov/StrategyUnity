using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для управления камерой
public class CameraControl : MonoBehaviour
{
    // Скорость вращения, перемещения и зума камеры
    public float rotateSpeed = 10.0f, speed = 10.0f, zoomSpeed = 1000000.0f;

    // Множитель скорости
    private float mult = 10.0f;

    // Метод, вызываемый каждый кадр
    private void Update()
    {
        // Получение ввода для горизонтального и вертикального перемещения
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        // Определение направления вращения
        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q))
            rotate = -1f;
        else if (Input.GetKey(KeyCode.E))
            rotate = 1f;

        // Удвоение скорости, если зажата клавиша LeftShift
        mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        // Вращение камеры вокруг оси Y в мировом пространстве
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * mult, Space.World);
        // Перемещение камеры в локальном пространстве
        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * mult * speed, Space.Self);

        // Зум камеры с использованием колесика мыши
        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");

        // Ограничение высоты камеры
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 30f),
            transform.position.z);
    }
}