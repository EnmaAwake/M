using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Weapon : MonoBehaviour
{
    Animator animator;
    public bool weapon1;
    public bool weapon2;
    public Transform firePoint;
    public GameObject redBulletPrefab;
    public GameObject blueBulletPrefab;
    public bool _isEnabled = true;
    public Color rColor = Color.red;
    public Color bColor = Color.blue;
    public float _size = 0.3f;

    void Start()
    {
        weapon1 = true;
        animator = GetComponent<Animator>();
        animator.SetBool("Red", true);
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && weapon1 == true)
        {
            weapon1 = false;
            weapon2 = true;
            BlueHalo();
            animator.SetBool("Red", false);

        }
        else if(Input.GetButtonDown("Fire2") && weapon2 == true)
        {
            weapon1 = true;
            weapon2 = false;
            RedHalo();
            animator.SetBool("Blue", false);
        }

        if(Input.GetButtonDown("Fire1") && weapon1 == true)
        {
            RedShoot();
        }
        else if(Input.GetButtonDown("Fire1") && weapon2 == true)
        {
            BlueShoot();
        }
    }

    void RedShoot()
    {
        Instantiate(redBulletPrefab, firePoint.position, firePoint.rotation);
        animator.SetTrigger("Shooting");
    }

    void BlueShoot()
    {
        Instantiate(blueBulletPrefab, firePoint.position, firePoint.rotation);
        animator.SetTrigger("Shooting");
    }

    void BlueHalo()
    {
            SerializedObject halo = new SerializedObject(GetComponent("Halo"));
            halo.FindProperty("m_Size").floatValue = _size;
            halo.FindProperty("m_Enabled").boolValue = _isEnabled;
            halo.FindProperty("m_Color").colorValue = bColor;
            halo.ApplyModifiedProperties();
            animator.SetBool("Blue", true);
    }

    void RedHalo()
    {
            SerializedObject halo = new SerializedObject(GetComponent("Halo"));
            halo.FindProperty("m_Size").floatValue = _size;
            halo.FindProperty("m_Enabled").boolValue = _isEnabled;
            halo.FindProperty("m_Color").colorValue = rColor;
            halo.ApplyModifiedProperties();
            animator.SetBool("Red", true);
    }
}
