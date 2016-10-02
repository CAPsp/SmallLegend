using UnityEngine;
using System.Collections;


namespace Utility{

	public class HierarchyUtil{

		// 一番親のオブジェクトのタグを持ってくる(再帰)
		public static GameObject FindParent(GameObject child) {

			if(child.transform.parent == null) {
				return child;
			}
			else {
				return FindParent(child.transform.parent.gameObject);
			}

		}

	}

}