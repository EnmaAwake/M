using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Weapon : MonoBehaviour
{
    public bool weapon1;
    public bool weapon2;
    public Transform firePoint;
    public GameObject redBulletPrefab;
    public GameObject blueBulletPrefab;
    [SerializeField] private bool _isEnabled = true;
    [SerializeField] private Color rColor = Color.red;
    [SerializeField] private Color bColor = Color.blue;
    [SerializeField] private float _size = 0.3f;

    void Start()
    {
        weapon1 = true;
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && weapon1 == true)
        {
            weapon1 = false;
            weapon2 = true;
            BlueHalo();
        }
        else if(Input.GetButtonDown("Fire2") && weapon2 == true)
        {
            weapon1 = true;
            weapon2 = false;
            RedHalo();
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
    }

    void BlueShoot()
    {
        Instantiate(blueBulletPrefab, firePoint.position, firePoint.rotation);
    }

    void BlueHalo()
    {
            SerializedObject halo = new SerializedObject(GetComponent("Halo"));
            halo.FindProperty("m_Size").floatValue = _size;
            halo.FindProperty("m_Enabled").boolValue = _isEnabled;
            halo.FindProperty("m_Color").colorValue = bColor;
            halo.ApplyModifiedProperties();
    }

    void RedHalo()
    {
            SerializedObject halo = new SerializedObject(GetComponent("Halo"));
            halo.FindProperty("m_Size").floatValue = _size;
            halo.FindProperty("m_Enabled").boolValue = _isEnabled;
            halo.FindProperty("m_Color").colorValue = rColor;
            halo.ApplyModifiedProperties();
    }
}
