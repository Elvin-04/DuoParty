using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private string color;

    public int caseSize;
    public float speed;
    public Vector2 moveDist;

    public bool isMoving;
    private Vector2 dest;

    RaycastHit hit;

    private void Start()
    {
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving)
        {        

            transform.position = Vector2.MoveTowards(transform.position, dest, speed*Time.deltaTime);
            if(Vector2.Distance(dest, player1.transform.position) < 0.05f)
            {
                player1.transform.position = dest;
                isMoving = false;
            }
        }
    }

    public void GridMovementPlayer(InputAction.CallbackContext ctx)
    {

        if (ctx.performed && !isMoving)
        {
            if(ctx.ReadValue<Vector2>().x != 0 && ctx.ReadValue<Vector2>().y != 0)
            {
                return;
            }

            
            dest = new Vector2( transform.position.x, transform.position.y ) + ctx.ReadValue<Vector2>();

            RaycastHit2D hit = Physics2D.Raycast(dest, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent<Case>(out Case _case) && (_case.GetColor() == color || _case.GetColor() == ""))
            {
                isMoving = true;
            }
        }
    }

}
