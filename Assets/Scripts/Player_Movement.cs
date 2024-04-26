using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Character Controller ������
//��� ��������� �� ������ ����� ������ Step Offset
//Slope Limit �������� �� ������������ ���� ��������� �� �����������

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller; //������ �� ����������

    public float speed = 12f; //������������� �������� ������
    public float jumpHeight = 3f; //������ ������
    public float gravity = -9.81f; //��������� ���������� �������. ����������� �������� �� ��������� ����.
    Vector3 velocity; //������ ��� ����� � �����������

    public Transform groundCheck; //������ �� ������ Ground Check
    public float groundDistance = 0.1f; //������ ����� �������� �����
    bool isGrounded; // ��� ������ �����������



    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance); //�������� �� ���������� �� �����. ����� ���� �� ������� ������ � �������� ���� � ��� �������.

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -10f; // -2, � �� 0 �.�. ������ ����� ����� �� ������ ���������
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); //��������� �������. "Horizontal" � "Vertical" �������� �� ���� � ����� �������� � ����������.

        if (Input.GetKey(KeyCode.LeftShift))
        {
            z += z;
        }

        Vector3 move = transform.right * x + transform.forward * z; //"������� �����������" transform.right �������� �� ����������� "�������" � �.�.
        controller.Move(move * speed * Time.deltaTime); //�������� ������� �������� �����������

        if (Input.GetButtonDown("Jump") && isGrounded) //Jump - ���� �� ���������, ��� "Horizontal" � "Vertical"
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //�������� ������ � ������. ������ �� �������, ����.
        }

        velocity.y += gravity * Time.deltaTime; //"�������" �� Y ������������� �� �������� ���������� �������
        controller.Move(velocity * Time.deltaTime); //�������� ��������� �� ����������. ������ Time.deltaTime ��-�� ������� ������� ���������� �������.
    }
}
