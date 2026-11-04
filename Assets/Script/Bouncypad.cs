using UnityEngine;


public class BouncyPad : MonoBehaviour
{
    [Range(0f, 1f)] public float bounciness = 0.9f;

    void Awake()
    {
        Collider col = GetComponent<Collider>();

        PhysicsMaterial mat = new PhysicsMaterial("Bouncy");
        mat.bounciness = bounciness;
        mat.bounceCombine = PhysicsMaterialCombine.Maximum; 
        mat.dynamicFriction = 0f;
        mat.staticFriction = 0f;

        col.sharedMaterial = mat;
    }
}