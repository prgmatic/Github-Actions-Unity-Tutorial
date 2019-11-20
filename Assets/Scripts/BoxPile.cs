using UnityEngine;

public class BoxPile : MonoBehaviour
{
    [SerializeField] private float _scale = 0.25f;
    [SerializeField] private float _length = 0.25f;
    [SerializeField] private int _rows = 10;
    [SerializeField] private float _mass = 1f;
    private Material _materal;

    private void Start()
    {
        _materal = new Material(Shader.Find("Legacy Shaders/Diffuse"));

        for (int row = 0; row < _rows; row++)
        {
            int boxCount = _rows - row;
            for (int i = 0; i < boxCount; i++)
            {
                var left = Vector3.left * (boxCount * _scale * 0.5f + _scale * 0.5f);
                var startPos = Vector3.up * (row * _scale + _scale * 0.5f);
                startPos += left;
                CreateCube(startPos + Vector3.right * _scale * i);
            }
        }
    }

    private void CreateCube(Vector3 position)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        var scale = Vector3.one * _scale;
        scale.x = _length;
        cube.transform.localScale = scale;
        cube.transform.SetParent(transform);
        cube.transform.localPosition = position;

        var rb = cube.AddComponent<Rigidbody>();
        rb.mass = _mass;

        var renderer = cube.GetComponent<MeshRenderer>();
        renderer.sharedMaterial = _materal;
        renderer.material.color = Color.HSVToRGB(Random.value, 1, 1);
    }
}
