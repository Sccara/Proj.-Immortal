using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject popUpWindow;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask interactLayerMask;
    [SerializeField] private IInteractable current;

    private Collider[] objsToInteract = new Collider[10];

    private void OnEnable()
    {
        inputReader.OnInteractPressed += Interact;
    }

    private void OnDisable()
    {
        inputReader.OnInteractPressed -= Interact;
    }

    private void Update()
    {
        CheckInteract();
    }

    public void CheckInteract()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, interactRadius, objsToInteract, interactLayerMask);

        float minDistance = Mathf.Infinity;
        GameObject closestObj = null;

        for (int i = 0; i < count; i++)
        {
            float distance = (transform.position - objsToInteract[i].gameObject.transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObj = objsToInteract[i].gameObject;
            }
        }

        if (closestObj == null)
        {
            popUpWindow.SetActive(false);
            current = null; 
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
            current.Interact(playerManager);
            popUpWindow.SetActive(false);
        }
    }
}
