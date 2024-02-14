using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragnDrop : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GraphicRaycaster m_Raycaster;
    private bool draging = false;
    [SerializeField] private GameObject DragNDrop;
    [SerializeField] private GameObject cardHand;
    [SerializeField] private GameObject redTrashHand;
    [SerializeField] private GameObject greenTrashHand;
    [SerializeField] private OneCardPerRound stopAll;

    [SerializeField] private Sprite porte_blinde;
    [SerializeField] private Sprite camera_trap;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private InventoryManager player1inventory;
    [SerializeField] private InventoryManager player2inventory;

    [SerializeField] private GridManager gridManager;

    private GameObject oldMouseOverGameObject;

    private BonusContainer bonusContainer;


    private void Update()
    {
        //get mouse pos
        Vector3 mousePos = Input.mousePosition;
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = mousePos;

        DragNDrop.transform.position = mousePos;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(pointerEventData, results);

        // if the mouse is in an element
        if (results.Count > 0)
        {
            GameObject result = results[0].gameObject;

            // drag and drop
            if (draging)
            {
                result.transform.SetParent(DragNDrop.transform);
                DragNDrop.transform.position = mousePos;
                // rotate dragged card
                if (result.tag == "Card")
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        cardHand.GetComponent<Hand>().TrunCardLeft();
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        cardHand.GetComponent<Hand>().TrunCardRight();
                    }
                }
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (oldMouseOverGameObject != null && oldMouseOverGameObject.GetComponent<Case>().GetCard() == null)
                {
                    gridManager.RemoveHole(oldMouseOverGameObject.GetComponent<Case>());
                    oldMouseOverGameObject.GetComponent<Case>().ResetImage();
                }
                if (hit.collider != null && hit.collider.TryGetComponent<Case>(out Case _case) && _case.GetInteractible() && hit.collider.gameObject.GetComponent<Case>().GetCard() == null && !_case.isReveal)
                {
                    gridManager.AddHole(hit.collider.GetComponent<Case>());
                    hit.collider.GetComponent<Case>().AddImage(cardHand.GetComponent<Hand>().card);
                    if (oldMouseOverGameObject != null && hit.collider.transform.rotation.z != cardHand.GetComponent<Hand>().rotation)
                    {
                        hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, cardHand.GetComponent<Hand>().rotation);
                    }
                    oldMouseOverGameObject = hit.collider.gameObject;
                }
            }

            // dragging card
            if (Input.GetMouseButtonDown(0) && ((result.tag == "Card" && result.GetComponentInParent<Hand>().card != null) || (result.tag == "BonusSlot" && result.GetComponentInParent<BonusContainer>().hasItem)) /*&& !draging*/)
            {
                FindObjectOfType<AudioManager>().PlaySound("card picked up");
                draging = true;
                cardHand = result.transform.parent.gameObject;
                if (result.tag == "BonusSlot")
                {
                    bonusContainer = result.GetComponentInParent<BonusContainer>();
                }
                
            }
            else if (draging && Input.GetMouseButtonUp(0) && (result.tag == "Card" || result.tag == "BonusSlot"))
            {
                draging = false;
                // discard a card
                if (results.Count > 1 && (results[1].gameObject.name == "Green2ndHandCard" || results[1].gameObject.name == "Red2ndHandCard"))
                {
                    PlaceInSecondHand();
                    CardReturn(result.gameObject);
                    stopAll.StopAllCards();
                }

                // place card in game
                else
                {
                    oldMouseOverGameObject = null;
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    //drop the card
                    if (hit.collider != null && hit.collider.TryGetComponent<Case>(out Case _case) && _case.GetInteractible() && hit.collider.gameObject.GetComponent<Case>().GetCard() == null
                        && !_case.isKey && !_case.isVaccineGreen && !_case.isVaccineRed)
                    {
                        gridManager.AddHole(hit.collider.GetComponent<Case>());
                        if (_case.isArmouredDoor)
                        {
                            _case.isReveal = true;
                            stopAll.StopAllCards();
                            //FindObjectOfType<AudioManager>().PlaySound("armouredDoor activated");
                            PlaceInSecondHand();
                            _case.GetComponent<SpriteRenderer>().sprite = porte_blinde;
                            _case.LockMovements();

                            if (_case.up != null && NotEndSpawnOrBonus(_case.up) && _case.up.GetCard() != null && _case.up.GetEColor() != cardcolors.redAndGreen)
                            {
                                _case.up.CreateCross(_case.up.GetColor());
                            }
                            if (_case.down != null && NotEndSpawnOrBonus(_case.down) && _case.down.GetCard() != null && _case.down.GetEColor() != cardcolors.redAndGreen)
                            {
                                _case.down.CreateCross(_case.down.GetColor());
                            }
                            if (_case.left != null && NotEndSpawnOrBonus(_case.left) && _case.left.GetCard() != null && _case.left.GetEColor() != cardcolors.redAndGreen)
                            {
                                _case.left.CreateCross(_case.left.GetColor());
                            }
                            if (_case.right != null && NotEndSpawnOrBonus(_case.right) && _case.right.GetCard() != null && _case.right.GetEColor() != cardcolors.redAndGreen)
                            {
                                _case.right.CreateCross(_case.right.GetColor());
                            } 
                        }
                        else if (_case.isBomb)
                        {
                            _case.isReveal = true;
                            stopAll.StopAllCards();
                            //FindObjectOfType<AudioManager>().PlaySound("alarm activated");
                            PlaceInSecondHand();
                            _case.GetComponent<SpriteRenderer>().sprite = camera_trap;
                            
                            _case.LockMovements();
                            StartCoroutine(TrapCamera(_case));
                        }
                        else
                        {
                            _case.isReveal = true;
                            FindObjectOfType<AudioManager>().PlaySound("card droped");
                            hit.collider.gameObject.GetComponent<Case>().AddCard(cardHand.GetComponent<Hand>().card);
                            hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, cardHand.GetComponent<Hand>().rotation);
                            cardHand.GetComponent<Hand>().RemoveCard();
                            particle.transform.position = hit.collider.transform.position;
                            particle.Play();
                            stopAll.StopAllCards();
                        }
                        if (result.tag == "Card" && hit.collider != null && hit.collider.TryGetComponent<Case>(out _case) && !_case.isArmouredDoor && !_case.isBomb && _case.GetInteractible() && hit.collider.gameObject.GetComponent<Case>().GetCard() == null
                        && !_case.isKey && !_case.isVaccineGreen && !_case.isVaccineRed)
                        {
                            if (_case.isHammer || _case.isAccessCard)
                            {
                                AddBonusToPlayer(cardHand.gameObject.tag, _case);
                            }
                            hit.collider.gameObject.GetComponent<Case>().AddCard(cardHand.GetComponent<Hand>().card);
                            hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, cardHand.GetComponent<Hand>().rotation);
                            cardHand.GetComponent<Hand>().RemoveCard();
                        }
                        else if (result.tag == "BonusSlot" && hit.collider != null && hit.collider.TryGetComponent<Case>(out _case) && _case.GetInteractible() && (_case.GetCard() != null || _case.GetArmouredDoor()))
                        {
                            if (_case.GetCard() != null && bonusContainer.bonus == TypeOfBonus.hammer)
                            {
                                _case.ResetCard();
                                bonusContainer.removeItem();
                            }
                            else if (_case.GetArmouredDoor() && bonusContainer.bonus == TypeOfBonus.accessCard)
                            {
                                _case.isArmouredDoor = false;
                                bonusContainer.removeItem();
                            }

                        }
                        
                    }
                    // return image in his place
                    CardReturn(result.gameObject);
                }
            }
        }





    }
    private void CardReturn(GameObject card)
    {
        card.transform.SetParent(cardHand.transform);
        card.transform.position = new Vector2(cardHand.transform.position.x, cardHand.transform.position.y);
    }

    private void PlaceInSecondHand()
    {
        switch (cardHand.gameObject.tag)
        {
            case ("Player1"):
                {
                    greenTrashHand.GetComponent<Hand>().AddCard(cardHand.GetComponent<Hand>().card);
                    cardHand.GetComponent<Hand>().RemoveCard();
                    break;
                }
            case ("Player2"):
                {
                    redTrashHand.GetComponent<Hand>().AddCard(cardHand.GetComponent<Hand>().card);
                    cardHand.GetComponent<Hand>().RemoveCard();
                    break;
                }
        }
    }

    private void AddBonusToPlayer(string tag, Case currentCase)
    {
        switch (tag)
        {
            case ("Player1"):
                player1inventory.AddItemInInventory(currentCase.gameObject);
                break;

            case ("Player2"):
                player2inventory.AddItemInInventory(currentCase.gameObject);
                break;

        }
    }

    private bool NotEndSpawnOrBonus(Case currentCase)
    {
        return (!currentCase.GetIsEnd() && !currentCase.GetIsSpawn() && !currentCase.isKey && !currentCase.isVaccineGreen && !currentCase.isVaccineRed);
    }

    IEnumerator TrapCamera(Case _case)
    {
        if (_case.up.GetCard() == null && _case.up.GetInteractible())
        {
            StartCoroutine(blinking(_case.up.GetComponent<SpriteRenderer>(), 1));
            gridManager.AddHole(_case.up.GetComponent<Case>());
            _case.up.LockMovements();
            _case.up.isReveal = true;
        }
        if (_case.down.GetCard() == null && _case.down.GetInteractible())
        {
            StartCoroutine(blinking(_case.down.GetComponent<SpriteRenderer>(), 1));
            gridManager.AddHole(_case.down.GetComponent<Case>());
            _case.down.LockMovements();
            _case.down.isReveal = true;
        }
        if (_case.right.GetCard() == null && _case.right.GetInteractible())
        {
            StartCoroutine(blinking(_case.right.GetComponent<SpriteRenderer>(), 1));
            gridManager.AddHole(_case.right.GetComponent<Case>());
            _case.right.LockMovements();
            _case.right.isReveal = true;
        }
        if (_case.left.GetCard() == null && _case.left.GetInteractible())
        {
            StartCoroutine(blinking(_case.left.GetComponent<SpriteRenderer>(), 1));
            gridManager.AddHole(_case.left.GetComponent<Case>());
            _case.left.LockMovements();
            _case.left.isReveal = true;
        }
        yield return new WaitForSeconds(1);
        if (_case.up.GetCard() == null && _case.up.GetInteractible())
        {
            _case.up.GetComponent<SpriteRenderer>().sprite = porte_blinde;
        }
        if (_case.down.GetCard() == null && _case.down.GetInteractible())
        {
            _case.down.GetComponent<SpriteRenderer>().sprite = porte_blinde;
        }
        if (_case.right.GetCard() == null && _case.right.GetInteractible())
        {
            _case.right.GetComponent<SpriteRenderer>().sprite = porte_blinde;
        }
        if (_case.left.GetCard() == null && _case.left.GetInteractible())
        {
            _case.left.GetComponent<SpriteRenderer>().sprite = porte_blinde;
        }
    }

    IEnumerator blinking(SpriteRenderer sr, float Totaltime)
    {
        float time = 0f;
        while(time / Totaltime < 0.25f)
        {
            time += Time.deltaTime;
            sr.color = new Color(1,1-time/Totaltime*4,1-time/Totaltime*4, 1);
            yield return new WaitForEndOfFrame();
        }
        while (time / Totaltime < 0.5f)
        {
            time += Time.deltaTime;
            sr.color = new Color(1,0 + time / Totaltime * 2, 0 + time / Totaltime * 2, 1);
            yield return new WaitForEndOfFrame();

        }
        while (time / Totaltime < 0.75f)
        {
            time += Time.deltaTime;
            sr.color = new Color(1, 1 - time / Totaltime * 0.5f, 1 - time / Totaltime * 0.5f, 1);
            yield return new WaitForEndOfFrame();
        }
        while (time / Totaltime < 1f)
        {
            time += Time.deltaTime;
            sr.color = new Color(1, 0 + time / Totaltime , 0 + time / Totaltime, 1);
            yield return new WaitForEndOfFrame();
        }
    }
}
