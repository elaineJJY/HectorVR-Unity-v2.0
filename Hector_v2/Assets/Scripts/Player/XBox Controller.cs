// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/XBox Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @XBoxController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @XBoxController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""XBox Controller"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""a2da98d8-97f6-4893-8dba-ccfdac38b280"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ed4a400d-0cf5-44e8-a0ad-39e03dfb5de1"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotationL"",
                    ""type"": ""Button"",
                    ""id"": ""61a7e5e3-4579-4204-814d-74c1fc0aa871"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotationR"",
                    ""type"": ""Button"",
                    ""id"": ""71f2855b-faad-479d-a0e5-0f7c5db06141"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""26a20c59-e1d7-4c69-a5df-a6e773c02e12"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move forward"",
                    ""type"": ""Button"",
                    ""id"": ""c2a6af26-fcf7-4007-8d2a-71d979ca4921"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move backward"",
                    ""type"": ""Button"",
                    ""id"": ""d12f4f56-6755-4b94-9c09-7f54a698ab65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a2aa966d-d88c-45af-bc17-7fa5def12118"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a065f1af-abdc-4d72-b733-a83381035547"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d86ff137-5263-423e-8510-73f368cb1564"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66fb08fa-4865-4a2e-8253-064225f112a3"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a03dfacf-77f8-4563-ab4f-0a697f5077e6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63c2077e-bf44-43c8-aecf-993fa504b994"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a70a6290-ce04-46f4-b526-008611cccb20"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""459e7b01-8a37-4a61-b9ed-0e509beb0d4e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dca0ff1-a0ba-4afa-8905-35f4189d152a"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f906de1b-81d4-49c4-a776-a5c22d268ede"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_RotationL = m_Movement.FindAction("RotationL", throwIfNotFound: true);
        m_Movement_RotationR = m_Movement.FindAction("RotationR", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_Moveforward = m_Movement.FindAction("Move forward", throwIfNotFound: true);
        m_Movement_Movebackward = m_Movement.FindAction("Move backward", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_RotationL;
    private readonly InputAction m_Movement_RotationR;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_Moveforward;
    private readonly InputAction m_Movement_Movebackward;
    public struct MovementActions
    {
        private @XBoxController m_Wrapper;
        public MovementActions(@XBoxController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @RotationL => m_Wrapper.m_Movement_RotationL;
        public InputAction @RotationR => m_Wrapper.m_Movement_RotationR;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @Moveforward => m_Wrapper.m_Movement_Moveforward;
        public InputAction @Movebackward => m_Wrapper.m_Movement_Movebackward;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @RotationL.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotationL;
                @RotationL.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotationL;
                @RotationL.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotationL;
                @RotationR.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotationR;
                @RotationR.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotationR;
                @RotationR.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotationR;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Moveforward.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveforward;
                @Moveforward.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveforward;
                @Moveforward.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveforward;
                @Movebackward.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovebackward;
                @Movebackward.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovebackward;
                @Movebackward.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovebackward;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotationL.started += instance.OnRotationL;
                @RotationL.performed += instance.OnRotationL;
                @RotationL.canceled += instance.OnRotationL;
                @RotationR.started += instance.OnRotationR;
                @RotationR.performed += instance.OnRotationR;
                @RotationR.canceled += instance.OnRotationR;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Moveforward.started += instance.OnMoveforward;
                @Moveforward.performed += instance.OnMoveforward;
                @Moveforward.canceled += instance.OnMoveforward;
                @Movebackward.started += instance.OnMovebackward;
                @Movebackward.performed += instance.OnMovebackward;
                @Movebackward.canceled += instance.OnMovebackward;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotationL(InputAction.CallbackContext context);
        void OnRotationR(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMoveforward(InputAction.CallbackContext context);
        void OnMovebackward(InputAction.CallbackContext context);
    }
}
