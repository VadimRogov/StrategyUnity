using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Класс для управления поведением объектов
public class SelectController : MonoBehaviour
{
    // Куб для выделения области
    public GameObject cube;
    // Список игроков
    public List<GameObject> players;
    // Слои для лучевого каста
    public LayerMask layer, layerMask;
    // Камера
    private Camera cam;
    // Куб выделения
    private GameObject cubeSelection;
    // Результат лучевого каста
    private RaycastHit hit;

    // Метод, вызываемый при старте
    private void Awake()
    {
        // Получение компонента камеры
        cam = GetComponent<Camera>();
    }

    // Метод, вызываемый каждый кадр
    private void Update()
    {
        // Установка целевой точки для игроков по правому клику мыши
        if (Input.GetMouseButtonDown(1) && players.Count > 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, layer))
                foreach (var e in players)
                    e.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);
        }

        // Выделение игроков по левому клику мыши
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var e in players)
                if (e != null)
                    e.transform.GetChild(0).gameObject.SetActive(false);

            players.Clear();

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, layer))
            {
                cubeSelection = Instantiate(cube, new Vector3(hit.point.x, 1, hit.point.z), Quaternion.identity);
            }
        }

        // Изменение размера и поворота куба выделения
        if (cubeSelection)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                float xScale = (hit.point.x - hitDrag.point.x) * -1;
                float zScale = hit.point.z - hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else if (xScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                else if (zScale < 0.0f)
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                else
                    cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                cubeSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }
        }

        // Завершение выделения игроков
        if (Input.GetMouseButtonUp(0) && cubeSelection)
        {
            RaycastHit[] hits = Physics.BoxCastAll(
                cubeSelection.transform.position,
                cubeSelection.transform.localScale,
                Vector3.up,
                Quaternion.identity,
                0,
                layerMask);

            foreach (var e in hits)
            {
                if (e.collider.CompareTag("Enemy"))
                    continue;

                players.Add(e.transform.gameObject);
                e.transform.GetChild(0).gameObject.SetActive(true);
            }

            Destroy(cubeSelection);
        }
    }
}
