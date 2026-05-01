using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject popUpWindow;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask interactLayerMask;
    [SerializeField] private IInteractable current;

    private void Start()
    {
        inputReader.OnInteractPressed += Interact;
    }

    private void Update()
    {
        CheckInteract();
    }

    public void CheckInteract()
    {
        Collider[] objsToInteract = Physics.OverlapSphere(transform.position, interactRadius, interactLayerMask);

        float minDistance = Mathf.Infinity;
        GameObject closestObj = null;
        foreach (Collider collider in objsToInteract)
        {
            float distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObj = collider.gameObject;
            }
        }

        if (closestObj == null)
        {
            popUpWindow.SetActive(false);
            return;
        }

        if (closestObj.TryGetComponent(out IInteractable interactable))
        {
            popUpWindow.SetActive(true);
            interactText.text = interactable.GetInteractText();
            current = interactable;
        }
        else
        {
            current = null;
            popUpWindow.SetActive(false);
        }
    }

    public void Interact()
    {
        if (current != null)
        {
            current.Interact(GetComponent<PlayerManager>());
            popUpWindow.SetActive(false);
        }
    }
}
