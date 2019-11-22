using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] private float _force = 100;
    [SerializeField] private float _scale = 0.2f;
    [SerializeField] private float _mass = 5f;
    private Plane _plane;
    private Material _material;

    private void Awake()
    {
        _plane = new Plane(-transform.forward, 2.5f);
        _material = new Material(Shader.Find("Legacy Shaders/Diffuse"));
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (_plane.Raycast(ray, out distance))
            {
                var point = ray.GetPoint(distance);
                ThrowProjectile(point);
            }
        }
    }

    private void ThrowProjectile(Vector3 targetPos)
    {
        var proj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        proj.transform.position = transform.position + Vector3.down * 0.25f;
        proj.transform.localScale = Vector3.one * _scale;

        proj.GetComponent<MeshRenderer>().material = _material;

        var rb = proj.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = _mass;

        var direction = (targetPos - proj.transform.position).normalized;
        var force = direction * _force;
        rb.AddForce(force, ForceMode.VelocityChange);
    }
}
