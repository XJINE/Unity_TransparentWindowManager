using UnityEngine;

public class RandomWalkerInSphere : MonoBehaviour
{
    #region Field

    public float movingRangeRadius = 5;

    public float moveSpeed = 5;

    private Vector3 targetPosition;

    #endregion Field

    #region Method

    protected virtual void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                      this.targetPosition,
                                                      Time.deltaTime * this.moveSpeed);

        if (this.transform.position == this.targetPosition)
        {
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        this.targetPosition = Random.onUnitSphere * this.movingRangeRadius;
    }

    #endregion Method
}