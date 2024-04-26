using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_look : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Сенса мыши

    public Transform playerBody; //Ссылка на основной обьект для поворота туловища

    float xRotation = 0f; //Переменная для работы с поворотом головы вверх/вниз

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Скрывает курсор
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //Считывание инпутов умножается на сенсу и на дэльтатайм для плавности при разном колве кадров

        xRotation -= mouseY; //"-" был в гайде. + инвертирует направление. просто = не даёт повернуть камеру.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Ограничение на поворот головы вверх/вниз

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Поворот по оси Х. Вращение относительно самого объекта. Сделан не так, как Y для внесения ограничения на поворот вверх/вниз
        playerBody.Rotate(Vector3.up * mouseX); //Поворот туловища (и камеры т.к. она в туловище). Up т.к. это ось Y
    }
}
