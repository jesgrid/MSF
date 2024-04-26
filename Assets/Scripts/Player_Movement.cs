using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Character Controller игрока
//Для взбирания на высоту нужно менять Step Offset
//Slope Limit отвечает за максимальный угол взбирания по поверхности

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller; //Ссылка на контроллер

    public float speed = 12f; //Корректировка скорости игрока
    public float jumpHeight = 3f; //Высота прыжка
    public float gravity = -9.81f; //Ускорение свободного падения. Статическое значение из реального мира.
    Vector3 velocity; //Вектор для рботы с гравитацией

    public Transform groundCheck; //Ссылка на объект Ground Check
    public float groundDistance = 0.1f; //Радиус сферы проверки земли
    bool isGrounded; // Для логики приземления



    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance); //Проверка на нахождение на земле. Созаёт лучь на позиции игрока с веркором вниз и его длинной.

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -10f; // -2, а не 0 т.к. иногда игрок может не упасть полностью
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); //Получение инпутов. "Horizontal" и "Vertical" отвечают за вазд и также работают с джойстками.

        if (Input.GetKey(KeyCode.LeftShift))
        {
            z += z;
        }

        Vector3 move = transform.right * x + transform.forward * z; //"Стрелка направления" transform.right отвечает за направление "направо" и т.д.
        controller.Move(move * speed * Time.deltaTime); //Передача вектора движения контроллеру

        if (Input.GetButtonDown("Jump") && isGrounded) //Jump - ввод по умолчанию, как "Horizontal" и "Vertical"
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Передача данных о прыжке. Расчёт по формуле, жиза.
        }

        velocity.y += gravity * Time.deltaTime; //"Падение" по Y увеличивается на значение свободного падения
        controller.Move(velocity * Time.deltaTime); //Передача ускорения на контроллер. Второй Time.deltaTime из-за формулы расчёта свободного падения.
    }
}
