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
	public class ArcheryTarget : MonoBehaviour
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
			Vector3[] Floor1 = { new Vector3(1, 1,0.5f) };
			Vector3[] Floor2 = { };
			Vector3[] Floor3 = { };
			int spawnPoint = Random.Range(0,Floor1.Length+Floor2.Length+Floor3.Length);
			if (spawnPoint < Floor1.Length)
			{
				this.gameObject.transform.position = Floor1[spawnPoint];
			}
			else if (spawnPoint < Floor1.Length + Floor2.Length)
			{
				this.gameObject.transform.position = Floor2[spawnPoint - Floor1.Length];
			}
			else {
				this.gameObject.transform.position = Floor2[spawnPoint - (Floor1.Length+Floor2.Length)];
			}
			yield return null;
		}
	}
}
