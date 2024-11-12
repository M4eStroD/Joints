using UnityEngine;

public class SwingMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _seat;
    [SerializeField] private float _swingStrength;

    public void MoveForward()
    {
        _seat.AddTorque(_seat.transform.right * -_swingStrength, ForceMode.Impulse);
    }

    public void MoveBack()
    {
        _seat.AddTorque(_seat.transform.right * _swingStrength, ForceMode.Impulse);
    }
}