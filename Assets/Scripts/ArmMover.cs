using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(SpringJoint))]
public class ArmMover : MonoBehaviour
{
    [SerializeField] private HingeJoint _hingeJoint;
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
        _hingeJoint = GetComponent<HingeJoint>();

        _jointMotor = _hingeJoint.motor;
        _jointLimits = _hingeJoint.limits;
        _jointLimits.max = _shootAngle;
        
        _hingeJoint.useLimits = true;
    }

    private void Update()
    {
        if (_hingeJoint.angle <= _loadAngle)
            ReadyToRelease?.Invoke();
    }

    public void Release()
    {
        _hingeJoint.useMotor = false;
    }

    public void PullDown()
    {
        _jointMotor.force = _pullDownForce;
        _jointMotor.targetVelocity = -_pullDownTargetVelocity;
        _jointLimits.min = _loadAngle;

        _springJoint.spring = _shootForce;
        _hingeJoint.useMotor = true;
    }
}