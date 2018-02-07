using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    #region Field

    public float rotateSpeed = 100;

    private Quaternion targetRotation;

    private Quaternion previousQuaternion;

    #endregion Field

    #region Method

    protected virtual void Start()
    {
        this.targetRotation = Random.rotation;
        this.previousQuaternion = base.transform.rotation;
    }

    protected virtual void Update()
    {
        this.previousQuaternion = this.transform.rotation;

        this.transform.rotation = Quaternion.RotateTowards
            (base.transform.rotation, this.targetRotation, Time.deltaTime * this.rotateSpeed);

        if (base.transform.rotation == this.previousQuaternion
         || this.transform.rotation == this.targetRotation)
        {
            this.targetRotation = Random.rotation;
        }
    }

    #endregion Method
}