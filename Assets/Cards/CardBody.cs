using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBody : MonoBehaviour
{
    Card card;

    private Vector2 TargetPosition = Vector2.zero;
    private SpriteRenderer renderer;
    private Collider2D collider;
    private bool Animating = false; //Perfoming an animation


    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Description;
    [SerializeField]
    private Text Cost;
    [SerializeField]
    private Text Stamina;
    private Canvas canvas;

    public void Innit(Card card)
    {
        this.card = card;
    }

    private void UpdateText()
    {
        Name.text = card.name;
        Description.text = card.description;
        Cost.text = card.ManaCost.ToString();
        Stamina.text = card.Stamina.ToString();
    }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        canvas = GetComponentInChildren<Canvas>();
    }
    void Update()
    {
        if (Animating)
            return;
        if (card != null)
            UpdateText();

        if (!isOnTargetPosition())
            MoveToTarget();
    }

    public void Discard()
    {
        StartCoroutine("DiscardAnimationRoutine");
    }

    public void SetPosition(Vector2 position)
    {
        TargetPosition = position;
    }

    public void SetPriority(int priority)
    {
        renderer.sortingOrder = priority * 2; //Card sorting layer is even so canvas can be between cards
        canvas.sortingOrder = priority * 2 + 1;
    }

    public bool IsHovered()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (collider.OverlapPoint(MousePos))
            return true;

        Vector2 closest = collider.ClosestPoint(MousePos);
        if (Mathf.Abs(closest.x - MousePos.x) < 0.1f && closest.y >= MousePos.y)
            return true; //Mouse is under the card

        return false;
    }


    bool isOnTargetPosition()
    {
        if (Vector2.Distance(TargetPosition, transform.position) < 0.001f) //Arbitrary epsilon value
        {
            transform.position = TargetPosition;
            return true;
        }
        return false;
    }

    void MoveToTarget()
    {
        float Velocity = 10f * Time.deltaTime;
        if (Velocity > 1)
            Velocity = 1;
        transform.position = Vector2.Lerp(transform.position, TargetPosition, Velocity);
    }

    public void Reshuffle()
    {
        StartCoroutine("ReshuffleRoutine");
    }

    private void StartAnimating()
    {
        Animating = true;
        GetComponentInChildren<Canvas>().sortingLayerName = "Default";
        renderer.sortingLayerName = "Default";
    }

    //ANIMATION ROUTINES
    IEnumerator DiscardAnimationRoutine()
    {
        StartAnimating();
        //Values
        float wait = 2f;
        float gravity = 50f;
        float initialJump = 20;
        float rangeLateralJump = 5;
        float rangeRotationSpeed = 50;

        float RotationSpeed = Random.Range(-rangeRotationSpeed, rangeRotationSpeed);
        Vector3 Velocity = new Vector3(Random.Range(-rangeLateralJump, rangeLateralJump), initialJump);

        while(wait > 0)
        {
            Velocity += Vector3.down * gravity * Time.deltaTime;
            transform.position += Velocity * Time.fixedDeltaTime;
            transform.Rotate(Vector3.forward * RotationSpeed * Time.fixedDeltaTime);
            wait -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        card.ResetBody();
    }

    IEnumerator ReshuffleRoutine()
    {
        StartAnimating();

        TargetPosition = CombatManager.Instance.CardManager.Deck.transform.position;

        while(!isOnTargetPosition())
        {
            MoveToTarget();
            yield return new WaitForFixedUpdate();
        }

        card.ResetBody();
    }
}
