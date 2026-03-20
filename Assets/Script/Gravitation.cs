using UnityEngine;
using System.Collections.Generic;

public class Gravitation : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    // List of attractable objects
    public static List<Gravitation> otherObjectList;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjectList == null) {otherObjectList = new List<Gravitation>();}
        otherObjectList.Add(this);
    }
    private void FixedUpdate()
    {
        foreach (Gravitation obj in otherObjectList)
        {
            // ป้องกันไม่ให้มีแรงดึงดูดตัวเอง
            if (obj != this) {AttractForce(obj);}
        }
    }
    void AttractForce(Gravitation other)
    {
        Rigidbody otherRb = other.rb;
        // หาทิศทางระหว่างวัตถุ
        Vector3 direction = rb.position - otherRb.position;
        // ระยะห่างระหว่างวัตถุ
        float distance = direction.magnitude;
        // ถ้าวัตถุอยู่ตำแหน่งเดียวกัน ไม่ให้ทำอะไร
        if (distance == 0f) { return;}
        // ใช้สูตรหาแรงดึงดูด F = G*((m1*m2)/r^2)
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        // รวมทิศทาง เข้ากับแรงดึงดูดที่ได้
        Vector3 gravityForce = forceMagnitude * direction.normalized;
        // ใส่แรงที่ได้ให้กับวัตถุอื่น
        otherRb.AddForce(gravityForce);
    }

}
