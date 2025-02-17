//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Target that sends events when hit by an arrow
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class ScoreTarget : MonoBehaviour
	{
        
		public UnityEvent onTakeDamage;

		public bool onceOnly = true;
		public Transform targetCenter;

		public Transform baseTransform;
		public Transform fallenDownTransform;
		public float fallTime = 0.5f;
        public ScoreText scoreText;

        const float targetRadius = 0.25f;

		private bool targetEnabled = true;


		//-------------------------------------------------
		private void ApplyDamage()
		{
			OnDamageTaken();
		}


		//-------------------------------------------------
		private void FireExposure()
		{
			OnDamageTaken();
		}


		//-------------------------------------------------
		private void OnDamageTaken()
		{
			onTakeDamage.Invoke();
			scoreText.score++;
			this.gameObject.SetActive(false);
		}
	}
}
