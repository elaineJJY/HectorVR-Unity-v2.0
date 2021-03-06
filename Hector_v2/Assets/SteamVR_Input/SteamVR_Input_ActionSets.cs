//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Input_ActionSet_default p__default;
        
        private static SteamVR_Input_ActionSet_platformer p_platformer;
        
        private static SteamVR_Input_ActionSet_buggy p_buggy;
        
        private static SteamVR_Input_ActionSet_mixedreality p_mixedreality;
        
        private static SteamVR_Input_ActionSet_PlayerControl p_PlayerControl;
        
        private static SteamVR_Input_ActionSet_menu p_menu;
        
        private static SteamVR_Input_ActionSet_HintsSet p_HintsSet;
        
        private static SteamVR_Input_ActionSet_SelectRobot p_SelectRobot;
        
        public static SteamVR_Input_ActionSet_default _default
        {
            get
            {
                return SteamVR_Actions.p__default.GetCopy<SteamVR_Input_ActionSet_default>();
            }
        }
        
        public static SteamVR_Input_ActionSet_platformer platformer
        {
            get
            {
                return SteamVR_Actions.p_platformer.GetCopy<SteamVR_Input_ActionSet_platformer>();
            }
        }
        
        public static SteamVR_Input_ActionSet_buggy buggy
        {
            get
            {
                return SteamVR_Actions.p_buggy.GetCopy<SteamVR_Input_ActionSet_buggy>();
            }
        }
        
        public static SteamVR_Input_ActionSet_mixedreality mixedreality
        {
            get
            {
                return SteamVR_Actions.p_mixedreality.GetCopy<SteamVR_Input_ActionSet_mixedreality>();
            }
        }
        
        public static SteamVR_Input_ActionSet_PlayerControl PlayerControl
        {
            get
            {
                return SteamVR_Actions.p_PlayerControl.GetCopy<SteamVR_Input_ActionSet_PlayerControl>();
            }
        }
        
        public static SteamVR_Input_ActionSet_menu menu
        {
            get
            {
                return SteamVR_Actions.p_menu.GetCopy<SteamVR_Input_ActionSet_menu>();
            }
        }
        
        public static SteamVR_Input_ActionSet_HintsSet HintsSet
        {
            get
            {
                return SteamVR_Actions.p_HintsSet.GetCopy<SteamVR_Input_ActionSet_HintsSet>();
            }
        }
        
        public static SteamVR_Input_ActionSet_SelectRobot SelectRobot
        {
            get
            {
                return SteamVR_Actions.p_SelectRobot.GetCopy<SteamVR_Input_ActionSet_SelectRobot>();
            }
        }
        
        private static void StartPreInitActionSets()
        {
            SteamVR_Actions.p__default = ((SteamVR_Input_ActionSet_default)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_default>("/actions/default")));
            SteamVR_Actions.p_platformer = ((SteamVR_Input_ActionSet_platformer)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_platformer>("/actions/platformer")));
            SteamVR_Actions.p_buggy = ((SteamVR_Input_ActionSet_buggy)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_buggy>("/actions/buggy")));
            SteamVR_Actions.p_mixedreality = ((SteamVR_Input_ActionSet_mixedreality)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_mixedreality>("/actions/mixedreality")));
            SteamVR_Actions.p_PlayerControl = ((SteamVR_Input_ActionSet_PlayerControl)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_PlayerControl>("/actions/PlayerControl")));
            SteamVR_Actions.p_menu = ((SteamVR_Input_ActionSet_menu)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_menu>("/actions/menu")));
            SteamVR_Actions.p_HintsSet = ((SteamVR_Input_ActionSet_HintsSet)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_HintsSet>("/actions/HintsSet")));
            SteamVR_Actions.p_SelectRobot = ((SteamVR_Input_ActionSet_SelectRobot)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_SelectRobot>("/actions/SelectRobot")));
            Valve.VR.SteamVR_Input.actionSets = new Valve.VR.SteamVR_ActionSet[] {
                    SteamVR_Actions._default,
                    SteamVR_Actions.platformer,
                    SteamVR_Actions.buggy,
                    SteamVR_Actions.mixedreality,
                    SteamVR_Actions.PlayerControl,
                    SteamVR_Actions.menu,
                    SteamVR_Actions.HintsSet,
                    SteamVR_Actions.SelectRobot};
        }
    }
}
