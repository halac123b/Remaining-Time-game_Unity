//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/Input System/PlayerInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputAction : IInputActionCollection2, IDisposable
{
  public InputActionAsset asset { get; }
  public @PlayerInputAction()
  {
    asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5c3a7f43-6dc7-4c4b-ac0b-a4d1475483dc"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""180493ca-7391-48ad-a247-57a68ac6e0f2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""1528ecea-7935-456a-a109-cd055299e7c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Duck"",
                    ""type"": ""Button"",
                    ""id"": ""2b783868-7b56-42ce-97a1-d77b34200461"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Process"",
                    ""type"": ""Button"",
                    ""id"": ""45e58772-ba7c-420d-8001-ffb8b6a8eed6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.001)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ed55626c-c35e-48ba-b823-a55bb772a13b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8c15a824-47cf-4f30-97aa-fbc925dd3944"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ddaae70d-8e7d-44c5-8154-55011252baf7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f47d2b1e-122d-44d2-b3f8-4ec714d2409b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c64a4342-3004-4d10-8d51-28972a8fabe3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d11a4138-17c2-4e3c-b867-62802acfc210"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df288bac-8b05-4aa3-8b71-d28a023e2666"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Duck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61ba504c-f264-46f1-bd5c-3858188814b6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Process"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
    // Player
    m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
    m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
    m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
    m_Player_Duck = m_Player.FindAction("Duck", throwIfNotFound: true);
    m_Player_Process = m_Player.FindAction("Process", throwIfNotFound: true);
  }

  public void Dispose()
  {
    UnityEngine.Object.Destroy(asset);
  }

  public InputBinding? bindingMask
  {
    get => asset.bindingMask;
    set => asset.bindingMask = value;
  }

  public ReadOnlyArray<InputDevice>? devices
  {
    get => asset.devices;
    set => asset.devices = value;
  }

  public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

  public bool Contains(InputAction action)
  {
    return asset.Contains(action);
  }

  public IEnumerator<InputAction> GetEnumerator()
  {
    return asset.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  public void Enable()
  {
    asset.Enable();
  }

  public void Disable()
  {
    asset.Disable();
  }

  public IEnumerable<InputBinding> bindings => asset.bindings;

  public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
  {
    return asset.FindAction(actionNameOrId, throwIfNotFound);
  }

  public int FindBinding(InputBinding bindingMask, out InputAction action)
  {
    return asset.FindBinding(bindingMask, out action);
  }

  // Player
  private readonly InputActionMap m_Player;
  private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
  private readonly InputAction m_Player_Move;
  private readonly InputAction m_Player_Run;
  private readonly InputAction m_Player_Duck;
  private readonly InputAction m_Player_Process;
  public struct PlayerActions
  {
    private @PlayerInputAction m_Wrapper;
    public PlayerActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
    public InputAction @Move => m_Wrapper.m_Player_Move;
    public InputAction @Run => m_Wrapper.m_Player_Run;
    public InputAction @Duck => m_Wrapper.m_Player_Duck;
    public InputAction @Process => m_Wrapper.m_Player_Process;
    public InputActionMap Get() { return m_Wrapper.m_Player; }
    public void Enable() { Get().Enable(); }
    public void Disable() { Get().Disable(); }
    public bool enabled => Get().enabled;
    public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
    public void AddCallbacks(IPlayerActions instance)
    {
      if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
      m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
      @Move.started += instance.OnMove;
      @Move.performed += instance.OnMove;
      @Move.canceled += instance.OnMove;
      @Run.started += instance.OnRun;
      @Run.performed += instance.OnRun;
      @Run.canceled += instance.OnRun;
      @Duck.started += instance.OnDuck;
      @Duck.performed += instance.OnDuck;
      @Duck.canceled += instance.OnDuck;
      @Process.started += instance.OnProcess;
      @Process.performed += instance.OnProcess;
      @Process.canceled += instance.OnProcess;
    }

    private void UnregisterCallbacks(IPlayerActions instance)
    {
      @Move.started -= instance.OnMove;
      @Move.performed -= instance.OnMove;
      @Move.canceled -= instance.OnMove;
      @Run.started -= instance.OnRun;
      @Run.performed -= instance.OnRun;
      @Run.canceled -= instance.OnRun;
      @Duck.started -= instance.OnDuck;
      @Duck.performed -= instance.OnDuck;
      @Duck.canceled -= instance.OnDuck;
      @Process.started -= instance.OnProcess;
      @Process.performed -= instance.OnProcess;
      @Process.canceled -= instance.OnProcess;
    }

    public void RemoveCallbacks(IPlayerActions instance)
    {
      if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
        UnregisterCallbacks(instance);
    }

    public void SetCallbacks(IPlayerActions instance)
    {
      foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
        UnregisterCallbacks(item);
      m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
      AddCallbacks(instance);
    }
  }
  public PlayerActions @Player => new PlayerActions(this);
  public interface IPlayerActions
  {
    void OnMove(InputAction.CallbackContext context);
    void OnRun(InputAction.CallbackContext context);
    void OnDuck(InputAction.CallbackContext context);
    void OnProcess(InputAction.CallbackContext context);
  }
}
