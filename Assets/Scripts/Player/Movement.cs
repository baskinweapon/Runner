using UnityEngine;

public class Movement : MonoBehaviour {
    private Animator _animator;
    private Vector3 direction = Vector3.forward;
    public float speed;
    public float jumpPower;
    public Rigidbody rb;

    private void OnEnable() {
        GameManager.instance.inputSystem.GetInputSystem().OnJump += Jump;
        _animator = GetComponent<Animator>();
    }

    private bool _isGround;
    // main player movement
    public void Update() {
        _isGround = OnGround();
        transform.position += Time.deltaTime * speed * direction;
    }

    // rotate when player hit rotate block
    public void Rotate(Vector3 dir, Vector3 pos) {
        transform.rotation = Quaternion.LookRotation(dir);
        transform.position = pos;
        direction = dir;
    }

    // check if player is on ground
    private bool OnGround() {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1f, LayerMask.GetMask("Ground")))
            return true;
        return false;
    }
    
    private float _jumps = 2;
    private void Jump() {
        if (_isGround) _jumps = 2;
        _jumps--;
        if (_jumps < 0) return;
        _animator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpPower * 1000f);
    }

    private void OnDisable() {
        GameManager.instance.inputSystem.GetInputSystem().OnJump -= Jump;
    }
}
