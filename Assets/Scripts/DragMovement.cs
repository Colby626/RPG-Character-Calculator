using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image thisImage;
    private DamageFormulaReader damageFormulaReader;
    private Vector2 offset;
    private Vector2 startPosition;

    public void Start()
    {
        thisImage = GetComponent<Image>();
        damageFormulaReader = FindObjectOfType<DamageFormulaReader>();
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        startPosition = transform.position;
        thisImage.raycastTarget = false;
        transform.SetSiblingIndex(transform.parent.childCount-1);
        offset = new Vector2(transform.position.x, transform.position.y) - pointerEventData.position;
        damageFormulaReader.attackingCard = transform.GetComponent<CharacterCard>();
        damageFormulaReader.DamageFormulaInput();
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        transform.position = pointerEventData.position + offset;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (damageFormulaReader.receivingCard != null)
        {
            if (damageFormulaReader.receivingCard.transform.GetChild(0).gameObject.activeSelf)
            {
                damageFormulaReader.receivingCard.GetComponent<CharacterCard>().TakeDamage();
                transform.position = startPosition;
            }
        }
        thisImage.raycastTarget = true;
        damageFormulaReader.attackingCard = null;
        if (damageFormulaReader.receivingCard != null)
        {
            damageFormulaReader.receivingCard.transform.GetChild(0).gameObject.SetActive(false);
            damageFormulaReader.receivingCard = null;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (damageFormulaReader.attackingCard != null)
        {
            if (damageFormulaReader.attackingCard != transform.GetComponent<CharacterCard>())
            {
                transform.GetChild(0).gameObject.SetActive(true);
                damageFormulaReader.receivingCard = transform.GetComponent<CharacterCard>();
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}