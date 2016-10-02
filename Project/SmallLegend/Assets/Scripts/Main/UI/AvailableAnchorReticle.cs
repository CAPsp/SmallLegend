using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;

// アンカー有効範囲に照準を向けてるときに色が変わる処理を制御
public class AvailableAnchorReticle {

	Image reticle_;
	Sprite availableReticle_;
	Sprite notAvailableReticle_;

	public AvailableAnchorReticle(Image reticle, Sprite available, Sprite notAvailable){
		reticle_ 				= reticle;
		availableReticle_ 		= available;
		notAvailableReticle_ 	= notAvailable;
	}

	public void Update(Vector3 shotPoint, Vector3 targetPoint){

		Vector3 angle = targetPoint - shotPoint;
		angle.Normalize ();

		RaycastHit hit;
		if (Physics.Raycast (shotPoint, angle, out hit, Anchor.limitAnchorLength_)) {
			GameObject parent = HierarchyUtil.FindParent (hit.transform.gameObject);

			switch (parent.tag) {

			case "Environment":
			case "Enemy":
				reticle_.sprite = availableReticle_;
				return;

			}

		}
			
		reticle_.sprite = notAvailableReticle_;

	}
		
}
