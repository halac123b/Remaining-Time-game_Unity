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

public partial class @PlayerInputAction: IInputActionCollection2, IDisposable
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
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c10824b1-e76f-4ff7-bad7-e7f60c12e625"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""f452c7c7-ed18-4c4f-8d15-ad6c7c86b3b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack02"",
                    ""type"": ""Button"",
                    ""id"": ""53955021-00c7-4c52-9882-0eee0795f2ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack03"",
                    ""type"": ""Button"",
                    ""id"": ""75173de7-ee59-40aa-9429-730e4ba134f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""E_Btn"",
                    ""type"": ""Button"",
                    ""id"": ""df89f2bb-03e2-471b-9229-020ceade3eca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""1"",
                    ""type"": ""Button"",
                    ""id"": ""7f4686a5-65f1-4c52-ad00-47bf25f2fdc1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""2"",
                    ""type"": ""Button"",
                    ""id"": ""ca13f761-bea9-4e7d-af1f-81a07b3d73ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""3"",
                    ""type"": ""Button"",
                    ""id"": ""652f7769-75f1-4bd9-9e6d-dd4e25117b3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""tab"",
                    ""type"": ""Button"",
                    ""id"": ""b8f061d4-0c12-4664-a293-e0c21b6d3b89"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ed55626c-c35e-48ba-b823-a55bb772a13b"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
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
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Process"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c0bc02e-6d28-499f-827c-3054bf3c133d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85b03068-47ba-409e-9daa-d5b12fd2a340"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd1fcde7-d5a5-4465-9611-c158e121b2cc"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack02"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2c7b655-e9e0-48c3-809b-ab3004fdafa3"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack03"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""417a8098-ef68-4061-8982-529c7cdfb1ff"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""E_Btn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e90f5757-6f66-4026-b680-86d0c6e44733"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46d53dd1-e974-4cee-9ee1-1c091273887a"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e7c75ff-615e-4331-beb6-e22cdf304b9b"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aee56db6-ce93-421c-8eef-db410165e579"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""tab"",
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
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_ChangeWeapon = m_Player.FindAction("ChangeWeapon", throwIfNotFound: true);
        m_Player_Attack02 = m_Player.FindAction("Attack02", throwIfNotFound: true);
        m_Player_Attack03 = m_Player.FindAction("Attack03", throwIfNotFound: true);
        m_Player_E_Btn = m_Player.FindAction("E_Btn", throwIfNotFound: true);
        m_Player__1 = m_Player.FindAction("1", throwIfNotFound: true);
        m_Player__2 = m_Player.FindAction("2", throwIfNotFound: true);
        m_Player__3 = m_Player.FindAction("3", throwIfNotFound: true);
        m_Player_tab = m_Player.FindAction("tab", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_ChangeWeapon;
    private readonly InputAction m_Player_Attack02;
    private readonly InputAction m_Player_Attack03;
    private readonly InputAction m_Player_E_Btn;
    private readonly InputAction m_Player__1;
    private readonly InputAction m_Player__2;
    private readonly InputAction m_Player__3;
    private readonly InputAction m_Player_tab;
    public struct PlayerActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Duck => m_Wrapper.m_Player_Duck;
        public InputAction @Process => m_Wrapper.m_Player_Process;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @ChangeWeapon => m_Wrapper.m_Player_ChangeWeapon;
        public InputAction @Attack02 => m_Wrapper.m_Player_Attack02;
        public InputAction @Attack03 => m_Wrapper.m_Player_Attack03;
        public InputAction @E_Btn => m_Wrapper.m_Player_E_Btn;
        public InputAction @_1 => m_Wrapper.m_Player__1;
        public InputAction @_2 => m_Wrapper.m_Player__2;
        public InputAction @_3 => m_Wrapper.m_Player__3;
        public InputAction @tab => m_Wrapper.m_Player_tab;
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
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @ChangeWeapon.started += instance.OnChangeWeapon;
            @ChangeWeapon.performed += instance.OnChangeWeapon;
            @ChangeWeapon.canceled += instance.OnChangeWeapon;
            @Attack02.started += instance.OnAttack02;
            @Attack02.performed += instance.OnAttack02;
            @Attack02.canceled += instance.OnAttack02;
            @Attack03.started += instance.OnAttack03;
            @Attack03.performed += instance.OnAttack03;
            @Attack03.canceled += instance.OnAttack03;
            @E_Btn.started += instance.OnE_Btn;
            @E_Btn.performed += instance.OnE_Btn;
            @E_Btn.canceled += instance.OnE_Btn;
            @_1.started += instance.On_1;
            @_1.performed += instance.On_1;
            @_1.canceled += instance.On_1;
            @_2.started += instance.On_2;
            @_2.performed += instance.On_2;
            @_2.canceled += instance.On_2;
            @_3.started += instance.On_3;
            @_3.performed += instance.On_3;
            @_3.canceled += instance.On_3;
            @tab.started += instance.OnTab;
            @tab.performed += instance.OnTab;
            @tab.canceled += instance.OnTab;
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
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @ChangeWeapon.started -= instance.OnChangeWeapon;
            @ChangeWeapon.performed -= instance.OnChangeWeapon;
            @ChangeWeapon.canceled -= instance.OnChangeWeapon;
            @Attack02.started -= instance.OnAttack02;
            @Attack02.performed -= instance.OnAttack02;
            @Attack02.canceled -= instance.OnAttack02;
            @Attack03.started -= instance.OnAttack03;
            @Attack03.performed -= instance.OnAttack03;
            @Attack03.canceled -= instance.OnAttack03;
            @E_Btn.started -= instance.OnE_Btn;
            @E_Btn.performed -= instance.OnE_Btn;
            @E_Btn.canceled -= instance.OnE_Btn;
            @_1.started -= instance.On_1;
            @_1.performed -= instance.On_1;
            @_1.canceled -= instance.On_1;
            @_2.started -= instance.On_2;
            @_2.performed -= instance.On_2;
            @_2.canceled -= instance.On_2;
            @_3.started -= instance.On_3;
            @_3.performed -= instance.On_3;
            @_3.canceled -= instance.On_3;
            @tab.started -= instance.OnTab;
            @tab.performed -= instance.OnTab;
            @tab.canceled -= instance.OnTab;
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
        void OnAttack(InputAction.CallbackContext context);
        void OnChangeWeapon(InputAction.CallbackContext context);
        void OnAttack02(InputAction.CallbackContext context);
        void OnAttack03(InputAction.CallbackContext context);
        void OnE_Btn(InputAction.CallbackContext context);
        void On_1(InputAction.CallbackContext context);
        void On_2(InputAction.CallbackContext context);
        void On_3(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
    }
}
