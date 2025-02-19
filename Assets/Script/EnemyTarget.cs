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
	public class EnemyTarget : MonoBehaviour
	{
		public UnityEvent onTakeDamage;

		public bool onceOnly = false;
		public Transform targetCenter;

		public Transform baseTransform;
		public Transform fallenDownTransform;
		public float fallTime = 0.5f;

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
			if ( targetEnabled )
			{
				onTakeDamage.Invoke();
				StartCoroutine( this.GoToCastle() );

				if ( onceOnly )
				{
					targetEnabled = false;
				}
			}
		}


		//-------------------------------------------------
		private IEnumerator GoToCastle()
		{
			Vector3[] Floor1 = { new Vector3(-15, .25f, -5), new Vector3(-15, .25f, 0), new Vector3(-15, .25f, 5), new Vector3(-15, .25f, 10), new Vector3(-15, .25f, 15), new Vector3(-10, .25f, -5), new Vector3(-10, .25f, 0), new Vector3(-10, .25f, 5), new Vector3(-10, .25f, 10), new Vector3(-10, .25f, 15), new Vector3(-5, .25f, -10), new Vector3(-5, .25f, -5), new Vector3(-5, .25f, 0), new Vector3(-5, .25f, 5), new Vector3(-5, .25f, 10), new Vector3(-5, .25f, 15), new Vector3(30, .25f, -10), new Vector3(30, .25f, -5), new Vector3(30, .25f, 0), new Vector3(30, .25f, 5), new Vector3(30, .25f, 10), new Vector3(30, .25f, 15), new Vector3(35, .25f, -10), new Vector3(35, .25f, -5), new Vector3(35,.25f,0), new Vector3(35, .25f, 5), new Vector3(35, .25f, 10), new Vector3(35, .25f, 15), new Vector3(40, .25f, -10), new Vector3(40, .25f, -5), new Vector3(40, .25f, 0), new Vector3(40, .25f, 5), new Vector3(40, .25f, 10), new Vector3(40, .25f, 15) };
			Vector3[] Floor2 = { new Vector3(-15, 5.25f, -5), new Vector3(-15, 5.25f, 0), new Vector3(-15, 5.25f, 5), new Vector3(-15, 5.25f, 10), new Vector3(-15, 5.25f, 15), new Vector3(-10, 5.25f, -5), new Vector3(-10, 5.25f, 0), new Vector3(-10, 5.25f, 5), new Vector3(-10, 5.25f, 10), new Vector3(-10, 5.25f, 15), new Vector3(-5, 5.25f, -10), new Vector3(-5, 5.25f, -5), new Vector3(-5, 5.25f, 0), new Vector3(-5, 5.25f, 5), new Vector3(-5, 5.25f, 10), new Vector3(-5, 5.25f, 15), new Vector3(25, 5.25f, -10), new Vector3(25, 5.25f, -5), new Vector3(25, 5.25f, 0), new Vector3(25, 5.25f, 5), new Vector3(25, 5.25f, 10), new Vector3(25, 5.25f, 15), new Vector3(30, 5.25f, -10), new Vector3(30, 5.25f, -5), new Vector3(30, 5.25f, 0), new Vector3(30, 5.25f, 5), new Vector3(30, 5.25f, 10), new Vector3(30, 5.25f, 15), new Vector3(35, 5.25f, -10), new Vector3(35, 5.25f, 10), new Vector3(35, 5.25f, 15), new Vector3(40, 5.25f, -10), new Vector3(40, 5.25f, 10), new Vector3(40, 5.25f, 15) };
			int spawnPoint = Random.Range(0,Floor1.Length+Floor2.Length);
			if (spawnPoint < Floor1.Length)
			{
				this.gameObject.transform.position = Floor1[spawnPoint];
			}
			else 
			{
				this.gameObject.transform.position = Floor2[spawnPoint - Floor1.Length];
			}
			yield return null;
		}
	}
}
