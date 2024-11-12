using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(SpringJoint))]
public class ArmMover : MonoBehaviour
{
    [SerializeField] private HingeJoint _hingeJount;
    [SerializeField] private SpringJoint _springJoint;

    [SerializeField] private float _pullDownForce = 450;
    [SerializeField] private float _pullDownTargetVelocity = 22;
    [SerializeField] private float _loadAngle = -90;
    [SerializeField] private float _shootForce = 20;

    private JointMotor _jointMotor;
    private JointLimits _jointLimits;

    private float _shootAngle = 5;

    public UnityAction ReadyToRelease;

    private void Awake()
    {
        _springJoint = GetComponent<SpringJoint>();
        _hingeJount = GetComponent<HingeJoint>();

        _jointMotor = _hingeJount.motor;
        _jointLimits = _hingeJount.limits;
        _jointLimits.max = _shootAngle;
        
        _hingeJount.useLimits = true;
    }

    private void Update()
    {
        if (_hingeJount.angle <= _loadAngle)
            ReadyToRelease?.Invoke();
    }

    public void Release()
    {
        _hingeJount.useMotor = false;
    }

    public void PullDown()
    {
        _jointMotor.force = _pullDownForce;
        _jointMotor.targetVelocity = -_pullDownTargetVelocity;
        _jointLimits.min = _loadAngle;

        _springJoint.spring = _shootForce;
        _hingeJount.useMotor = true;
    }
}