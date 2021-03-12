﻿using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;

namespace CrowdedMod.Patches
{
    public static class CreateGameOptionsPatches
    {
        [HarmonyPatch(typeof(CreateOptionsPicker), nameof(CreateOptionsPicker.Start))]
        public static class CreateOptionsPicker_Start // Credits to XtraCube (mostly)
        {
	        public const byte maxPlayers = 15;
	        
            static void Postfix(CreateOptionsPicker __instance)
            {
	            if (__instance.mode != SettingsMode.Host) return;
                var offset = __instance.MaxPlayerButtons[1].transform.position.x - __instance.MaxPlayerButtons[0].transform.position.x;

                #region MaxPlayers stuff
                
				List<SpriteRenderer> playerButtons = __instance.MaxPlayerButtons.ToList();
				
				SpriteRenderer plusButton = Object.Instantiate(playerButtons.Last(), playerButtons.Last().transform.parent);
				plusButton.GetComponentInChildren<TextRenderer>().Text = "+";
				plusButton.name = "255";
				plusButton.transform.position = playerButtons.Last().transform.position + new Vector3(offset*2, 0, 0);
				var passiveButton = plusButton.GetComponent<PassiveButton>();
				passiveButton.OnClick.m_PersistentCalls.m_Calls.Clear();
				passiveButton.OnClick.AddListener((UnityAction)plusListener);
				
				void plusListener()
				{
					byte curHighest = byte.Parse(playerButtons[__instance.MaxPlayerButtons.Length - 2].name);
					int delta = Mathf.Clamp(curHighest + 7, curHighest, maxPlayers) - curHighest;
					if (delta == 0) return; // fast skip
					for (byte i = 1; i < 8; i++)
					{
						SpriteRenderer button = __instance.MaxPlayerButtons[i];
						button.name = 
							button.GetComponentInChildren<TextRenderer>().Text = 
								(byte.Parse(button.name) + delta).ToString();
					}
					__instance.SetMaxPlayersButtons(__instance.GetTargetOptions().MaxPlayers); // MaxPlayers
				}
				
				SpriteRenderer minusButton = Object.Instantiate(playerButtons.Last(), playerButtons.Last().transform.parent);
				minusButton.GetComponentInChildren<TextRenderer>().Text = "-";
				minusButton.name = "255";
				minusButton.transform.position = playerButtons.First().transform.position;
				var minusPassiveButton = minusButton.GetComponent<PassiveButton>();
				minusPassiveButton.OnClick.m_PersistentCalls.m_Calls.Clear();
				minusPassiveButton.OnClick.AddListener((UnityAction)minusListener);
				
				void minusListener()
				{
					byte curLowest = byte.Parse(playerButtons[1].name);
					int delta = curLowest - Mathf.Clamp(curLowest - 7, 4, curLowest);
					if (delta == 0) return; // fast skip
					for (byte i = 1; i < 8; i++)
					{
						SpriteRenderer button = __instance.MaxPlayerButtons[i];
						button.name = 
							button.GetComponentInChildren<TextRenderer>().Text = 
								(byte.Parse(button.name) - delta).ToString();
					}
					__instance.SetMaxPlayersButtons(__instance.GetTargetOptions().MaxPlayers); // MaxPlayers
				}
				
				playerButtons.ForEach(b =>
				{
					var button = b.GetComponent<PassiveButton>();
					button.OnClick.m_PersistentCalls.m_Calls.Clear();
					void defaultListener()
					{
						byte value = byte.Parse(button.name);
						var targetOptions = __instance.GetTargetOptions();
						if (value <= targetOptions.NumImpostors) // NumImpostors
						{
							targetOptions.NumImpostors = value - 1;
							__instance.Method_64(targetOptions.NumImpostors); // UpdateImpostorButtons
						}
						__instance.SetMaxPlayersButtons(value);
					} 
					button.OnClick.AddListener((UnityAction)defaultListener);
					button.transform.position += new Vector3(offset, 0, 0);
				});
				
				playerButtons.Insert(0, minusButton);
				playerButtons.Add(plusButton);
				__instance.MaxPlayerButtons = playerButtons.ToArray();
				
				#endregion
			}
        }
    }
}