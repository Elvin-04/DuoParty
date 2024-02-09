using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private string color;
    [SerializeField] private TrailRenderer trailRenderer;

    public int caseSize;
    public float speed;
    public Vector2 moveDist;
    public bool canModeDown;
    public bool canModeUp;
    public bool canMoveRight;
    public bool canMoveLeft;
    public bool isMoving;
    private Vector2 dest;

    int turn;

    RaycastHit hit;
    public Cards card;
    public Case actCase;
    public Case destCase;

    private void Start()
    {
        isMoving = false;

        if(color ==  "Red")
            turn = 1;
        else
            turn = 0;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, speed*Time.deltaTime);
            if(Vector2.Distance(dest, transform.position) < 0.05f)
            {
                actCase = destCase;
                destCase = null;
                transform.position = dest;
                isMoving = false;
                trailRenderer.enabled = true;

            }
        }
    }

    public void GridMovementPlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isMoving && RoundByRound.instance.turn == turn)
        {
            
            if(ctx.ReadValue<Vector2>().x != 0 && ctx.ReadValue<Vector2>().y != 0)
            {
                return;
            }
                    
                    
            dest = new Vector2( transform.position.x, transform.position.y ) + ctx.ReadValue<Vector2>();

            RaycastHit2D hit = Physics2D.Raycast(dest, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent<Case>(out Case _case) && (_case.GetColor() == color || _case.GetColor() == "RedAndGreen" || _case.GetColor() == ""))
            {
                
                destCase = _case;

                Path newPath = destCase.GetPathByColor(color);
                Path lastPath = actCase.GetPathByColor(color);

                _case.GetCard();
                if (ctx.ReadValue<Vector2>().x == 1 && _case != null && newPath.canMoveLeft && (actCase == null || lastPath.canMoveRight))
                {
                    isMoving = true;
                }
                else if (ctx.ReadValue<Vector2>().x == -1 && _case != null && newPath.canMoveRight && (actCase == null || lastPath.canMoveLeft))
                {
                    isMoving = true;
                }
                else if (ctx.ReadValue<Vector2>().y == 1 && _case != null && newPath.canMoveDown && (actCase == null || lastPath.canMoveUp))
                {
                    isMoving = true;
                }
                else if (ctx.ReadValue<Vector2>().y == -1 && _case != null && newPath.canMoveUp && (actCase == null || lastPath.canMoveDown))
                {
                    isMoving = true;
                }
            }

        }
    }
}
