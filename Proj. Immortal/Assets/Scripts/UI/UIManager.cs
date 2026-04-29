using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private InputReader inputReader;

    [Header("HUD")]
    [SerializeField] private GameObject hudContainer;

    [Header("Windows")]
    [SerializeField] private List<WindowMapping> registeredWindows;

    private Dictionary<WindowType, UIWindow> _windows = new Dictionary<WindowType, UIWindow>();
    private Stack<UIWindow> _windowStack = new Stack<UIWindow>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
           
        foreach (var mapping in registeredWindows)
        {
            _windows.Add(mapping.type, mapping.windowObject);
            mapping.windowObject.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        inputReader.OnInventoryPressed += () =>
        { 
            ToggleWindow(WindowType.Inventory, hideHUD: true); 
        };
    
        inputReader.OnEquipmentPressed += () =>
        {
            ToggleWindow(WindowType.Equipment, hideHUD: true);
        };

        inputReader.OnEscapePressed += CloseLastWindow;
    }

    private void OnDestroy()
    {
        if (inputReader != null)
        {
            //inputReader.OnInventoryPressed -= HandleInventoryToggle;
            inputReader.OnEscapePressed -= CloseLastWindow;
        }
    }

    public void ToggleWindow(WindowType type, bool hideHUD = false)
    {
        if (_windows.TryGetValue(type, out UIWindow window))
        {
            if (window.gameObject.activeSelf)
            {
                CloseLastWindow();
            }
            else
            {
                OpenWindow(window, hideHUD);
            }
        }
    }

    private void OpenWindow(UIWindow window, bool hideHUD)
    {
        window.OnOpen();
        _windowStack.Push(window);

        if (hideHUD && hudContainer != null)
            hudContainer.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseLastWindow()
    {
        if (_windowStack.Count > 0)
        {
            UIWindow window = _windowStack.Pop();
            window.OnClose();

            if (_windowStack.Count == 0)
            {
                if (hudContainer != null) hudContainer.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public T GetWindow<T>(WindowType type) where T : UIWindow
    {
        return registeredWindows.Find((w) => w.type == type).windowObject.GetComponent<T>();

        //foreach (var window in registeredWindows)
        //{
        //    if (window.type == type)
        //    {
        //        return window.windowObject.GetComponent<T>();
        //    }
        //}

        //return null;
    }
}

[System.Serializable]
public struct WindowMapping
{
    public WindowType type;
    public UIWindow windowObject;
}

public enum WindowType
{
    Inventory,
    LevelUp,
    PauseMenu,
    Equipment,
    Selection
}
